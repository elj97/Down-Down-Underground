using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanPath : MonoBehaviour
{

    public float waitTime = 1f;

    public AstarPath astarPathScript;

    IEnumerator Reset(float Count)
    {
        yield return new WaitForSeconds(Count); //Count is the amount of time in seconds that you want to wait.
        ScanPathing();                                        //And here goes your method of resetting the game...
        yield return null;
    }

    public void InitiateCoroutine()
    {
        StartCoroutine("Reset", waitTime);
    }

    public void ScanPathing()
    {
        astarPathScript.Scan();
    }
}
