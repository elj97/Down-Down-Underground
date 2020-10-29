using UnityEngine;

/*
    Script: SAEArcadeCabinetInputTestMB
    Author: Gareth Lockett
    Version: 1.0
    Description:    Simple script for testing SAE ArcadeMachine inputs.
                    Includes examples of polling and System Action events.
*/

public class SAEArcadeCabinetInputTest : MonoBehaviour
{
    // Methods
    private void Start()
    {
        // Subscribe event handler methods.
        SAE.ArcadeMachine.playerPressedButton += this.PlayerPressedButton;
        SAE.ArcadeMachine.playerHeldButton += this.PlayerHeldButton;
        SAE.ArcadeMachine.playerReleasedButton += this.PlayerReleasedButton;
        SAE.ArcadeMachine.playerJoystickAxisChanged += this.PlayerJoystickAxisChanged;
        SAE.ArcadeMachine.configurationFinished += this.ConfigurationFinsihed;

        // If you are only going to use polling, then you need to configure players first! (Probably good to do this for events too)
        SAE.ArcadeMachine.input.ConfigurePlayers();
    }

    private void Update()
    {
        // Example of polling buttons (Eg Checking here if the YELLOW players' button 1 is pressed)
        if( SAE.ArcadeMachine.input.PlayerPressingButton( SAE.ArcadeMachine.PlayerColorId.YELLOW_PLAYER, 1 ) == true )
            { Debug.Log( "Polling: YELLOW player is pressing button 1" ); }

        // Example of polling axis (Eg Checking YELLOW players' joystick axis)
        Debug.Log( "YELLOW joystick axis: " + SAE.ArcadeMachine.input.PlayerJoystickAxis( SAE.ArcadeMachine.PlayerColorId.YELLOW_PLAYER ) );
    }
    
    // Event handlers.
    private void PlayerPressedButton( SAE.ArcadeMachine.PlayerColorId playerId, int buttonId )
    {
        Debug.Log( "PlayerPressedButton: " + playerId.ToString() + "  " + buttonId );
    }
    private void PlayerHeldButton( SAE.ArcadeMachine.PlayerColorId playerId, int buttonId )
    {
        Debug.Log( "PlayerHeldButton: " + playerId.ToString() + "  " + buttonId );
    }
    private void PlayerReleasedButton( SAE.ArcadeMachine.PlayerColorId playerId, int buttonId )
    {
        Debug.Log( "PlayerReleasedButton: " + playerId.ToString() + "  " + buttonId );
    }
    private void PlayerJoystickAxisChanged( SAE.ArcadeMachine.PlayerColorId playerId, Vector2 axis )
    {
        Debug.Log( "PlayerJoystickAxisChanged: " + playerId.ToString() + "  " + axis );
    }
    private void ConfigurationFinsihed()
    {
        Debug.Log( "ConfigurationFinsihed!" );
    }
}
