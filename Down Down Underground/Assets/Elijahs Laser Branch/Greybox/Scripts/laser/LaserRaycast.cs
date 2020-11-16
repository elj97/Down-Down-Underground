using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRaycast : MonoBehaviour
{
    public RaycastHit rayCastHitObject;
    public LayerMask layerMask;
    public float maxDistance = 10f;
    
    void Update()
    {
        //if (Physics.Raycast (transform.position, transform.forward, out rayCastHitObject, maxDistance, layerMask))
        //{
        //    Debug.Log(rayCastHitObject.transform.name);
        //}

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, maxDistance, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * maxDistance, Color.white);
            Debug.Log("Did not Hit");
        }
    }


}
