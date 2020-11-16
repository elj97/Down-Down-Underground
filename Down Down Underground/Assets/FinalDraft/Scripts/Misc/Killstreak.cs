using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killstreak : MonoBehaviour
{
    public int killstreak = 0; //remove public after testing***
    public bool spawnedFruit = false; //remove public after testing***
    public FruitSpawn fruitSpawn;

    void Update()
    {
        //If anyone looks at this code Im sorry... This is just the way I know how to do it for now and I dont have time to look for a more efficient route
        if (killstreak == 2 || killstreak == 4 || killstreak == 6)
        {
            if (spawnedFruit == false)
            {
                spawnFruit1();
                spawnedFruit = true;
            }
        }
        if (killstreak == 8)
        {
            if (spawnedFruit == false)
            {
                spawnFruit2();
                spawnedFruit = true;
            }
        }
        if (killstreak == 10)
        {
            if (spawnedFruit == false)
            {
                spawnFruit2();
                spawnedFruit = true;
                killstreak = 0;
            }
        }
        if (killstreak == 0 || killstreak == 1 || killstreak == 3 || killstreak == 5 || killstreak == 7 || killstreak == 9)
        {
            spawnedFruit = false;
        }
    }

    void spawnFruit1()
    {
        print("spawn fruit 1");
        fruitSpawn.SpawnFruit1();
    }

    void spawnFruit2()
    {
        print("spawn fruit 2");
        fruitSpawn.SpawnFruit2();
    }

}
