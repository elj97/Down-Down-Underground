using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    //Player Stats
    [Header("Player Stats")]
    public float moveSpeed = 5f;
    private Vector2 movementInput;

    //MineCircle Variables
    enum miningDirection
    {
        NONE, UP, DOWN, LEFT, RIGHT
    }
    [Header("Mine Circle Variables")]
    public GameObject mineCircle;
    public float mineCircleOffSet = 0.65f;
    miningDirection mDirection;

    //GetComponent things (found out it is expensive in update)
    Rigidbody rigidBody;
    [Header("Rotation")]
    public Transform modelTransform;
    
    [Header("Misc")]
    public Highscore scoring;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Movement
        transform.Translate(new Vector3(movementInput.x, movementInput.y, 0f) * moveSpeed * Time.deltaTime);

        //print("X movement is: " + movementInput.x);
        //print("Y movement is: " + movementInput.y);

        //Decides the direction of the mining circle depending on the movement
        if (movementInput.x > 0.4 && movementInput.y > -0.4)
        {
            mineCircle.SetActive(true);
            mDirection = miningDirection.RIGHT;
            //Stop Sliding
            rigidBody.isKinematic = false;
        } else if (movementInput.x < -0.4 && movementInput.y < 0.4)
        {
            mineCircle.SetActive(true);
            mDirection = miningDirection.LEFT;
            //Stop Sliding
            rigidBody.isKinematic = false;
        } else if (movementInput.y > 0.4 && movementInput.x > -0.4)
        {
            mineCircle.SetActive(true);
            mDirection = miningDirection.UP;
            //Stop Sliding
            rigidBody.isKinematic = false;
        } else if (movementInput.y < -0.4 && movementInput.x < 0.4)
        {
            mineCircle.SetActive(true);
            mDirection = miningDirection.DOWN;
            //Stop Sliding
            rigidBody.isKinematic = false;
        } else
        {
            mineCircle.SetActive(false);
        }

        if (movementInput.y < 0.01 && movementInput.x < 0.01 && movementInput.y > -0.01 && movementInput.x > -0.01)
        {
            //Stop Sliding
            rigidBody.isKinematic = true;
        } else
        {
            rigidBody.isKinematic = false;
        }

        //Moves the mining circle to face the direction of the axis values
        switch (mDirection)
        {
            case miningDirection.NONE:
                break;
            case miningDirection.UP:
                //Debug.Log("UP");
                mineCircle.transform.position = transform.position + new Vector3(0f, mineCircleOffSet, 0f);
                modelTransform.rotation = Quaternion.Euler(-90f, 0f, 0f);
                break;
            case miningDirection.DOWN:
                //Debug.Log("DOWN");
                mineCircle.transform.position = transform.position + new Vector3(0f, -mineCircleOffSet, 0f);
                modelTransform.rotation = Quaternion.Euler(90f, 180f, 0f);
                break;
            case miningDirection.LEFT:
                //Debug.Log("LEFT");
                mineCircle.transform.position = transform.position + new Vector3(-mineCircleOffSet, 0f, 0f); //left
                modelTransform.rotation = Quaternion.Euler(0f, -90f, 90f);
                break;
            case miningDirection.RIGHT:
                //Debug.Log("RIGHT");
                mineCircle.transform.position = transform.position + new Vector3(mineCircleOffSet, 0f, 0f); //right
                modelTransform.rotation = Quaternion.Euler(0f, 90f, -90f);
                break;
        }

    }

    public void OnMove(InputAction.CallbackContext ctx) => movementInput = ctx.ReadValue<Vector2>();

    //Collecting Fruit
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fruit"))
        {
            
            if (other.gameObject.GetComponent<Fruit>().retrievedPoints == false)
            {
                scoring.IncreaseScore(other.gameObject.GetComponent<Fruit>().pointWorth);
                Destroy(other.gameObject, other.gameObject.GetComponent<Fruit>().deathTime);
                other.gameObject.GetComponent<Fruit>().retrievedPoints = true;
            }
        }
    }

}
