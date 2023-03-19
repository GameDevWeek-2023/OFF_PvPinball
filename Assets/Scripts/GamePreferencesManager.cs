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
    
    public int numberOfBalls = 2;
    public int numberOfGhostBalls = 1;
    
    public int selectedGameMode = 0;
    public int selectedTime = 2;

    public int hpLeft = 5;
    public int hpRight = 5;

    public int[] times = { 1, 2, 3, 5, 10 };

    public HighscoreController highscoreController;

    public GamemodeManager singlelayerManager;
    public GamemodeManager multiplayerManager;

    public GameController gameController;

    public Pipe ballSpawnerLeft;
    public Pipe ballSpawnerRight;
    
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
        RefreshGamemodeController();

        if (ballSpawnerLeft != null)
        {
            ballSpawnerLeft.startBalls = numberOfBalls;
            ballSpawnerLeft.startGhostBalls = numberOfGhostBalls;
        }

        if (ballSpawnerRight != null)
        {
            ballSpawnerRight.startBalls = numberOfBalls;
            ballSpawnerRight.startGhostBalls = numberOfGhostBalls;
        }

        if (gameController != null)
        {
            gameController.InitHP(hpLeft, hpRight);
        }

        if (gameController != null)
        {
            gameController.Init();
        }
    }

    private void OnApplicationQuit()
    {
        SaveGameData();
    }

    public void RefreshGamemodeController()
    {
        if (singlelayerManager != null && multiplayerManager != null)
        {
            singlelayerManager.RefreshInputValues();
            multiplayerManager.RefreshInputValues();
        }
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

        
        HighScores.UploadScore(name, points);
        
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
        if (highscoreController != null)
        {
            highscoreController.FillHighscores();
        }
    }

    public void SetLeftName(string name)
    {
        playerLeftName = name;
        SaveGameData();
        RefreshGamemodeController();
    }

    public void SetRightName(string name)
    {
        playerRightName = name;
        SaveGameData();
        RefreshGamemodeController();
    }

    public void SetNumberOfBalls(int balls)
    {
        numberOfBalls = balls;
        SaveGameData();
        RefreshGamemodeController();
    }

    public void SetNumberOfGhostBalls(int balls)
    {
        numberOfGhostBalls = balls;
        SaveGameData();
        RefreshGamemodeController();
    }

    public void SetGameMode(int id)
    {
        selectedGameMode = id;
        SaveGameData();
        RefreshGamemodeController();
    }

    public void SetSelectedTime(int id)
    {
        selectedTime = id;
        SaveGameData();
        RefreshGamemodeController();
    }

    public void SetHPLeft(int id)
    {
        hpLeft = id;
        SaveGameData();
        RefreshGamemodeController();
    }

    public void SetHPRight(int id)
    {
        hpRight = id;
        SaveGameData();
        RefreshGamemodeController();
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
        highscoreData.numberOfBalls = numberOfBalls;
        highscoreData.numberOfGhostBalls = numberOfGhostBalls;
        highscoreData.selectedGameMode = selectedGameMode;
        highscoreData.selectedTime = selectedTime;
        highscoreData.hpLeft = hpLeft;
        highscoreData.hpRight = hpRight;
    }

    public void LoadFromSaveData(HighscoreData highscoreData)
    {
        highScores = highscoreData.highScores;
        names = highscoreData.names;
        playerLeftName = highscoreData.playerLeftName;
        playerRightName = highscoreData.playerRightName;
        selectedGameMode = highscoreData.selectedGameMode;
        numberOfBalls = highscoreData.numberOfBalls;
        numberOfGhostBalls = highscoreData.numberOfGhostBalls;
        selectedTime = highscoreData.selectedTime;
        hpLeft = highscoreData.hpLeft;
        hpRight = highscoreData.hpRight;

        if (hpLeft == 0)
        {
            hpLeft = 5;
        }
        if(hpRight == 0)
        {
            hpRight = 5;
        }
        
        CombineData();
    }
}

public struct ScoreData
{
    public string playerName;
    public int score;
}
