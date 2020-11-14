using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FireTester : MonoBehaviour
{
    public GameObject laser;

    PS4Controls controls;

    private void Awake()
    {
        controls = new PS4Controls();
        controls.Enable();

        controls.Player.Fire.performed += ctx => Fire();
    }

    void Fire()
    {
        // can add reload delay for fire - no button mashing
        RaycastHit hit;

        if ( Physics.Raycast(laser.transform.position, laser.transform.forward, out hit) )
        {
            if ( hit.collider.tag == "Enemy" )
            {
                print("Hit : " + hit.collider.gameObject.name);
            }
        }
    }

    private void OnEnable()
    {
        controls.Player.Fire.performed += Fire_performed;
        controls.Player.Fire.Enable();

        laser.SetActive(true);
    }

    private void OnDisable()
    {
        controls.Player.Fire.performed -= Fire_performed;
        controls.Player.Fire.Disable();

        laser.SetActive(false);
    }

    private void Fire_performed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Debug.Log("Fire");
    }
}
