using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FireTester : MonoBehaviour
{
    public GameObject laser;

    PS4Controls controls;

    private LineRenderer lr;

    private void Awake()
    {
        controls = new PS4Controls();
    }

    void Fire()
    {
        //// can add reload delay for fire - no button mashing
        //RaycastHit hit;

        //if ( Physics.Raycast(laser.transform.position, laser.transform.forward, out hit) )
        //{
        //    if ( hit.collider.tag == "Enemy" )
        //    {
        //        print("Hit : " + hit.collider.gameObject.name);
        //    }
        //}

        RaycastHit hit;

        if ( Physics.Raycast(transform.position, transform.forward, out hit) )
        {
            if ( hit.collider )
            {
                lr.SetPosition(1, new Vector3(0, 0, hit.distance));
            }
            else
            {
                lr.SetPosition(1, new Vector3(0, 0, 5000));
            }
        }
    }

    private void OnEnable()
    {
        controls.Player.Fire.performed += Fire_performed;
        controls.Enable();

        //laser.SetActive(true);
    }

    private void OnDisable()
    {
        controls.Player.Fire.performed -= Fire_performed;
        controls.Enable();

        //laser.SetActive(false);
    }

    private void Fire_performed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Debug.Log("Fire");
    }

    private void Update()
    {
        controls.Player.Fire.performed += ctx => Fire();
    }
}
