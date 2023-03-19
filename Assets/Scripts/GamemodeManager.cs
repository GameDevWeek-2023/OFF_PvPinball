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
    [SerializeField] private TMP_InputField hpLeftInput = null;
    [SerializeField] private TMP_InputField hpRightInput = null;

    [SerializeField] private TMP_Dropdown dropDown;
    [SerializeField] private TMP_Dropdown timeSelectDropDown;

    [SerializeField] private GameObject timeSelector;
    
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

        hpLeftInput.text = gamePreferencesManager.hpLeft.ToString();

        if (hpRightInput != null)
        {
            hpRightInput.text = gamePreferencesManager.hpRight.ToString();
        }
        else
        {
            
        }
        

        if (isSinglePlayer && gamePreferencesManager.selectedGameMode == 3)
        {
            dropDown.value = 2;
        }
        else
        {
            dropDown.value = gamePreferencesManager.selectedGameMode;
        }

        timeSelectDropDown.value = gamePreferencesManager.selectedTime;
        if (gamePreferencesManager.selectedGameMode == 1)
        {
            timeSelector.SetActive(true);
        }
        else
        {
            timeSelector.SetActive(false);
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
    
    public void SaveHPLeft()
    {
        gamePreferencesManager.SetHPLeft(int.Parse(hpLeftInput.text));
    }

    public void SaveHPRight()
    {
        if (hpRightInput != null)
        {
            gamePreferencesManager.SetHPRight(int.Parse(hpRightInput.text));
        }
        
    }

    public void SaveGameMode()
    {
        print(dropDown.value);
        switch (dropDown.value)
        {
            case(0): 
                currentGameMode = GameMode.DEFAULT;
                gamePreferencesManager.SetGameMode(0);
                timeSelector.SetActive(false);
                break;
            case(1): 
                currentGameMode = GameMode.TIMER;
                gamePreferencesManager.SetGameMode(1);
                timeSelector.SetActive(true);
                break;
            case(2): 
                currentGameMode = GameMode.ENDLESS;
                gamePreferencesManager.SetGameMode(2);
                timeSelector.SetActive(false);
                break;
            case(3): 
                currentGameMode = GameMode.COOP;
                gamePreferencesManager.SetGameMode(3);
                timeSelector.SetActive(false);
                break;
        }
    }

    public void SaveSelectedTime()
    {
        switch (timeSelectDropDown.value)
        {
            case(0):
                gamePreferencesManager.SetSelectedTime(0);
                break;
            case(1):
                gamePreferencesManager.SetSelectedTime(1);
                break;
            case(2):
                gamePreferencesManager.SetSelectedTime(2);
                break;
            case(3):
                gamePreferencesManager.SetSelectedTime(3);
                break;
            case(4):
                gamePreferencesManager.SetSelectedTime(4);
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
