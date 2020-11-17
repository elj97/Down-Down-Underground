using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootScript : MonoBehaviour
{
    [Header("Stats")]
    public float fireRate = 1f;
    public float projectileSpeed;
    public float projectileLifetime;

    public float projectileRotation;
    float nextTimeToFire = 0f;

    [Header("Prefabs")]
    public GameObject projectile;

    void Update()
    {
        
    }

    public void Shoot()
    {
        if (Time.time >= nextTimeToFire)
        {
            print("shooting in the projectile thing");
            Instantiate(projectile, this.transform.position, Quaternion.Euler(projectileRotation, 90f, 90f), this.transform);
            nextTimeToFire = Time.time + 1f / fireRate;
        }
    }
}
