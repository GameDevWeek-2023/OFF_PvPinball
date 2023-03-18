using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

public class GamemodeManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInputFieldLeft = null;
    [SerializeField] private TMP_InputField nameInputFieldRight = null;
    [SerializeField] private TMP_InputField numberOfBallsInput = null;
    [SerializeField] private TMP_InputField numberOfGhostBallsInput = null;

    [SerializeField] private TMP_Dropdown dropDown;
    
    public bool isSinglePlayer;

    public GamePreferencesManager gamePreferencesManager;

    private GameMode currentGameMode = GameMode.DEFAULT;

    private void Start()
    {
        RefreshInputValues();
    }
    
    public void RefreshInputValues()
    {
        nameInputFieldLeft.text = gamePreferencesManager.playerLeftName;
        
        if (nameInputFieldRight != null)
        {
            nameInputFieldRight.text = gamePreferencesManager.playerRightName;
        }
        
        numberOfBallsInput.text = gamePreferencesManager.numberOfBalls.ToString();
        numberOfGhostBallsInput.text = gamePreferencesManager.numberOfGhostBalls.ToString();

        if (isSinglePlayer && gamePreferencesManager.selectedGameMode == 3)
        {
            dropDown.value = 2;
        }
        else
        {
            dropDown.value = gamePreferencesManager.selectedGameMode;
        }
        
    }

    public void SaveLeftPlayerName()
    {
        gamePreferencesManager.SetLeftName(nameInputFieldLeft.text);
    }
    
    public void SaveRightPlayerName()
    {
        gamePreferencesManager.SetRightName(nameInputFieldRight.text);
    }

    public void SaveNumberOfBalls()
    {
        gamePreferencesManager.SetNumberOfBalls(int.Parse(numberOfBallsInput.text));
    }

    public void SaveNumberOfGhostBalls()
    {
        gamePreferencesManager.SetNumberOfGhostBalls(int.Parse(numberOfGhostBallsInput.text));
    }

    public void SaveGameMode()
    {
        print(dropDown.value);
        switch (dropDown.value)
        {
            case(0): 
                currentGameMode = GameMode.DEFAULT;
                gamePreferencesManager.SetGameMode(0);
                break;
            case(1): 
                currentGameMode = GameMode.TIMER;
                gamePreferencesManager.SetGameMode(1);
                break;
            case(2): 
                currentGameMode = GameMode.ENDLESS;
                gamePreferencesManager.SetGameMode(2);
                break;
            case(3): 
                currentGameMode = GameMode.COOP;
                gamePreferencesManager.SetGameMode(3);
                break;
        }
    }
}

public enum GameMode
{
    DEFAULT,
    TIMER,
    ENDLESS,
    COOP
}
