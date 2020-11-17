using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lose : MonoBehaviour
{
    public GameObject yellowPlayer;
    public GameObject redPlayer;
    public GameObject greenPlayer;
    public GameObject bluePlayer;

    public PlayerLives playerLives;

    public GameObject loseScreen;

    void Update()
    {
        if (yellowPlayer.activeInHierarchy == false && redPlayer.activeInHierarchy == false && greenPlayer.activeInHierarchy == false && bluePlayer.activeInHierarchy == false && playerLives.life < 1)
        {
            loseScreen.SetActive(true);
            if (SAE.ArcadeMachine.input.PlayerPressingButton(SAE.ArcadeMachine.PlayerColorId.YELLOW_PLAYER, 1) == true)
            {
                ReturnToMenu();
            }
            if (SAE.ArcadeMachine.input.PlayerPressingButton(SAE.ArcadeMachine.PlayerColorId.BLUE_PLAYER, 1) == true)
            {
                ReturnToMenu();
            }
            if (SAE.ArcadeMachine.input.PlayerPressingButton(SAE.ArcadeMachine.PlayerColorId.GREEN_PLAYER, 1) == true)
            {
                ReturnToMenu();
            }
            if (SAE.ArcadeMachine.input.PlayerPressingButton(SAE.ArcadeMachine.PlayerColorId.RED_PLAYER, 1) == true)
            {
                ReturnToMenu();
            }
        }
    }

    void ReturnToMenu()
    {

    }

}
