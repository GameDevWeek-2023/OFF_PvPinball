using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI scoreText;

    public void SetScore(string score)
    {
        scoreText.text = score;
    }

    public void SetPlayerName(string playerName)
    {
        playerNameText.text = playerName;
    }
}
