using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testControls : MonoBehaviour
{
    public int speed;
    public float friction;
    public float lerpSpeed;

    private float xDeg;
    private float yDeg;
    private Quaternion fromRotation;
    private Quaternion toRotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            xDeg -= Input.GetAxis("Mouse X") * speed * friction;
            //yDeg += Input.GetAxis("Mouse Y") * speed * friction;

            fromRotation = transform.rotation;
            toRotation = Quaternion.Euler(0, xDeg, 0);

            transform.rotation = Quaternion.Lerp(fromRotation, toRotation, Time.deltaTime * lerpSpeed);
        }
    }
}

// var touch = Input.GetTouch(0);
// var deltaX = touch.deltaPosition.x;
