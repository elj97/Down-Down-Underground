using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEnemy : MonoBehaviour
{
    //Enemy Stats
    [Header("Enemy Stats")]
    public float health;
    public float moveSpeed;
    public float detectionRange = 2f;

    //Enemy Factors
    [Header("Patrolling")]
    public bool patrolling;
    //Pathing for Patrolling
    public GameObject patrolPoint1;
    public GameObject patrolPoint2;
    bool atStartPoint = true;


    //Other Scripts to be called
    [Header("Other Scripts")]
    public AIPath aIPathScript;

    //Pathing for the Player/s
    [Header("Pathing to Player")]
    public float checkDistanceTime = 1f;
    bool followPlayer;
    public bool playerInRange;
    public SphereCollider playerDetectionCollider;
    public DetectPlayer detectPlayerScript;
    [Range(0,100)]
    public float followPlayerChance = 50f;
    public float detectPlayerTime = 1f;

    //Variables with no place at the moment
    [Header("Misc")]
    float randomNumber;

    IEnumerator FollowPlayer(float Count)
    {
        yield return new WaitForSeconds(Count);
        if (playerInRange == true)
        {
            randomNumber = Random.Range(0f, 100f);
            if (randomNumber <= followPlayerChance)
            {
                followPlayer = true;
            }
        }
        if (followPlayer == false)
        {
            StartCoroutine("FollowPlayer", detectPlayerTime);
        }
        yield return null;
    }

    void Start()
    {
        patrolling = true;
        aIPathScript.maxSpeed = moveSpeed;

        //Sets Tags for the Patrol Points
        patrolPoint1.tag = "PatrolPoint1";
        patrolPoint2.tag = "PatrolPoint2";

        //Sets the collider to be the range of our detection (since the collider is what is actually affecting the detection)
        playerDetectionCollider.radius = detectionRange;

        //Initiate the Coroutine loop
        StartCoroutine("FollowPlayer", detectPlayerTime);

    }

    void Update()
    {
        if (patrolling == true)
        {
            if (atStartPoint == true)
            {
                GetComponent<AIDestinationSetter>().target = patrolPoint1.transform;
            }
            if (atStartPoint == false)
            {
                GetComponent<AIDestinationSetter>().target = patrolPoint2.transform;
            }
        }
        
        if (followPlayer == true)
        {
            patrolling = false;
            GetComponent<AIDestinationSetter>().target = detectPlayerScript.followedPlayer.transform;
        }

        //Kill Player
        if (health <= 0)
        {
            Death();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        /*if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject, 0f);
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PatrolPoint1"))
        {
            atStartPoint = false;
        }
        if (other.gameObject.CompareTag("PatrolPoint2"))
        {
            atStartPoint = true;
        }
    }

    //Death
    void Death()
    {
        Destroy(this.gameObject);
    }

    //Drawing Ranges n Stuff
    void OnDrawGizmosSelected()
    {
        // Draw a red sphere at the transform's position
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
    
}
