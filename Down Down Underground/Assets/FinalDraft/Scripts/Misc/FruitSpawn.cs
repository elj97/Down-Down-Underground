using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawn : MonoBehaviour
{
    public GameObject Fruit1;
    public GameObject Fruit2;

    public void SpawnFruit1()
    {
        Object.Instantiate(Fruit1, transform);
    }

    public void SpawnFruit2()
    {
        Object.Instantiate(Fruit2, transform);
    }
}
