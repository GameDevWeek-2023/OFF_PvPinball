using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class GamemodeManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInputFieldLeft = null;
    [SerializeField] private TMP_InputField nameInputFieldRight = null;
    [SerializeField] private TMP_InputField numberOfBallsInput = null;
    [SerializeField] private TMP_InputField numberOfGhostBallsInput = null;
    
    public bool isSinglePlayer;

    public GamePreferencesManager gamePreferencesManager;

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
}
