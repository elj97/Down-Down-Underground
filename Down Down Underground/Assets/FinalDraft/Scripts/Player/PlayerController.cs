using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SAE
{
    public class PlayerController : MonoBehaviour
    {
        #region Variables
        //Player Stats
        [Header("Player Stats")]
        public float moveSpeed = 5f;
        public float movementInputX;
        public float movementInputY;

        //MineCircle Variables
        enum miningDirection
        {
            NONE, UP, DOWN, LEFT, RIGHT
        }
        [Header("Mine Circle Variables")]
        public GameObject mineCircle;
        public float mineCircleOffSet = 0.65f;
        miningDirection mDirection;
        
        public enum Player
        {
            NONE, YELLOW, BLUE, RED, GREEN
        }
        [Header("Player Select")]
        public Player playerSelect;

        //GetComponent things (found out it is expensive in update)
        Rigidbody rigidBody;
        ShootScript shootScript;

        [Header("Rotation")]
        public Transform modelTransform;

        [Header("Misc")]
        public Highscore scoring;
        public Respawn respawnScript;
        public PlayerLives playerLivesScript;
        Mine mineScript;
        #endregion

        private void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
            shootScript = GetComponent<ShootScript>();
            mineScript = mineCircle.GetComponent<Mine>();
        }

        void Update()
        {

            #region Direction and Drift Prevention
            //Decides the direction of the mining circle depending on the movement
            if (movementInputX > 0.4 && movementInputY > -0.4)
            {
                mineCircle.SetActive(true);
                mDirection = miningDirection.RIGHT;
                //Stop Sliding
                rigidBody.isKinematic = false;
            }
            else if (movementInputX < -0.4 && movementInputY < 0.4)
            {
                mineCircle.SetActive(true);
                mDirection = miningDirection.LEFT;
                //Stop Sliding
                rigidBody.isKinematic = false;
            }
            else if (movementInputY > 0.4 && movementInputX > -0.4)
            {
                mineCircle.SetActive(true);
                mDirection = miningDirection.DOWN;
                //Stop Sliding
                rigidBody.isKinematic = false;
            }
            else if (movementInputY < -0.4 && movementInputX < 0.4)
            {
                mineCircle.SetActive(true);
                mDirection = miningDirection.UP;
                //Stop Sliding
                rigidBody.isKinematic = false;
            }
            else
            {
                mineScript.diggingRock = false;
                mineCircle.SetActive(false);
            }

            if (movementInputY < 0.01 && movementInputX < 0.01 && movementInputY > -0.01 && movementInputX > -0.01)
            {
                //Stop Sliding
                rigidBody.isKinematic = true;
            }
            else
            {
                rigidBody.isKinematic = false;
            }
            #endregion
            
            #region Switch Case for Direction
            //Moves the mining circle to face the direction of the axis values
            switch (mDirection)
            {
                case miningDirection.NONE:
                    break;
                case miningDirection.UP:
                    //Debug.Log("UP");
                    mineCircle.transform.position = transform.position + new Vector3(0f, mineCircleOffSet, 0f);
                    modelTransform.rotation = Quaternion.Euler(-90f, 0f, 0f);
                    shootScript.projectileRotation = -90f;

                    break;
                case miningDirection.DOWN:
                    //Debug.Log("DOWN");
                    mineCircle.transform.position = transform.position + new Vector3(0f, -mineCircleOffSet, 0f);
                    modelTransform.rotation = Quaternion.Euler(90f, 180f, 0f);
                    shootScript.projectileRotation = 90f;
                    break;
                case miningDirection.LEFT:
                    //Debug.Log("LEFT");
                    mineCircle.transform.position = transform.position + new Vector3(-mineCircleOffSet, 0f, 0f); //left
                    modelTransform.rotation = Quaternion.Euler(0f, -90f, 90f);
                    shootScript.projectileRotation = 180f;
                    break;
                case miningDirection.RIGHT:
                    //Debug.Log("RIGHT");
                    mineCircle.transform.position = transform.position + new Vector3(mineCircleOffSet, 0f, 0f); //right
                    modelTransform.rotation = Quaternion.Euler(0f, 90f, -90f);
                    shootScript.projectileRotation = 0f;
                    break;
            }
            #endregion

            #region Shooting
            if (playerSelect == Player.YELLOW)
            {
                if (SAE.ArcadeMachine.input.PlayerPressingButton(SAE.ArcadeMachine.PlayerColorId.YELLOW_PLAYER, 8) == true)
                {
                    Fire();
                }
            }
            if (playerSelect == Player.BLUE)
            {
                if (SAE.ArcadeMachine.input.PlayerPressingButton(SAE.ArcadeMachine.PlayerColorId.BLUE_PLAYER, 8) == true)
                {
                    Fire();
                }
            }
            if (playerSelect == Player.RED)
            {
                if (SAE.ArcadeMachine.input.PlayerPressingButton(SAE.ArcadeMachine.PlayerColorId.RED_PLAYER, 8) == true)
                {
                    Fire();
                }
            }
            if (playerSelect == Player.GREEN)
            {
                if (SAE.ArcadeMachine.input.PlayerPressingButton(SAE.ArcadeMachine.PlayerColorId.GREEN_PLAYER, 8) == true)
                {
                    Fire();
                }
            }
            #endregion

        }
        

        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Fruit"))
            {

                if (other.gameObject.GetComponent<Fruit>().retrievedPoints == false)
                {
                    SoundManager.PlaySound("fruitPickup");
                    scoring.IncreaseScore(other.gameObject.GetComponent<Fruit>().pointWorth);
                    Destroy(other.gameObject, other.gameObject.GetComponent<Fruit>().deathTime);
                    other.gameObject.GetComponent<Fruit>().retrievedPoints = true;
                }
            }

            if (other.gameObject.CompareTag("Enemy"))
            {
                Death();
            }
            if (other.gameObject.CompareTag("Projectile"))
            {
                Death();
            }
            if (other.gameObject.CompareTag("Death"))
            {
                Death();
            }
        }

        void Death()
        {
            if (playerLivesScript.life > 0)
            {
                playerLivesScript.life--;
                if (playerSelect == Player.YELLOW)
                {
                    respawnScript.YellowPlayerDie();
                }
                if (playerSelect == Player.BLUE)
                {
                    respawnScript.BluePlayerDie();
                }
                if (playerSelect == Player.RED)
                {
                    respawnScript.RedPlayerDie();
                }
                if (playerSelect == Player.GREEN)
                {
                    respawnScript.GreenPlayerDie();
                }
            } else
            {
                this.gameObject.SetActive(false);
            }
        }

        void Fire()
        {
            shootScript.Shoot();
        }

    }
}
