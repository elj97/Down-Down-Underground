using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{

    public GenericEnemy genericEnemyScript;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Check if players have lives left and then RespawnPlayer();
            genericEnemyScript.Death();
        }
    }
}
