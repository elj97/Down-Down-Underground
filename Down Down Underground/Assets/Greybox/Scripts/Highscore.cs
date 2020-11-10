using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highscore : MonoBehaviour
{
    [Header("Text")]
    public Text score;
    public Text highScore;

    private int currentScore = 0;
    private int bestScore = 0;

    void Start()
    {
        if (PlayerPrefs.HasKey("highScore"))
        {
            bestScore = PlayerPrefs.GetInt("highScore");
            highScore.text = "Best: " + bestScore;
        }

        score.text = "0";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            IncreaseScore(1);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetScore();
        }
    }

    public void IncreaseScore(int increase)
    {
        currentScore += increase;


        // can't use on click for some reason??
        if (PlayerPrefs.GetInt("highScore") < currentScore) 
        {
            PlayerPrefs.SetInt("highScore", currentScore);

            bestScore = PlayerPrefs.GetInt("highScore");
            highScore.text = "Best: " + bestScore;
        }

        score.text = "" + currentScore;
    }

    public void ResetScore()
    {
        // can't use on click for some reason??
        PlayerPrefs.DeleteKey("highScore");

        score.text = "0";
        highScore.text = "Best: xx";
    }
}
