using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHit : MonoBehaviour
{
    // time it takes to destroy enemy
    private bool destroyEnemy;
    public float enemyTimeDur = 1f; //How many seconds enemy hitbox
    public float enemyTime = 0f; //The enemy timer

    private GameObject g;

    void Update()
    {
        // time it takes to destroy enemy
        if (destroyEnemy == false)
        {
            if (enemyTime > 0)
            {
                enemyTime -= Time.deltaTime;
            }
            else
            {
                enemyTime = enemyTimeDur;
                destroyEnemy = true;
            }
        }
        /*if (enemyTime < 0)
        {
            enemyTime = enemyTimeDur;
        }*/
    }


    // destroy when timer is up
    private void OnTriggerStay(Collider other)
    {
        
            print("Entered a hitbox");
            if (other.gameObject.CompareTag("Enemy"))
            {

                if (destroyEnemy == true)
                {
                        enemyTime = enemyTimeDur;
                        Destroy(other.gameObject);

                }
                if (enemyTime < 0)
                {
                    Destroy(other.gameObject);
                    // enemyTime = enemyTimeDur;
                    destroyEnemy = true;
                }

                print("Entered enemy hitbox");
            }
        
    }

    // reset the timer when new enemy enter
    private void OnTriggerEnter(Collider other)
    {
        print("Touched Trigger");
        if (other.gameObject.CompareTag("Enemy"))
        {
            destroyEnemy = false;
            //enemyTime = enemyTimeDur;
        }
    }

}