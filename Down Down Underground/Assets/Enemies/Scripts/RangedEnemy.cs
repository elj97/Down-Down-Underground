using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{

    [Header("Stats")]
    public float fireRate = 1f;
    public float projectileSpeed;
    public float projectileLifetime;
    int amountProjectiles = 4;
    float projectileRotation = 0f;
    float nextTimeToFire = 0f;

    [Header("Prefabs")]
    public GameObject projectile;

    [Header("Other Parts")]
    public RangedDetectPlayer shootRangeCollider;
    [Tooltip("This is how many objects this object is the parent of before the scene is started")]
    public int naturalChildren = 2;

    void Update()
    {
        if (Time.time >= nextTimeToFire)
        {
            if (shootRangeCollider.detectPlayer == true)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
        }

        if (this.gameObject.transform.childCount > naturalChildren)
        {
            this.GetComponentInParent<GenericEnemy>().currentMoveSpeed = 0f;
        } else
        {
            this.GetComponentInParent<GenericEnemy>().currentMoveSpeed = this.GetComponentInParent<GenericEnemy>().moveSpeed;
        }
    }

    void Shoot()
    {
        for(int i = 0; i < amountProjectiles; i++)
        {
            print("it is doing the loop");
            Instantiate(projectile, this.transform.position, Quaternion.Euler(projectileRotation, 90f, 90f), this.transform);
            projectileRotation += 90;
        }
    }
}
