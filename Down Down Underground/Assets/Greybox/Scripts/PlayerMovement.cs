using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE
{
    public class PlayerMovement : MonoBehaviour
    {

        public GameObject mineCircle;
        public float mineCircleOffSet = 0.65f;
        public float speed;
        float horizontalMove = 0f;
        float verticalMove = 0f;

        bool currentlyMovingVert = false;
        bool currentlyMovingHori = false;

        public enum miningDirection
        {
            NONE, UP, DOWN, LEFT, RIGHT
        }
        miningDirection mDirection;

        private void Start()
        {
            mDirection = miningDirection.NONE;
        }
        void Update()
        {
            //Right and Left Input for Keyboard
            if (Input.GetKey(KeyCode.D))
            {
                horizontalMove = speed; //Sets speed
                currentlyMovingVert = true; //Indicates current movement
                                            //direction = 0f; //Sets direction to the right
                mDirection = miningDirection.RIGHT;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                horizontalMove = -speed; //Sets speed
                currentlyMovingVert = true; //Indicates current movement
                                            //direction = 2f; //Sets direction to the left
                mDirection = miningDirection.LEFT;
            }
            else
            {
                horizontalMove = 0f; //Sets speed
                currentlyMovingVert = false; //Indicates current movement
            }
            //Up and Down Input for Keyboard
            if (Input.GetKey(KeyCode.W))
            {
                verticalMove = speed; //Sets speed
                currentlyMovingHori = true; //Indicates current movement
                                            //direction = 3f; //Sets direction to up
                mDirection = miningDirection.UP;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                verticalMove = -speed; //Sets speed
                currentlyMovingHori = true; //Indicates current movement
                                            //direction = 1f; //Sets direction to down
                mDirection = miningDirection.DOWN;
            }
            else
            {
                verticalMove = 0f; //Sets speed
                currentlyMovingHori = false; //Indicates current movement
            }

            //Activates or Deactivates the mining circle based on whether the player is moving or not
            if (mineCircle.activeInHierarchy == true)
            {
                if (currentlyMovingHori == false && currentlyMovingVert == false)
                {
                    mineCircle.SetActive(false);
                }
            }
            else
            {
                if (currentlyMovingHori == true)
                {
                    mineCircle.SetActive(true);
                }
                if (currentlyMovingVert == true)
                {
                    mineCircle.SetActive(true);
                }
            }

            switch (mDirection)
            {
                case miningDirection.NONE:
                    break;
                case miningDirection.UP:
                    Debug.Log("UP");
                    mineCircle.transform.position = transform.position + new Vector3(0f, mineCircleOffSet, 0f);
                    break;
                case miningDirection.DOWN:
                    Debug.Log("DOWN");
                    mineCircle.transform.position = transform.position + new Vector3(0f, -mineCircleOffSet, 0f);
                    break;
                case miningDirection.LEFT:
                    Debug.Log("LEFT");
                    mineCircle.transform.position = transform.position + new Vector3(-mineCircleOffSet, 0f, 0f); //left
                    break;
                case miningDirection.RIGHT:
                    Debug.Log("RIGHT");
                    mineCircle.transform.position = transform.position + new Vector3(mineCircleOffSet, 0f, 0f); //right
                    break;
            }

        }

        private void FixedUpdate()
        {
            //Moves the player if the speed isn't at 0
            if (horizontalMove != 0f)
            {
                transform.position += transform.TransformDirection(Vector3.right) * Time.fixedDeltaTime * horizontalMove;
            }
            if (verticalMove != 0f)
            {
                transform.position += transform.TransformDirection(Vector3.up) * Time.fixedDeltaTime * verticalMove;
            }
        }
    }
}
