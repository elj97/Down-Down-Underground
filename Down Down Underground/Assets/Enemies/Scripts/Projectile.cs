using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public GameObject thisProjectile;
    float projectileSpeed;
    

    void Start()
    {
        //projectileSpeed = gameObject.GetComponentInParent<RangedEnemy>().projectileSpeed;
        this.transform.parent = null;
    }
    
    void Update()
    {
        this.thisProjectile.GetComponent<Rigidbody>().velocity = new Vector3(this.transform.rotation.eulerAngles.x, this.transform.rotation.eulerAngles.y, 0f) * projectileSpeed;

    }
}
