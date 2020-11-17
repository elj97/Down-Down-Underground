using UnityEngine;

/*
    Script: Based on GameManager_Test1
    Author: Gareth Lockett
    Version: 1.0
    Description:    Super simple example moving player cubes around using input from SAE arcade machine.
                    Make sure the SAE.ArcadeMachine component/prefab is in the scene and calls ConfigurePlayer() (Eg use WITHOUT events for this example)
*/

namespace SAE
{
    public class GameManagerModified : MonoBehaviour
    {
        // Properties
        public GameObject yellowPlayer;
        public GameObject bluePlayer;
        public GameObject redPlayer;
        public GameObject greenPlayer;

        Rigidbody yellowPlayerRigidbody;          // Reference to yellow players' Rigidbody component.
        Rigidbody bluePlayerRigidbody;            // Reference to blue players' Rigidbody component.
        Rigidbody redPlayerRigidbody;             // Reference to red players' Rigidbody component.
        Rigidbody greenPlayerRigidbody;           // Reference to green players' Rigidbody component.

        PlayerController yellowPlayerController;
        PlayerController bluePlayerController;
        PlayerController redPlayerController;
        PlayerController greenPlayerController;

        public float moveSpeed = 5f;            // Velocity speed of moving players.
        public Vector2 wrapSize;                // When a players position goes outside these (width/height) values, teleport to other side.

        private void Start()
        {
            yellowPlayerRigidbody = yellowPlayer.GetComponent<Rigidbody>();
            bluePlayerRigidbody = bluePlayer.GetComponent<Rigidbody>();
            redPlayerRigidbody = redPlayer.GetComponent<Rigidbody>();
            greenPlayerRigidbody = greenPlayer.GetComponent<Rigidbody>();

            yellowPlayerController = yellowPlayer.GetComponent<PlayerController>();
            bluePlayerController = bluePlayer.GetComponent<PlayerController>();
            redPlayerController = redPlayer.GetComponent<PlayerController>();
            greenPlayerController = greenPlayer.GetComponent<PlayerController>();
        }

        // Methods
        private void Update()
        {
            // Poll SAE.ArcadeMachine for players' joystick axis and use to set Rigidbody velocity.
            Vector2 axisValues = SAE.ArcadeMachine.PlayerJoystickAxisStatic(ArcadeMachine.PlayerColorId.YELLOW_PLAYER);
            this.yellowPlayerRigidbody.velocity = new Vector3(axisValues.x, -axisValues.y, 0f) * this.moveSpeed;
            yellowPlayerController.movementInputX = axisValues.x;
            yellowPlayerController.movementInputY = axisValues.y;
            //print("the y value is " + axisValues.y);
            //print("the x value is " + axisValues.x);

            axisValues = SAE.ArcadeMachine.PlayerJoystickAxisStatic(ArcadeMachine.PlayerColorId.BLUE_PLAYER);
            this.bluePlayerRigidbody.velocity = new Vector3(axisValues.x, -axisValues.y, 0f) * this.moveSpeed;
            bluePlayerController.movementInputX = axisValues.x;
            bluePlayerController.movementInputY = axisValues.y;
            //bluePlayerController.movementInput = axisValues;

            axisValues = SAE.ArcadeMachine.PlayerJoystickAxisStatic(ArcadeMachine.PlayerColorId.RED_PLAYER);
            this.redPlayerRigidbody.velocity = new Vector3(axisValues.x, -axisValues.y, 0f) * this.moveSpeed;
            redPlayerController.movementInputX = axisValues.x;
            redPlayerController.movementInputY = axisValues.y;
            //redPlayerController.movementInput = axisValues;

            axisValues = SAE.ArcadeMachine.PlayerJoystickAxisStatic(ArcadeMachine.PlayerColorId.GREEN_PLAYER);
            this.greenPlayerRigidbody.velocity = new Vector3(axisValues.x, -axisValues.y, 0f) * this.moveSpeed;
            greenPlayerController.movementInputX = axisValues.x;
            greenPlayerController.movementInputY = axisValues.y;
            //greenPlayerController.movementInput = axisValues;

            // Poll SAE.ArcadeMachine for YELLOW players' button 0 (True if held down)
            if (SAE.ArcadeMachine.PlayerPressingButtonStatic(ArcadeMachine.PlayerColorId.YELLOW_PLAYER, 0) == true)
            { Debug.Log("YELLOW player pressed button 0 (Held down)"); }

            // Poll SAE.ArcadeMachine for YELLOW players' button 1 (True if pressed once .. eg. not held down)
            if (SAE.ArcadeMachine.PlayerPressingButtonStatic(ArcadeMachine.PlayerColorId.YELLOW_PLAYER, 1, true) == true)
            { Debug.Log("YELLOW player pressed button 1 (Not held down)"); }

            

        }
    }
}
