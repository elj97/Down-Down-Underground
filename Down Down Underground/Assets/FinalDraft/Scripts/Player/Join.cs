using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Join : MonoBehaviour
{
    public GameObject yellowPlayer;
    public GameObject redPlayer;
    public GameObject greenPlayer;
    public GameObject bluePlayer;

    void Update()
    {
        if (SAE.ArcadeMachine.input.PlayerPressingButton(SAE.ArcadeMachine.PlayerColorId.YELLOW_PLAYER, 1) == true)
        {
            yellowPlayer.SetActive(true);
        }
        if (SAE.ArcadeMachine.input.PlayerPressingButton(SAE.ArcadeMachine.PlayerColorId.RED_PLAYER, 1) == true)
        {
            redPlayer.SetActive(true);
        }
        if (SAE.ArcadeMachine.input.PlayerPressingButton(SAE.ArcadeMachine.PlayerColorId.GREEN_PLAYER, 1) == true)
        {
            greenPlayer.SetActive(true);
        }
        if (SAE.ArcadeMachine.input.PlayerPressingButton(SAE.ArcadeMachine.PlayerColorId.BLUE_PLAYER, 1) == true)
        {
            bluePlayer.SetActive(true);
        }
    }
}
