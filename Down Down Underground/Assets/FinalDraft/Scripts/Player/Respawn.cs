using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [Header("Players")]
    public GameObject YellowPlayer;
    public GameObject BluePlayer;
    public GameObject RedPlayer;
    public GameObject GreenPlayer;

    [Header("Respawn Points")]
    [SerializeField] private Transform yellowRespawnPoint;
    [SerializeField] private Transform blueRespawnPoint;
    [SerializeField] private Transform redRespawnPoint;
    [SerializeField] private Transform greenRespawnPoint;

 

    [Header("Spawn Delay")] 
    public float spawnDelay = 2f;


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer ("YellowPlayer"))
        {
            YellowPlayer.SetActive(false);
            StartCoroutine("Respawner", spawnDelay);
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("BluePlayer"))
        {
            BluePlayer.SetActive(false);
            StartCoroutine("Respawner", spawnDelay);
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("RedPlayer"))
        {
            RedPlayer.SetActive(false);
            StartCoroutine("Respawner", spawnDelay);
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("GreenPlayer"))
        {
            GreenPlayer.SetActive(false);
            StartCoroutine("Respawner", spawnDelay);
        }
    }


    // here i want to get the player lives if last heart destroyed then die and start the respawner coroutine

    IEnumerator YellowRespawner(float Count)
    {
        yield return new WaitForSeconds(Count);
        YellowPlayer.transform.position = yellowRespawnPoint.transform.position;
        YellowPlayer.SetActive(true);
        yield return null;
    }

    IEnumerator BlueRespawner(float Count)
    {
        yield return new WaitForSeconds(Count);
        BluePlayer.transform.position = blueRespawnPoint.transform.position;
        BluePlayer.SetActive(true);
        yield return null;
    }

    IEnumerator RedRespawner(float Count)
    {
        yield return new WaitForSeconds(Count);
        RedPlayer.transform.position = redRespawnPoint.transform.position;
        RedPlayer.SetActive(true);
        yield return null;
    }

    IEnumerator Respawner(float Count)
    {
        yield return new WaitForSeconds(Count);
        GreenPlayer.transform.position = greenRespawnPoint.transform.position;
        GreenPlayer.SetActive(true);
        yield return null;
    }


}
