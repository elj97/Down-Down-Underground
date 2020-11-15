using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SAE
{
    public class PlayerController : MonoBehaviour
    {
        //Decide Player
        public enum WhichPlayer
        {
            YellowPlayer, BluePlayer, RedPlayer, GreenPlayer
        }
        [Header("Select Player")]
        public WhichPlayer whichPlayer;

        //MineCircle Variables
        enum miningDirection
        {
            NONE, UP, DOWN, LEFT, RIGHT
        }
        [Header("Mine Circle Variables")]
        public GameObject mineCircle;
        public float mineCircleOffSet = 0.65f;
        miningDirection mDirection;

        void Update()
        {
            //Figures out which axis/controller it should be set to based on what is selected in the inspector
            if (whichPlayer == WhichPlayer.YellowPlayer)
            {
                Vector2 axisValues = SAE.ArcadeMachine.PlayerJoystickAxisStatic(ArcadeMachine.PlayerColorId.YELLOW_PLAYER);
                if (axisValues.y < 0)
                {
                    //up
                    
                    mDirection = miningDirection.UP;
                }
                if (axisValues.y > 0)
                {
                    //down
                    
                    mDirection = miningDirection.DOWN;
                }
                if (axisValues.x > 0)
                {
                    //right
                    
                    mDirection = miningDirection.RIGHT;
                }
                if (axisValues.x < 0)
                {
                    //left
                    
                    mDirection = miningDirection.LEFT;
                }

                if (axisValues.x != 0)
                {
                    mineCircle.SetActive(true);
                }
                else if (axisValues.y != 0)
                {
                    mineCircle.SetActive(true);
                }
                else
                {
                    mineCircle.SetActive(false);
                }

            }
            else if (whichPlayer == WhichPlayer.BluePlayer)
            {
                Vector2 axisValues = SAE.ArcadeMachine.PlayerJoystickAxisStatic(ArcadeMachine.PlayerColorId.BLUE_PLAYER);
                if (axisValues.y < 0)
                {
                    //up
                    
                    mDirection = miningDirection.UP;
                }
                if (axisValues.y > 0)
                {
                    //down
                    
                    mDirection = miningDirection.DOWN;
                }
                if (axisValues.x > 0)
                {
                    //right
                    
                    mDirection = miningDirection.RIGHT;
                }
                if (axisValues.x < 0)
                {
                    //left
                    
                    mDirection = miningDirection.LEFT;
                }

                if (axisValues.x != 0)
                {
                    mineCircle.SetActive(true);
                }
                else if (axisValues.y != 0)
                {
                    mineCircle.SetActive(true);
                }
                else
                {
                    mineCircle.SetActive(false);
                }
            }
            else if (whichPlayer == WhichPlayer.RedPlayer)
            {
                Vector2 axisValues = SAE.ArcadeMachine.PlayerJoystickAxisStatic(ArcadeMachine.PlayerColorId.RED_PLAYER);
                if (axisValues.y < 0)
                {
                    //up
                    
                    mDirection = miningDirection.UP;
                }
                if (axisValues.y > 0)
                {
                    //down
                    
                    mDirection = miningDirection.DOWN;
                }
                if (axisValues.x > 0)
                {
                    //right
                    
                    mDirection = miningDirection.RIGHT;
                }
                if (axisValues.x < 0)
                {
                    //left
                    
                    mDirection = miningDirection.LEFT;
                }

                if (axisValues.x != 0)
                {
                    mineCircle.SetActive(true);
                }
                else if (axisValues.y != 0)
                {
                    mineCircle.SetActive(true);
                }
                else
                {
                    mineCircle.SetActive(false);
                }
            }
            else if (whichPlayer == WhichPlayer.GreenPlayer)
            {
                Vector2 axisValues = SAE.ArcadeMachine.PlayerJoystickAxisStatic(ArcadeMachine.PlayerColorId.GREEN_PLAYER);
                if (axisValues.y < 0)
                {
                    //up
                    
                    mDirection = miningDirection.UP;
                }
                if (axisValues.y > 0)
                {
                    //down
                    
                    mDirection = miningDirection.DOWN;
                }
                if (axisValues.x > 0)
                {
                    //right
                    
                    mDirection = miningDirection.RIGHT;
                }
                if (axisValues.x < 0)
                {
                    //left
                    
                    mDirection = miningDirection.LEFT;
                }

                if (axisValues.x != 0)
                {
                    mineCircle.SetActive(true);
                }
                else if (axisValues.y != 0)
                {
                    mineCircle.SetActive(true);
                }
                else
                {
                    mineCircle.SetActive(false);
                }
            }
            else
            {
                print("Something has gone wrong, a player should be assigned but somehow they haven't been");
            }

            //Sets the axis values
            //float inputY = axisValues.y;
            //float inputX = axisValues.x;

            //print("the y value is " + axisValues.y);
            //print("the x value is " + axisValues.x);


            //Determines where the mining circle should be based on the direction of the axis values
            /*if (axisValues.y < 0)
            {
                //up
                
                mDirection = miningDirection.UP;
            }
            if (axisValues.y > 0)
            {
                //down
                
                mDirection = miningDirection.DOWN;
            }
            if (axisValues.x > 0)
            {
                //right
                
                mDirection = miningDirection.RIGHT;
            }
            if (axisValues.x < 0)
            {
                //left
                
                mDirection = miningDirection.LEFT;
            }*/

            //Moves the mining circle to face the direction of the axis values
            switch (mDirection)
            {
                case miningDirection.NONE:
                    break;
                case miningDirection.UP:
                    //Debug.Log("UP");
                    mineCircle.transform.position = transform.position + new Vector3(0f, mineCircleOffSet, 0f);
                    break;
                case miningDirection.DOWN:
                    //Debug.Log("DOWN");
                    mineCircle.transform.position = transform.position + new Vector3(0f, -mineCircleOffSet, 0f);
                    break;
                case miningDirection.LEFT:
                    //Debug.Log("LEFT");
                    mineCircle.transform.position = transform.position + new Vector3(-mineCircleOffSet, 0f, 0f); //left
                    break;
                case miningDirection.RIGHT:
                    //Debug.Log("RIGHT");
                    mineCircle.transform.position = transform.position + new Vector3(mineCircleOffSet, 0f, 0f); //right
                    break;
            }

        }
    }
}