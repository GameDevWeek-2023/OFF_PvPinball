using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HighscoreData
{
    public int[] highScores = new int[50];
    public string[] names = new string[50];
    public string playerLeftName;
    public string playerRightName;

    public int numberOfBalls;
    public int numberOfGhostBalls;
    
    public int selectedGameMode;
    public int selectedTime;
    
    public int hpLeft;
    public int hpRight;

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public void LoadFromJson(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);
    }
}

public interface ISaveable
{
    void PopulateSaveData(HighscoreData highscoreData);
    void LoadFromSaveData(HighscoreData highscoreData);
}

