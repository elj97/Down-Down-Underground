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

    public enum Player
    {
        NONE, YELLOW, BLUE, RED, GREEN
    }
    [Header("Misc")]
    public Player playerSelect;

    [Header("Spawn Delay")] 
    public float spawnDelay = 2f;

    public void YellowPlayerDie()
    {
        YellowPlayer.SetActive(false);
        StartCoroutine("YellowRespawner", spawnDelay);
    }
    public void BluePlayerDie()
    {
        BluePlayer.SetActive(false);
        StartCoroutine("BlueRespawner", spawnDelay);
    }
    public void RedPlayerDie()
    {
        RedPlayer.SetActive(false);
        StartCoroutine("RedRespawner", spawnDelay);
    }
    public void GreenPlayerDie()
    {
        GreenPlayer.SetActive(false);
        StartCoroutine("GreenRespawner", spawnDelay);
    }

    public IEnumerator YellowRespawner(float Count)
    {
        yield return new WaitForSeconds(Count);
        YellowPlayer.transform.position = yellowRespawnPoint.transform.position;
        YellowPlayer.SetActive(true);
        yield return null;
    }

    public IEnumerator BlueRespawner(float Count)
    {
        yield return new WaitForSeconds(Count);
        BluePlayer.transform.position = blueRespawnPoint.transform.position;
        BluePlayer.SetActive(true);
        yield return null;
    }

    public IEnumerator RedRespawner(float Count)
    {
        yield return new WaitForSeconds(Count);
        RedPlayer.transform.position = redRespawnPoint.transform.position;
        RedPlayer.SetActive(true);
        yield return null;
    }

    public IEnumerator GreenRespawner(float Count)
    {
        yield return new WaitForSeconds(Count);
        GreenPlayer.transform.position = greenRespawnPoint.transform.position;
        GreenPlayer.SetActive(true);
        yield return null;
    }


}
