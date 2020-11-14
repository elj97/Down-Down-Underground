using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testWeapon : MonoBehaviour
{
    public GameObject laser;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ( Input.GetAxis("Fire") == 1 )
        {
            laser.SetActive(true);
            Fire();
        }
        else if ( Input.GetAxis("Fire") == 0 )
        {
            laser.SetActive(false);
        }
    }

    void Fire()
    {
        // can add reload delay for fire - no button mashing
        RaycastHit hit;
        
        if (Physics.Raycast(laser.transform.position, laser.transform.forward, out hit))
        {
            if (hit.collider.tag == "Enemy")
            {
                print("Hit : " + hit.collider.gameObject.name);
            }
        }  
    }

}
