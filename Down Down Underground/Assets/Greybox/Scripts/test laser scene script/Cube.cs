using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cube : MonoBehaviour
{
    //instance
    PS4Controls controls;

    Vector2 move;
    Vector2 rotate;

    // called before start
    private void Awake() 
    {
        controls = new PS4Controls();

        // .start, .performed, .canceled
        // ctx is short for context
        // => means "go to"
        controls.Player.Grow.performed += ctx => Grow();

        controls.Player.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => move = Vector2.zero;

        controls.Player.Rotate.performed += ctx => rotate = ctx.ReadValue<Vector2>();
        controls.Player.Rotate.canceled += ctx => rotate = Vector2.zero;
    }

    void Grow()
    {
        transform.localScale *= 1.1f;
    }

    private void Update()
    {
        Vector2 m = new Vector2(move.x, -move.y) * Time.deltaTime;
        transform.Translate(m, Space.World);

        // made rotate.x/y negative if not turning the correct way
        Vector2 r = new Vector2(-rotate.y, rotate.x) * 100f * Time.deltaTime;
        transform.Rotate(r, Space.World);
    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }
}
