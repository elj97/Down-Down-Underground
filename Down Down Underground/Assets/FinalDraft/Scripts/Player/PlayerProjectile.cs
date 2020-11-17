using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{

    float projectileSpeed;
    Vector3 direction;
    float lifetime;


    void Start()
    {
        projectileSpeed = this.GetComponentInParent<ShootScript>().projectileSpeed;
        lifetime = this.GetComponentInParent<ShootScript>().projectileLifetime;
        this.transform.parent = null;
        Destroy(this.gameObject, lifetime);
    }

    void Update()
    {
        transform.position += transform.TransformDirection(Vector3.forward) * Time.fixedDeltaTime * projectileSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(this.gameObject, 0.01f);
        }
        else if (other.CompareTag("Ground"))
        {
            Destroy(this.gameObject, 0.01f);
        }
    }
}
