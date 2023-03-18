using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameHighscoreManager : MonoBehaviour
{
    public GamePreferencesManager gamePreferencesManager;
    public int leftPlayerScore = 0;
    public int rightPlayerScore = 0;

    public void ResetPlayerPoints()
    {
        leftPlayerScore = 0;
        rightPlayerScore = 0;
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
    }

    public void SaveHighScore()
    {
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
}
