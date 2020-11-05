using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject laser;

    public float laserTimeDur = 1f; //How many seconds laser is activated
    public float laserTime = 0f; //The timer
    private bool laserCancelled = false;
    
    // time it takes to destroy enemy
    private bool destroyEnemy;
    public float enemyTimeDur = 1f; //How many seconds enemy hitbox
    public float enemyTime = 0f; //The enemy timer

    private void Awake()
    {
        laserTime = laserTimeDur;
    }

    void Update()
    {
       if (laserCancelled == false)
        {
            // Key to activate laser
            if (Input.GetKeyDown(KeyCode.Space))
            {
                laser.SetActive(true);
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                laser.SetActive(false);
                laserTime = laserTimeDur;
            }
        }

        // laser timer
        if (laserTime > 0)
        {
            laserTime -= Time.deltaTime;
        }
        if (laserTime < 0)
        {
            laserCancelled = true;
            laserTime = laserTimeDur;
        }

        if (laserCancelled == true)
        {
            laser.SetActive(false);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            laserCancelled = false;
        }

        // time it takes to destroy enemy
        if (enemyTime > 0)
        {
            enemyTime -= Time.deltaTime;
        }
        /*if (enemyTime < 0)
        {
            enemyTime = enemyTimeDur;
        }*/
    }
    
    // destroy when timer is up
    private void OnTriggerStay(Collider other)
    {
        print("Enter enemy hitbox");
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (destroyEnemy == true)
            {
                Destroy(other.gameObject);
            }
        }
    }

    // reset the timer when new enemy enter
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyTime = enemyTimeDur;
        }
    }

}
