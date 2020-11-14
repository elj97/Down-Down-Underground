using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{

    [Header("Stats")]
    public float rateOfFire = 1f;
    int amountProjectiles = 4;
    float projectileRotation = 0f;

    [Header("Prefabs")]
    public GameObject projectile;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        for(int i = 0; i < amountProjectiles; i++)
        {
            Instantiate(projectile, this.transform.position, Quaternion.Euler(0f, projectileRotation, 0f), this.transform);
            projectileRotation += 90;
        }
    }
}
