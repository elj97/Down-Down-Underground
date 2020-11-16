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
    public int pointWorth = 100;

    [Header("Prefabs")]
    public GameObject projectile;

    [Header("Other Parts")]
    public RangedDetectPlayer shootRangeCollider;
    [Tooltip("This is how many objects this object is the parent of before the scene is started, MUST BE ACCURATE")]
    public int naturalChildren = 2;

    //GetComponent things (found out it is expensive in update)
    GenericEnemy genericEnemy;
    Rigidbody rigidBody;

    private void Start()
    {
        genericEnemy = this.GetComponentInParent<GenericEnemy>();
        rigidBody = this.GetComponent<Rigidbody>();
    }

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
            genericEnemy.currentMoveSpeed = 0f;
            //Stop Sliding
            rigidBody.isKinematic = true;
        } else
        {
            genericEnemy.currentMoveSpeed = genericEnemy.moveSpeed;
            //Stop Sliding
            rigidBody.isKinematic = false;
        }
    }

    void Shoot()
    {
        for(int i = 0; i < amountProjectiles; i++)
        {
            Instantiate(projectile, this.transform.position, Quaternion.Euler(projectileRotation, 90f, 90f), this.transform);
            projectileRotation += 90;
        }
    }
}
