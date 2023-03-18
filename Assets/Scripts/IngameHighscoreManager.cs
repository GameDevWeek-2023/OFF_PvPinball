using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IngameHighscoreManager : MonoBehaviour
{
    public GamePreferencesManager gamePreferencesManager;
    public int leftPlayerScore = 0;
    public int rightPlayerScore = 0;
    public GameObject[] scoreBoards;
    public GameObject scoreBoard;
    private int lastScoreBoard = 0;

    public TextMeshProUGUI scoreLeft;
    public TextMeshProUGUI scoreRight;
    public void ResetPlayerPoints()
    {
        leftPlayerScore = 0;
        rightPlayerScore = 0;
        UpdateUI();
    }

    private void Awake()
    {
        for(int i = 0;i < scoreBoards.Length;i++)
        {
            GameObject gam = Instantiate(scoreBoard);
            scoreBoards[i] = gam;
            gam.SetActive(false);
        }
    }
    public void Score(int punkte , Transform pos , Vector3 force , Color color)
    {
        scoreBoards[lastScoreBoard++].GetComponent<DamageNumbers>().Aktivate(punkte,pos , force , color);
        if(lastScoreBoard >= scoreBoards.Length)
        {
            lastScoreBoard = 0;
        }

        AddPoints(true, punkte);
    }

    public void AddPoints(bool isLeftPlayer, int points)
    {
        if (isLeftPlayer)
        {
            leftPlayerScore += points;
        }
        else
        {
            rightPlayerScore += points;
        }
        UpdateUI(); 
    }

    public void SaveHighScore()
    {
        print("saving highscore");
        if (leftPlayerScore > 0)
        {
            gamePreferencesManager.AddHighScore(gamePreferencesManager.playerLeftName, leftPlayerScore);
        }

        if (rightPlayerScore > 0)
        {
            gamePreferencesManager.AddHighScore(gamePreferencesManager.playerRightName, rightPlayerScore);
        }

        ResetPlayerPoints();
    }

    public void UpdateUI()
    {
        scoreLeft.text = leftPlayerScore.ToString();
        scoreRight.text = rightPlayerScore.ToString();
    }
}
