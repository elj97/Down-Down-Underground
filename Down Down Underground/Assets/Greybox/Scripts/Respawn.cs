using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject Player;
    [SerializeField] private Transform Playermodel;
    [SerializeField] private Transform respawnPoint;
    public float spawnDelay = 2f;

    /*  //script that works
    void OnTriggerEnter(Collider other)
    {
        Player.transform.position = respawnPoint.transform.position;
    }*/

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player.SetActive(false);
            StartCoroutine("Respawner", spawnDelay);
        }
    }

    // here i want to get the player lives if last heart destroyed then die and start the respawner coroutine

    IEnumerator Respawner(float Count)
    {
        yield return new WaitForSeconds(Count);
        Playermodel.transform.position = respawnPoint.transform.position;
        Player.SetActive(true);
        yield return null;
    }


    // script to use coroutine
    /*public GameObject player;
    public Transform spawnPoint;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(other.gameObject);
            StartCoroutine("Respawner", 2f);
        }
    }

    IEnumerator Respawner(float spawnDelay)
    {
        yield return new WaitForSeconds(spawnDelay);
        Instantiate(player, spawnPoint.position, spawnPoint.rotation);
    }*/

}
