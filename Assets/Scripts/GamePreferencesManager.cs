using System;

using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class GamePreferencesManager : MonoBehaviour, ISaveable
{
    public int[] highScores = new[]
    {
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
    };

    public string[] names = new[]
    {
        "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
        "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
    };

    public Dictionary<int, ScoreData> combinedData = new Dictionary<int, ScoreData>();
    
    public string playerLeftName = "Player 1";
    public string playerRightName = "Player 2";
    public int selectedGameMode = 0;

    public HighscoreController highscoreController;
    
    const string glyphs= "abcdefg$$anonymous$$jklmnopqrstuvwxyz0123456789";

    private int addCounter = 0;

    
    void Start()
    {
        CombineData();
        LoadGameData();
        if (highscoreController != null)
        {
            FillHighscores();
        }
    }

    private void OnApplicationQuit()
    {
        SaveGameData();
    }

    public void AddRandomHighscore()
    {
        int charAmount = UnityEngine.Random.Range(3, 10); //set those to the minimum and maximum length of your string
        string randomName = "";
        for(int i=0; i<charAmount; i++)
        {
            randomName += glyphs[UnityEngine.Random.Range(0, glyphs.Length)];
        }

        int p = UnityEngine.Random.Range(50, 10000);
        
        AddHighScore(randomName, p);
    }
    
    public void AddHighScore(string name, int points)
    {
        ScoreData s = new ScoreData();
        s.score = points;
        s.playerName = name;
        
        //combinedData.Add(combinedData.Count + addCounter, s);
        combinedData =  combinedData.OrderBy(pair => pair.Value.score).Reverse().ToDictionary(pair => pair.Key, pair => pair.Value);
        int key = combinedData.Keys.Last();
        combinedData[key] = s;
        combinedData =  combinedData.OrderBy(pair => pair.Value.score).Reverse().ToDictionary(pair => pair.Key, pair => pair.Value);
        
        if (highscoreController != null)
        {
            FillHighscores();
        }
        SaveGameData();
        addCounter++;
    }
    public void FillHighscores()
    {
        highscoreController.FillHighscores();
    }
    
    public void ResetHighscores()
    {
        highScores = new[]
        {
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
        };
        names = new[]
        {
            "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
            "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
        };
    }

    public void ResetPlayerNames()
    {
        playerLeftName = "Player 1";
        playerRightName = "Player 2";
    }

    public void ResetAll()
    {
        ResetHighscores();
        ResetPlayerNames();
        
        CombineData();
        SaveGameData();
        
        FillHighscores();
    }
    
    public void CombineData()
    {
        combinedData = new Dictionary<int, ScoreData>();
        for (int i = 0; i < 50; i++)
        {
            ScoreData s = new ScoreData();
            s.score = highScores[i];
            s.playerName = names[i];
            combinedData[i] = s;
        }
    }

    public void SplitData()
    {
        for (int i = 0; i < 50; i++)
        {
            highScores[i] = combinedData[i].score;
            names[i] = combinedData[i].playerName;
        }
    }
    
    public void SaveGameData()
    {
        HighscoreData sd = new HighscoreData();
        PopulateSaveData(sd);

        if (FileManager.WriteToFile("SaveData.dat", sd.ToJson()))
        {
            print("data saved succesfully");
        }
        else
        {
            print("failed to save data");
        }
    }

    public void LoadGameData()
    {
        if (FileManager.LoadFromFile("SaveData.dat", out var json))
        {
            HighscoreData sd = new HighscoreData();
            sd.LoadFromJson(json);
            
            LoadFromSaveData(sd);
            print("data loaded succesfully");
        }
        else
        {
            print("failed to load data");
        }
    }
    
    public void PopulateSaveData(HighscoreData highscoreData)
    {
        SplitData();
        
        highscoreData.highScores = highScores;
        highscoreData.names = names;
        highscoreData.playerLeftName = playerLeftName;
        highscoreData.playerRightName = playerRightName;
        highscoreData.selectedGameMode = selectedGameMode;
    }

    public void LoadFromSaveData(HighscoreData highscoreData)
    {
        highScores = highscoreData.highScores;
        names = highscoreData.names;
        playerLeftName = highscoreData.playerLeftName;
        playerRightName = highscoreData.playerRightName;
        selectedGameMode = highscoreData.selectedGameMode;
        
        CombineData();
    }
}

public struct ScoreData
{
    public string playerName;
    public int score;
}
