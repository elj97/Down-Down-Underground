using UnityEngine;

/*
    Script: GameManager_Test1
    Author: Gareth Lockett
    Version: 1.0
    Description:    Super simple example moving player cubes around using input from SAE arcade machine.
                    Make sure the SAE.ArcadeMachine component/prefab is in the scene and calls ConfigurePlayer() (Eg use WITHOUT events for this example)
*/

namespace SAE
{
    public class GameManager_Test1 : MonoBehaviour
    {
        // Properties
        public Rigidbody yellowPlayer;          // Reference to yellow players' Rigidbody component.
        public Rigidbody bluePlayer;            // Reference to blue players' Rigidbody component.
        public Rigidbody redPlayer;             // Reference to red players' Rigidbody component.
        public Rigidbody greenPlayer;           // Reference to green players' Rigidbody component.

        public float moveSpeed = 5f;            // Velocity speed of moving players.
        public Vector2 wrapSize;                // When a players position goes outside these (width/height) values, teleport to other side.

        // Methods
        private void Update()
        {
            // Poll SAE.ArcadeMachine for players' joystick axis and use to set Rigidbody velocity.
            Vector2 axisValues = SAE.ArcadeMachine.PlayerJoystickAxisStatic( ArcadeMachine.PlayerColorId.YELLOW_PLAYER );
            this.yellowPlayer.velocity = new Vector3( axisValues.x, 0f, -axisValues.y ) * this.moveSpeed;

            axisValues = SAE.ArcadeMachine.PlayerJoystickAxisStatic( ArcadeMachine.PlayerColorId.BLUE_PLAYER );
            this.bluePlayer.velocity = new Vector3( axisValues.x, 0f, -axisValues.y ) * this.moveSpeed;

            axisValues = SAE.ArcadeMachine.PlayerJoystickAxisStatic( ArcadeMachine.PlayerColorId.RED_PLAYER );
            this.redPlayer.velocity = new Vector3( axisValues.x, 0f, -axisValues.y ) * this.moveSpeed;

            axisValues = SAE.ArcadeMachine.PlayerJoystickAxisStatic( ArcadeMachine.PlayerColorId.GREEN_PLAYER );
            this.greenPlayer.velocity = new Vector3( axisValues.x, 0f, -axisValues.y ) * this.moveSpeed;

            // Poll SAE.ArcadeMachine for YELLOW players' button 0 (True if held down)
            if( SAE.ArcadeMachine.PlayerPressingButtonStatic( ArcadeMachine.PlayerColorId.YELLOW_PLAYER, 0 ) == true )
            { Debug.Log( "YELLOW player pressed button 0 (Held down)" ); }

            // Poll SAE.ArcadeMachine for YELLOW players' button 1 (True if pressed once .. eg. not held down)
            if( SAE.ArcadeMachine.PlayerPressingButtonStatic( ArcadeMachine.PlayerColorId.YELLOW_PLAYER, 1, true ) == true )
            { Debug.Log( "YELLOW player pressed button 1 (Not held down)" ); }



            // Check for offscreen loop.
            Transform[] allPlayerTransforms = { this.yellowPlayer.transform, this.bluePlayer.transform, this.redPlayer.transform, this.greenPlayer.transform };
            foreach( Transform playerTransform in allPlayerTransforms )
            {
                Vector3 tmpPosition = playerTransform.position;

                // Check left/right sides.
                if( tmpPosition.x < -this.wrapSize.x ) { tmpPosition.x = this.wrapSize.x +( tmpPosition.x + this.wrapSize.x ); }
                else if( tmpPosition.x > this.wrapSize.x ) { tmpPosition.x = -this.wrapSize.x +( tmpPosition.x - this.wrapSize.x ); }

                // Check top/bottom sides.
                if( tmpPosition.z < -this.wrapSize.y ) { tmpPosition.z = this.wrapSize.y +( tmpPosition.z + this.wrapSize.y ); }
                else if( tmpPosition.z > this.wrapSize.y ) { tmpPosition.z = -this.wrapSize.y +( tmpPosition.z - this.wrapSize.y ); }

                playerTransform.position = tmpPosition;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube( Vector3.zero, new Vector3( this.wrapSize.x, 0f, this.wrapSize.y ) * 2f );
        }
    }
}
