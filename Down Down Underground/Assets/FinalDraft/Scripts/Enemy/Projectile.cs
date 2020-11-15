using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    float projectileSpeed;
    Vector3 direction;
    float lifetime;
    

    void Start()
    {
        projectileSpeed = this.GetComponentInParent<RangedEnemy>().projectileSpeed;
        lifetime = this.GetComponentInParent<RangedEnemy>().projectileLifetime;
        Destroy(this.gameObject, lifetime);
    }

    void Update()
    {
        transform.position += transform.TransformDirection(Vector3.forward) * Time.fixedDeltaTime * projectileSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        } else if (other.CompareTag("Ground"))
        {
            Destroy(this.gameObject);
        }
    }

}
