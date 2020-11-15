using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{

    RockDetectGround rockDetectGround;
    public float deathTime = 1f;
    public float fallSpeed = 1f;
    public float fallStartTime = 1f;
    public ScanPath scanPath;

    bool hasFallen = false;
    bool destroyedCurrentGround = false;
    bool scannedPath = false;

    IEnumerator Fall(float Count)
    {
        yield return new WaitForSeconds(Count);
        Fall();
        yield return null;
    }

    void Start()
    {
        rockDetectGround = this.GetComponentInChildren<RockDetectGround>();
    }
    
    void Update()
    {
        if (rockDetectGround.noGround == true)
        {
            StartCoroutine("Fall", fallStartTime);
            hasFallen = true;
        } else
        {
            if (hasFallen == true)
            {
                Destroy();
                fallSpeed = 0f;
            }
        }
    }

    void Fall()
    {
        transform.position += transform.TransformDirection(Vector3.down) * Time.fixedDeltaTime * fallSpeed;
    }

    void Destroy()
    {
        Destroy(this.gameObject, deathTime);
        if (scannedPath == false)
        {
            scanPath.InitiateCoroutine();
            scannedPath = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            if (hasFallen == true && destroyedCurrentGround == false)
            {
                Destroy(other.gameObject, deathTime);
                destroyedCurrentGround = true;
            }
        }
        if (other.gameObject.CompareTag("Player"))
        {
            //Kill
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            //Kill
        }
    }
}
