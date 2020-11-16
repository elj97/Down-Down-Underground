using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{

    public int pointWorth;
    public float deathTime = 0.01f;
    public bool retrievedPoints = false;
    public float lastingTime;

    private void Start()
    {
        Destroy(this.gameObject, lastingTime);
    }

}
