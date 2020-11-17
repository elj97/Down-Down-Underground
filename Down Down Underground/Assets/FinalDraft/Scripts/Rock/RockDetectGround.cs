using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockDetectGround : MonoBehaviour
{
    public bool noGround;

    bool triggered = false;
    Collider currentGround;

    private void Update()
    {
        if (triggered && !currentGround)
        {
            noGround = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ground") || other.CompareTag("Pipe"))
        {
            noGround = false;
            triggered = true;
            currentGround = other.gameObject.GetComponent<Collider>();
        }
    }
}
