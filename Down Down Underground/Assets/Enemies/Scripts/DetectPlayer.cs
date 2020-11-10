using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{

    public GenericEnemy thisEnemyScript;
    public GameObject followedPlayer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            thisEnemyScript.playerInRange = true;
            followedPlayer = other.gameObject;
        }
    }
}
