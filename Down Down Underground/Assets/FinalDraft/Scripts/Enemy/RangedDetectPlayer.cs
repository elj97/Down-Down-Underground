using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedDetectPlayer : MonoBehaviour
{
    public bool detectPlayer = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            detectPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            detectPlayer = false;
        }
    }

}
