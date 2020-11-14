using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputTester : MonoBehaviour
{
    private PS4Controls controls;
    [SerializeField] private float speed = 3f;
    private Vector2 moveAxis;

    private void Awake()
    {
        controls = new PS4Controls();
        controls.Player.Move.Enable();

        controls.Player.Move.performed += HandleMove;
        controls.Player.Move.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(moveAxis.x * Time.deltaTime * speed, 0, moveAxis.y * Time.deltaTime * speed);
    }

    private void HandleMove(InputAction.CallbackContext context)
    {
        moveAxis = context.ReadValue<Vector2>();
        Debug.Log($"Move Axis {moveAxis}");
    }
}
