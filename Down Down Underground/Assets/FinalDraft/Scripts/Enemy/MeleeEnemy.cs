using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{

    public int pointWorth = 50;

    //GetComponent things (found out it is expensive in update)
    GenericEnemy genericEnemy;

    private void Start()
    {
        genericEnemy = this.GetComponent<GenericEnemy>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Check if players have lives left and then RespawnPlayer();
            genericEnemy.Death();
        }
    }
}
