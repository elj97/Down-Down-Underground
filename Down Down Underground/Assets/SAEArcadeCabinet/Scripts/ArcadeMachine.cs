using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/*
    Script: SAE.ArcadeMachine
    Author: Gareth Lockett
    Version: 1.0
    Description:    Script for configuring and getting joystick axis and buttons on the SAE arcade machine cabinet.

                    Usage:
                            - Attach this script to an empty game object in the scene (Note: this script is set to DontDestroyOnLoad)
                            - Options on component for toggling the use of events.

                    There are 3 ways to use this script:

                        1) Polling 
                            - Make sure to toggle OFF 'useEvents' on the component (If you only want to use polling)
                            - You can directly call SAE.ArcadeMachine.instance.PlayerPressingButton( PlayerColorId, ButtonId, SinglePress [optional] ) from any other script to find if a particular button for a specified player is currently pressed.
                            - You can directly call SAE.ArcadeMachine.instance.PlayerJoystickAxis( PlayerColorId ) from any other script to get the current X and Y axis position of a joystick.
                                OR
                            - You can directly call SAE.ArcadeMachine.PlayerPressingButtonStatic( PlayerColorId, ButtonId, SinglePress [optional] ) from any other script to find if a particular button for a specified player is currently pressed.
                            - You can directly call SAE.ArcadeMachine.PlayerJoystickAxisStatic( PlayerColorId ) from any other script to get the current X and Y axis position of a joystick.

                        2) Unity events
                            - Set the exposed Unity events on the component:
                                playerJoystickButtonPressedEvents:  invoked when a player first presses a button. Passes the playerColorId and buttonId.
                                playerJoystickButtonHeldEvents:     invoked when a player continues to hold down a button. Passes the playerColorId and buttonId.
                                playerJoystickButtonReleasedEvents: invoked when a player stops pressing a button. Passes the playerColorId and buttonId.
                                playerJoystickAxisEvents:           invoked when a player moves a joystick. Passes the playerColorId and axis values.
                                configurationFinishedEvent:         invoked when player configuration finishes. Does not pass anything.

                        3) System actions
                            - Subscribe to events from your own scripts:
                                playerPressedButton:                invoked when a player first presses a button. Passes the playerColorId and buttonId.
                                playerHeldButton:                   invoked when a player continues to hold down a button. Passes the playerColorId and buttonId.
                                playerReleasedButton:               invoked when a player stops pressing a button. Passes the playerColorId and buttonId.
                                playerJoystickAxisChanged:          invoked when a player moves a joystick. Passes the playerColorId and axis values.
                                configurationFinished:              invoked when player configuration finishes. Does not pass anything.


                    Notes:
                            - Will then automatically execute ConfigurePlayers() on Start(). Otherwise you can manually call SAE.ArcadeMachine.instance.ConfigurePlayers() from your own script.

                            - It is possible to mix and match polling, Unity Events, and System Actions. Just make sure ConfigurePlayers() gets called before polling or listening for events.
                                - For example, you might set up the Unity Event for configurationFinishedEvent to trigger hiding/unhiding some GameObject, 
                                    whilst listening for player button presses as System Actions, 
                                    and polling for player joystick axis positions during your own script's Update() method.

                            - Make sure legacy InputManager has been set up with X, Y axis 1-8 joysticks (KeyCode buttons seem to only go to 8 per joystick?)

                            - Escape key during configuration will stop configuring (But keep already configured joysticks)

                            - dontConfigureInEditor will allow you to skip configuration in the editor (Eg while setting up your game)
*/

namespace SAE
{
    public class ArcadeMachine : MonoBehaviour
    {
        // System events
        public static Action<PlayerColorId, int> playerPressedButton;               // <Joystick id, Button id> Invoked when a button has started to be pressed.
        public static Action<PlayerColorId, int> playerHeldButton;                  // <Joystick id, Button id> Invoked each frame while a button is held down.
        public static Action<PlayerColorId, int> playerReleasedButton;              // <Joystick id, Button id> Invoked when a button has stopped been pressed.
        public static Action<PlayerColorId, Vector2> playerJoystickAxisChanged;     // <Joystick id, Vector2 XY axis values> Invoked when an axis value changed.
        public static Action configurationFinished;                                 // Invoked when configuraion finishes.

        // Unity events
        [Serializable] public class PlayerJoystickButtonEvent : UnityEvent<PlayerColorId, int> { }
        [Serializable] public class PlayerJoystickAxisEvent : UnityEvent<PlayerColorId, Vector2> { }
        public PlayerJoystickButtonEvent playerJoystickButtonPressedEvents, playerJoystickButtonHeldEvents, playerJoystickButtonReleasedEvents;
        public PlayerJoystickAxisEvent playerJoystickAxisEvents;
        public UnityEvent configurationFinishedEvent;

        // Enumerators
        public enum PlayerColorId { UNKNOWN, YELLOW_PLAYER, BLUE_PLAYER, RED_PLAYER, GREEN_PLAYER }

        // Sub classes
        private class PlayerInput
        {
            public PlayerColorId playerId;                                          // Will be either:UNKNOWN, YELLOW_PLAYER, BLUE_PLAYER, RED_PLAYER, GREEN_PLAYER;
            public int joystickId;                                                  // The mapped joystick id (1-4)
            public int firstButtonKeyCodeId;                                        // Joystick 1 = 350, Joystick 2 = 370, Joystick 3 = 390, Joystick 4 = 410
            public List<KeyCode> buttonsPressed;                                    // Keycodes of buttons currently pressed.
            public Vector2 lastJoystickPosition;                                    // Last checked joystick x,y axis position.

            // Constructor
            public PlayerInput()
            {
                this.playerId = PlayerColorId.UNKNOWN;
                this.joystickId = -1;
                this.buttonsPressed = new List<KeyCode>();
            }

            // Methods
            public void ResetPlayer()
            {
                this.playerId = PlayerColorId.UNKNOWN;
                this.joystickId = -1;
                this.firstButtonKeyCodeId = 0;
                this.buttonsPressed.Clear();
                this.lastJoystickPosition = Vector2.zero;
            }
        }

        // Properties
        public static ArcadeMachine input;                                          // Singleton reference.
        public bool useEvents = true;                                               // Allow invoking of events for button presses, axis changes etc
        public KeyCode resetPlayersKey = KeyCode.F11;                               // Clear all players joystick mapping and trigger ConfigurePlayers().

        public bool dontConfigureInEditor;                                          // If true, will just assign joysticks as they are detected automatically.

        private PlayerInput[] playerInputs;                                         // The 4 player inputs representing the joysticks & buttons.
        private bool configuring;                                                   // Track the configuring state.

        private float configurationStartTime;                                       // Used to give a few seconds before listen for buttons/axis when configuring.
        private float nextEventListenTime;                                          // Used to give a few seconds after configuring before listening for events.
        private Canvas configureCanvas;                                             // Reference to the configuration canvas.
        private Text configurationText;                                             // Reference to the configuration text.

        // Methods
        private void Awake()
        {
            // Set up singleton.
            if( ArcadeMachine.input != null ) { if( ArcadeMachine.input != this ) { Destroy( this ); return; } }
            ArcadeMachine.input = this;

            DontDestroyOnLoad( this.gameObject );
        }

        private void Start()
        {
            // Manually create 4 players.
            this.playerInputs = new PlayerInput[ 4 ];
            for( int i = 0; i < this.playerInputs.Length; i++ ) { this.playerInputs[ i ] = new PlayerInput(); }

            // Configure players (Note: You can comment this out if you want to call SAE.ArcadeMachine.instance.ConfigurePlayers() from your own script to start configuration)
            this.ConfigurePlayers();
        }

        private void Update()
        {
            // Check if rest all players key has been pressed.
            if( Input.GetKeyDown( this.resetPlayersKey ) == true ) { this.ResetAllPlayers(); }

            // Check if configuring.
            if( this.configuring == true ) { this.ConfigurePlayers(); return; }
            else if( this.configureCanvas != null ) { Destroy( this.configureCanvas.gameObject ); }

            // Check for events.
            if( this.useEvents == true ) { if( Time.unscaledTime > this.nextEventListenTime ) { this.CheckForEvents(); } }
        }

        private void CheckForEvents()
        {
            // Sanity check. Check playerInputs have been created/exist (Should have been set up in Start)
            if( this.playerInputs == null ) { return; }
            if( this.configuring == true ) { return; }

            // Check for any button presses.
            int startKeyCodeId = ( int ) KeyCode.Joystick1Button0;  // 350
            int endKeyCodeId = ( int ) KeyCode.Joystick8Button19;   // 509
            for( int i = startKeyCodeId; i < endKeyCodeId; i++ )
            {
                KeyCode keyCode = ( KeyCode ) i;
                bool keyPressed = Input.GetKey( keyCode );

                // Get the joystick & button id from the keyCode.
                int joystickId = -1, buttonId = -1;
                if( this.GetKeyCodeJoystickAndButtonId( keyCode, ref joystickId, ref buttonId ) == false ) { continue; }
                if( joystickId == -1 || buttonId == -1 ) { continue; } //?

                // Get the player input by the joystick id.
                PlayerInput playerInput = this.GetPlayerInputByJoystickId( joystickId );
                if( playerInput == null )
                {
                    if( keyPressed == true )
                    {
                        // Key as pressed by an unknown player ... configure!
                        this.configuring = true;
                    }
                    continue; //?
                }

                // Check for button events.
                if( keyPressed == true )
                {
                    if( playerInput.buttonsPressed.Contains( keyCode ) == false )
                    {
                        // Started pressing the button.
                        playerInput.buttonsPressed.Add( keyCode ); // Add the button.
                        ArcadeMachine.playerPressedButton?.Invoke( playerInput.playerId, buttonId );      // Invoke any pressed button events.
                        this.playerJoystickButtonPressedEvents?.Invoke( playerInput.playerId, buttonId );
                    }
                    else
                    {
                        ArcadeMachine.playerHeldButton?.Invoke( playerInput.playerId, buttonId );         // Invoke any holding down the button events.
                        this.playerJoystickButtonHeldEvents?.Invoke( playerInput.playerId, buttonId );
                    }
                }
                else if( playerInput.buttonsPressed.Contains( keyCode ) == true )
                {
                    playerInput.buttonsPressed.Remove( keyCode ); // Remove the button.
                    ArcadeMachine.playerReleasedButton?.Invoke( playerInput.playerId, buttonId );         // Invoke any released button events.
                    this.playerJoystickButtonReleasedEvents?.Invoke( playerInput.playerId, buttonId );
                }
            }

            // Check for joystick axis change events.
            for( int i = 0; i < this.playerInputs.Length; i++ )
            {
                if( this.playerInputs[ i ].playerId == PlayerColorId.UNKNOWN ) { continue; }
                if( this.playerInputs[ i ].joystickId <= 0 ) { continue; }

                // Check for changed axis.
                string axisName = "Joystick" + this.playerInputs[ i ].joystickId; // Eg Joystick1_xAxis
                Vector2 axisValues = new Vector2( Input.GetAxis( axisName + "_xAxis" ), Input.GetAxis( axisName + "_yAxis" ) );
                if( axisValues.x == this.playerInputs[ i ].lastJoystickPosition.x && axisValues.y == this.playerInputs[ i ].lastJoystickPosition.y ) { continue; } // No change.

                // Invoke any axis change events.
                ArcadeMachine.playerJoystickAxisChanged?.Invoke( this.playerInputs[ i ].playerId, axisValues );
                this.playerJoystickAxisEvents?.Invoke( this.playerInputs[ i ].playerId, axisValues );

                // Update the last axis values.
                this.playerInputs[ i ].lastJoystickPosition = axisValues;
            }
        }

        public void ConfigurePlayers()
        {
            // Set configuring state.
            this.configuring = true;

            // Check if configuring in the editor.
            if( Application.isEditor == true && this.dontConfigureInEditor == true )
            {
                string[] allJoystickNames = Input.GetJoystickNames();
                Debug.Log( "[SAE.ArcadeMachine] "+allJoystickNames.Length  +" joysticks autoconfiguring in editor." );

                // Automatically set the detected joysticks using the order they are detected in (Eg first joystick will be yellow player etc)
                for( int i = 0; i < allJoystickNames.Length; i++ )
                {
                    if( i >= this.playerInputs.Length ) { this.FinishConfiguration(); return; }
                    if( i >= Enum.GetNames( typeof( PlayerColorId ) ).Length ) { this.FinishConfiguration(); return; }

                    this.playerInputs[ i ].playerId = ( PlayerColorId ) i + 1;
                    this.playerInputs[ i ].joystickId = i + 1;
                    this.SetPlayerInputFirstButtonKeyCodeId( this.playerInputs[ i ] );
                }
                this.FinishConfiguration();
                return;
            }

            // Check if skipping configuring any other players.
            if( Input.GetKeyDown( KeyCode.Escape ) == true )
            {
                this.FinishConfiguration();
                return;
            }

            // Find the next player to configure.
            int maxPlayers = Enum.GetNames( typeof( PlayerColorId ) ).Length -1;
//maxPlayers = 2; // TESTING
            for( int p = 1; p < maxPlayers + 1; p++ )
            {
                PlayerColorId playerId = ( PlayerColorId ) p;
                if( this.GetPlayerInputById( playerId ) == null )
                {
//Debug.Log( "[SAE.ArcadeMachine] Configure " + playerId.ToString() );

                    // Check there is a configuration canvas. If not, create one.
                    if( this.configureCanvas == null )
                    {
                        // Create a canvas.
                        GameObject configCanvas = new GameObject( "configCanvas" );
                        this.configureCanvas = configCanvas.AddComponent<Canvas>();
                        this.configureCanvas.renderMode = RenderMode.ScreenSpaceOverlay;

                        // Create a black background to hide the scene.
                        GameObject bgPanelGO = new GameObject( "bgPanel" );
                        bgPanelGO.transform.SetParent( configCanvas.transform );
                        Image bgPanel = bgPanelGO.AddComponent<Image>();
                        bgPanel.color = Color.black;
                        RectTransform rtPanel = bgPanel.GetComponent<RectTransform>();
                        rtPanel.anchoredPosition = Vector2.zero;
                        rtPanel.SetSizeWithCurrentAnchors( RectTransform.Axis.Horizontal, Screen.width );
                        rtPanel.SetSizeWithCurrentAnchors( RectTransform.Axis.Vertical, Screen.height );

                        // Create configuration text.
                        GameObject textGO = new GameObject( "configText" );
                        textGO.transform.SetParent( bgPanel.transform );
                        this.configurationText = textGO.AddComponent<Text>();
                        this.configurationText.color = Color.white;
                        RectTransform rtText = this.configurationText.GetComponent<RectTransform>();
                        rtText.anchoredPosition = Vector2.up * ( Screen.height * 0.15f );
                        rtText.SetSizeWithCurrentAnchors( RectTransform.Axis.Horizontal, Screen.width * 0.9f );
                        this.configurationText.resizeTextForBestFit = true;
                        this.configurationText.font = Font.CreateDynamicFontFromOSFont( "Arial", 36 );// Mathf.RoundToInt( Screen.width * 0.02f ) );
                        this.configurationText.supportRichText = true;
                        this.configurationText.verticalOverflow = VerticalWrapMode.Overflow;
                        this.configurationText.alignment = TextAnchor.UpperCenter;

                        this.configurationStartTime = Time.unscaledTime;
                    }

                    // Set the title text for configuring.
                    this.configurationText.text = "<b>Input Configuration</b>\n\n";

                    // Give a few seconds before checking for buttons.
                    if( Time.unscaledTime < this.configurationStartTime + 2f ) { return; }

                    // Set the text for the configuring player.
                    string playerColorStr = playerId.ToString().Replace( "_PLAYER", "" );
                    this.configurationText.text += "<color=" + playerColorStr.ToLower() + ">" + playerColorStr + " player press a button or move the joystick.</color>";

                    PlayerInput playerInput = null;

                    // Check for any axis input.
                    int numSetupAxis = 8; // This is the number of xAxis and yAxis joystick inputs set up in the legacy InputManager.
                    for( int i = 0; i < numSetupAxis; i++ )
                    {
                        // Check if the joystick id has already been assigned.
                        if( this.GetPlayerInputByJoystickId( i + 1 ) != null ) { continue; }

                        // Check the axis for movement.
                        string axisName = "Joystick" + ( i + 1 ); // Eg Joystick1_xAxis
                        if( Mathf.Abs( Input.GetAxis( axisName + "_xAxis" ) ) < 0.5f && Mathf.Abs( Input.GetAxis( axisName + "_yAxis" ) ) < 0.5f ) { continue; }

                        // Assign the joystick id to the first UNKNOWN player.
                        playerInput = this.GetPlayerInputById( PlayerColorId.UNKNOWN );
                        if( playerInput == null )
                        {
Debug.LogWarning( "[SAE.ArcadeMachine] Could not get an UNKNOWN player in configuration?!" );
                            this.configuring = false;
                            return;
                        }
                        playerInput.playerId = playerId;
                        playerInput.joystickId = i + 1;

                        break;
                    }

                    // Check for any button presses.
                    if( playerInput == null )
                    {
                        int startKeyCodeId = ( int ) KeyCode.Joystick1Button0;  // 350
                        int endKeyCodeId = ( int ) KeyCode.Joystick8Button19;   // 509
                        for( int i = startKeyCodeId; i < endKeyCodeId; i++ )
                        {
                            KeyCode keyCode = ( KeyCode ) i;
                            bool keyPressed = Input.GetKey( keyCode );
                            if( keyPressed == false ) { continue; }

                            // Get the joystick and button ids for the pressed keyCode.
                            int joystickId = -1, buttonId = -1;
                            if( this.GetKeyCodeJoystickAndButtonId( keyCode, ref joystickId, ref buttonId ) == false ) { continue; }
                            if( joystickId == -1 || buttonId == -1 ) { continue; } //?

                            // Check if the pressed buttons' joystick id already belongs to another player.
                            if( this.GetPlayerInputByJoystickId( joystickId ) != null ) { continue; }

                            // Assign the joystick id to the first UNKNOWN player.
                            playerInput = this.GetPlayerInputById( PlayerColorId.UNKNOWN );
                            if( playerInput == null )
                            {
Debug.LogWarning( "[SAE.ArcadeMachine] Could not get an UNKNOWN player in configuration?!" );
                                this.configuring = false;
                                return;
                            }
                            playerInput.playerId = playerId;
                            playerInput.joystickId = joystickId;

                            break;
                        }
                    }

                    if( playerInput != null )
                    {
                        // Set the first keyCode button based on playerId.
                        this.SetPlayerInputFirstButtonKeyCodeId( playerInput );

Debug.Log( "[SAE.ArcadeMachine] " + playerInput.playerId.ToString() + " was assigned to joystick " + playerInput.joystickId );

                        // Check if finished configuring (Eg no more UNKOWN players)
                        if( p == maxPlayers ) { this.FinishConfiguration(); }
                    }

                    return;
                }
            }
        }
        private void SetPlayerInputFirstButtonKeyCodeId( PlayerInput playerInput )
        {
            if( playerInput == null ) { return; }
            // Set the first keyCode button based on playerId.
            switch( playerInput.joystickId )
            {
                case 1: playerInput.firstButtonKeyCodeId = 350; break;
                case 2: playerInput.firstButtonKeyCodeId = 370; break;
                case 3: playerInput.firstButtonKeyCodeId = 390; break;
                case 4: playerInput.firstButtonKeyCodeId = 410; break;
                case 5: playerInput.firstButtonKeyCodeId = 430; break;
                case 6: playerInput.firstButtonKeyCodeId = 450; break;
                case 7: playerInput.firstButtonKeyCodeId = 470; break;
                case 8: playerInput.firstButtonKeyCodeId = 490; break;
            }
        }
        private void FinishConfiguration()
        {
            ArcadeMachine.configurationFinished?.Invoke();
            this.configurationFinishedEvent?.Invoke();
            this.configuring = false;
            this.nextEventListenTime = Time.unscaledTime + 2f;
            Debug.Log( "[SAE.ArcadeMachine] Finished configuration." );
        }

        private bool GetKeyCodeJoystickAndButtonId( KeyCode keyCode, ref int joystickId, ref int buttonId )
        {
            // Get the joystick & button id from the keyCode (Eg Joystick1Button0 == 1)
            if( keyCode.ToString().StartsWith( "Joystick" ) == false ) { return false; }
            string[] tmpStrParts = keyCode.ToString().Replace( "Joystick", "" ).Replace( "Button", "," ).Split( ",".ToCharArray() );
            if( tmpStrParts.Length != 2 ) { return false; }
            joystickId = int.Parse( tmpStrParts[ 0 ] );
            buttonId = int.Parse( tmpStrParts[ 1 ] );
            return true;
        }

        private PlayerInput GetPlayerInputById( PlayerColorId playerId )
        {
            // Sanity check.
            if( this.playerInputs == null ) { return null; }
            return this.playerInputs.FirstOrDefault( item => item.playerId == playerId );
        }

        private PlayerInput GetPlayerInputByJoystickId( int joystickId )
        {
            // Sanity check.
            if( this.playerInputs == null ) { return null; }
            return this.playerInputs.FirstOrDefault( item => item.joystickId == joystickId );
        }

        private void ResetAllPlayers()
        {
            // Sanity check.
            if( this.playerInputs == null ) { return; }

            // Reset all the players.
            for( int i=0; i<this.playerInputs.Length; i++ ){ this.playerInputs[ i ].ResetPlayer(); }

Debug.Log( "[SAE.ArcadeMachine] Reset all player joystick mappings." );

            // Trigger ConfigurePlayers()
            this.ConfigurePlayers();
        }


        public bool PlayerPressingButton( PlayerColorId playerId, int buttonId, bool singlePress = false )
        {
            // Sanity checks.
            if( this.playerInputs == null ) { return false; }
            if( playerId == PlayerColorId.UNKNOWN ) { return false; }
            if( buttonId < 0 ) { return false; }
            if( this.configuring == true ) { return false; }

            // Get the player input via id.
            PlayerInput playerInput = this.GetPlayerInputById( playerId );
            if( playerInput == null ) { return false; }

            // Calculate the button keyCode id and check the key.
            if( singlePress == false ) { return Input.GetKey( ( KeyCode ) playerInput.firstButtonKeyCodeId + buttonId ); }
            else { return Input.GetKeyDown( ( KeyCode ) playerInput.firstButtonKeyCodeId + buttonId ); }
        }
        public Vector2 PlayerJoystickAxis( PlayerColorId playerId )
        {
            // Sanity checks.
            if( this.playerInputs == null ) { return Vector2.zero; }
            if( playerId == PlayerColorId.UNKNOWN ) { return Vector2.zero; }
            if( this.configuring == true ) { return Vector2.zero; }

            // Get the player input via id.
            PlayerInput playerInput = this.GetPlayerInputById( playerId );
            if( playerInput == null ) { return Vector2.zero; }
            if( playerInput.joystickId <= 0 ) { return Vector2.zero; }

            string axisName = "Joystick" + playerInput.joystickId; // Eg Joystick1_xAxis
            return new Vector2( Input.GetAxis( axisName + "_xAxis" ), Input.GetAxis( axisName + "_yAxis" ) );
        }

        // Convenience static methods.
        public static bool PlayerPressingButtonStatic( PlayerColorId playerId, int buttonId, bool singlePress = false )
        {
            // Make sure a SAE.ArcadeMachine.input singleton exists.
            if( SAE.ArcadeMachine.input == null ) { return false; }
            return SAE.ArcadeMachine.input.PlayerPressingButton( playerId , buttonId, singlePress );
        }
        public static Vector2 PlayerJoystickAxisStatic( PlayerColorId playerId )
        {
            // Make sure a SAE.ArcadeMachine.input singleton exists.
            if( SAE.ArcadeMachine.input == null ) { return Vector2.zero; }
            return SAE.ArcadeMachine.input.PlayerJoystickAxis( playerId );
        }
    }
}
