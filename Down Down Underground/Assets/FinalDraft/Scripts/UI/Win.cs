using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{

    public int enemiesInScene;
    public float winWaitTime;
    bool win = false;
    public GameObject winScreen;

    IEnumerator WinWait(float Count)
    {
        yield return new WaitForSeconds(Count); //Count is the amount of time in seconds that you want to wait.
        win = true;                                        //And here goes your method of resetting the game...
        yield return null;
    }

    void Update()
    {
        if (enemiesInScene < 1)
        {
            StartCoroutine("WinWait", winWaitTime);
        }

        if (win == true)
        {
            winScreen.SetActive(true);
            if (SAE.ArcadeMachine.input.PlayerPressingButton(SAE.ArcadeMachine.PlayerColorId.YELLOW_PLAYER, 1) == true)
            {
                NextLevel();
            }
            if (SAE.ArcadeMachine.input.PlayerPressingButton(SAE.ArcadeMachine.PlayerColorId.BLUE_PLAYER, 1) == true)
            {
                NextLevel();
            }
            if (SAE.ArcadeMachine.input.PlayerPressingButton(SAE.ArcadeMachine.PlayerColorId.GREEN_PLAYER, 1) == true)
            {
                NextLevel();
            }
            if (SAE.ArcadeMachine.input.PlayerPressingButton(SAE.ArcadeMachine.PlayerColorId.RED_PLAYER, 1) == true)
            {
                NextLevel();
            }
        }
    }

    void NextLevel()
    {

    }
}
