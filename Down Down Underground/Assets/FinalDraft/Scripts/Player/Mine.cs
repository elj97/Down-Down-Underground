using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{

    public float miningTimeDur = 0.4f; //How many seconds it takes to mine a block
    public float miningTime = 0f; //The timer for mining blocks
    bool mined = false;
    public GameObject targetTile;
    public bool diggingRock = false; //set private once done testing

    public ScanPath scanPath;

    private void Awake()
    {
        miningTime = miningTimeDur;
        mined = false;
    }

    void Update()
    {
        if (miningTime > 0)
        {
            miningTime -= Time.deltaTime;
            mined = false;
        }
        if (miningTime < 0)
        {
            mined = true;
            miningTime = miningTimeDur;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //print("Enter trigger");
        //if (other.gameObject.CompareTag("Rock"))
        //{
        //    diggingRock = true;
        //}
        //else if (other.gameObject.CompareTag("Rock") == false)
        //{
        //    diggingRock = false;
        //}

        if (diggingRock == false)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                SoundManager.PlaySound("digSound");

                if (other.gameObject != targetTile)
                {
                    miningTime = miningTimeDur;
                    targetTile = other.gameObject;
                }

                if (mined == true)
                {
                    Destroy(other.gameObject);
                    targetTile = null;
                    scanPath.InitiateCoroutine();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Rock"))
        {
            diggingRock = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Rock"))
        {
            diggingRock = false;
        }
    }
}
