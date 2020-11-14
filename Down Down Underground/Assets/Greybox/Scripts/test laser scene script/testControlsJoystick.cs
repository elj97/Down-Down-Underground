using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testControlsJoystick : MonoBehaviour
{
    public float moveSpeed;

    //private Vector2 moveInput;

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("MoveHorizontal"), Input.GetAxis("MoveVertical"), 0.0f);

        transform.position = transform.position + movement * Time.deltaTime;

        //moveInput = new Vector2(Input.GetAxisRaw("MoveHorizontal"), Input.GetAxisRaw("MoveVertical"));
    }
}
