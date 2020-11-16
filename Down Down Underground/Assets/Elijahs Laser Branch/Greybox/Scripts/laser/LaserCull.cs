using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCull : MonoBehaviour
{
    private LineRenderer lr;
    public float maxDistance = 10f;
    public RaycastHit rayCastHitObject;
    public LayerMask layerMask;
    
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Fire();
        }
    }

    void Fire()
    {
        lr.SetPosition(0, transform.position);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance, layerMask))
        {
            if (hit.collider)
            {
                lr.SetPosition(1, hit.point);
            }
        }
        else lr.SetPosition(1, transform.forward * maxDistance);
    }
}

