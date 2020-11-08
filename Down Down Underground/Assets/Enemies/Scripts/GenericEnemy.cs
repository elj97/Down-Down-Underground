using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEnemy : MonoBehaviour
{

    public float health;
    public float moveSpeed;

    bool patrolling;

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject, 0f);
        }
    }

}
