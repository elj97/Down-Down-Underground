using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRotation : MonoBehaviour
{

    Transform parentTransform;
    public float customValue;

    void Start()
    {
        parentTransform = this.GetComponentInParent<Transform>();
    }

    void Update()
    {
        //print("x value is " + Mathf.RoundToInt(parentTransform.rotation.eulerAngles.x));
        //print("y value is " + Mathf.RoundToInt(parentTransform.rotation.eulerAngles.y));
        //print("z value is " + Mathf.RoundToInt(parentTransform.rotation.eulerAngles.z));

        //Up
        if (MathExtension.IsBetweenRange(parentTransform.rotation.eulerAngles.x, 225, 315))
        {
            print("up");
            //this.gameObject.transform.rotation = Quaternion.Euler(270f, 0f, 0f);
        }

        //Down
        if (MathExtension.IsBetweenRange(parentTransform.rotation.eulerAngles.x, 45, 135))
        {
            print("down");
            //this.gameObject.transform.rotation = Quaternion.Euler(90f, 180f, 0f);
        }

        //Right and Left
        if (MathExtension.IsBetweenRange(parentTransform.rotation.eulerAngles.x, -45, 45) || MathExtension.IsBetweenRange(parentTransform.rotation.eulerAngles.x, 315, 405))
        {
            //Right
            if (MathExtension.IsBetweenRange(parentTransform.rotation.eulerAngles.y, 45, 135))
            {
                print("right");
                //this.gameObject.transform.rotation = Quaternion.Euler(0f, 90f, 270f);
            }
            //Left
            if (MathExtension.IsBetweenRange(parentTransform.rotation.eulerAngles.y, 225, 315))
            {
                print("left");
                //this.gameObject.transform.rotation = Quaternion.Euler(0f, 90f, 270f);
            }

        }

    }

}
