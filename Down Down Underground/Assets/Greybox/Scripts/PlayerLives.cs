using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLives : MonoBehaviour
{
    public GameObject[] hearts;
    public int life;

    void Update()
    {
        if (life < 1)
        {
            Destroy(hearts[0].gameObject);
        }
        else if (life < 2)
        {
            Destroy(hearts[1].gameObject);
        }
        else if (life < 3)
        {
            Destroy(hearts[2].gameObject);
            //Die();
        }

        if (Input.GetKeyDown(KeyCode.Q)) {
            TakeDamage(1);
        }

    }

    public void TakeDamage(int d)
    {
        life -= d;
        print("takedamage is being called");
    }

    // somehow start the respawn script's respawner coroutine
    //public void Die();
     
}
