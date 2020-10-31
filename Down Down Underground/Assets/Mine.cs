using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{

    public float miningTimeDur = 0.4f; //How many seconds it takes to mine a block
    float miningTime = 0f; //The timer for mining blocks
    bool mined = false;

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
        if (other.gameObject.CompareTag("Ground"))
        {
            if (mined == true)
            {
                Destroy(other.gameObject);
            }
        }
    }
}
