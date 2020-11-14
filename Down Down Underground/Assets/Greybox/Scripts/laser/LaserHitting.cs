using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHitting : MonoBehaviour
{
    public float secondsToDestroy = 1f;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject, secondsToDestroy);
        }

    }

}
