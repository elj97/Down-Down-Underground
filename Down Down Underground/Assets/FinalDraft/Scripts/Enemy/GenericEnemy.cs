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
    public float currentMoveSpeed;
    public float detectionRange = 2f;
    int pointWorth;

    //Enemy Factors
    [Header("Patrolling")]
    public bool patrolling;
    //Pathing for Patrolling
    public GameObject patrolPoint1;
    public GameObject patrolPoint2;
    bool atStartPoint = true;
    public float timeToDie = 0.1f;


    //Other Scripts to be called
    [Header("Other Scripts")]
    public AIPath aIPathScript;
    public Highscore scoring;
    public Killstreak killstreak;

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
    public bool testKill = false;
    bool deathHasHappened = false;

    //GetComponent things (found out it is expensive in update)
    AIDestinationSetter aIDestinationSetter;
    

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
        currentMoveSpeed = moveSpeed;
        patrolling = true;
        //aIPathScript.maxSpeed = currentMoveSpeed;

        //Sets Tags for the Patrol Points
        patrolPoint1.tag = "PatrolPoint1";
        patrolPoint2.tag = "PatrolPoint2";

        //Sets the collider to be the range of our detection (since the collider is what is actually affecting the detection)
        playerDetectionCollider.radius = detectionRange;

        //Initiate the Coroutine loop
        StartCoroutine("FollowPlayer", detectPlayerTime);

        if (GetComponent<MeleeEnemy>() != null)
        {
            pointWorth = GetComponent<MeleeEnemy>().pointWorth;
        }
        if (GetComponent<RangedEnemy>() != null)
        {
            pointWorth = GetComponent<RangedEnemy>().pointWorth;
        }
        aIDestinationSetter = GetComponent<AIDestinationSetter>();
    }

    void Update()
    {
        aIPathScript.maxSpeed = currentMoveSpeed;

        if (patrolling == true)
        {
            if (atStartPoint == true)
            {
                aIDestinationSetter.target = patrolPoint1.transform;
            }
            if (atStartPoint == false)
            {
                aIDestinationSetter.target = patrolPoint2.transform;
            }
        }
        
        if (followPlayer == true)
        {
            patrolling = false;
            aIDestinationSetter.target = detectPlayerScript.followedPlayer.transform;
        }

        if (health <= 0)
        {
            Death();
        }

        //Testing Killstreaks
        if (testKill == true)
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
    public void Death()
    {
        if (deathHasHappened == false)
        {
            scoring.IncreaseScore(pointWorth);
            Destroy(transform.parent.gameObject, timeToDie);
            deathHasHappened = true;
            killstreak.killstreak++;
        }
    }
}
