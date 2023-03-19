using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameController : MonoBehaviour
{

    private bool gameStarted, isPlaying, isPaused;

    public Pipe startPipeLeft, startPipeRight;
    private float timer = 0;
    public TextMeshProUGUI timerText, endGameTimerText, winnerText;
    public GameObject timerGO;
    public GameObject endScreen;
    public GameObject pauseScreen;

    public GamePreferencesManager gamePreferencesManager;
    
    public Healthbar healthbar;

    public int startLeft;
    public int startRight;

    public int hitPointsLeft;
    public int hitPointsRight;

    public IngameHighscoreManager ingameHighscoreManager;

    private bool isTimed = false;
    
    public int currentGameMode = 0;
    

    private void Start()
    {
        
    }

    public void Init()
    {
        timerText.text = timer.ToString("F2");
        endScreen.SetActive(false);
        pauseScreen.SetActive(false);
        FindObjectOfType<AudioManager>().Play("BackgroundMusic");
        
        if (ingameHighscoreManager != null)
        {
            ingameHighscoreManager.ResetPlayerPoints();
        }
        
        ChangeGameMode();
    }

    public void ChangeGameMode()
    {
        currentGameMode = gamePreferencesManager.selectedGameMode;
        
        switch (currentGameMode)
        {
            case(0):
                isTimed = false;
                ingameHighscoreManager.isCoop = false;
                ingameHighscoreManager.ToggleCounter(true);
                timerGO.SetActive(false);
                break;
            case(1):
                isTimed = true;
                timer = gamePreferencesManager.times[gamePreferencesManager.selectedTime] * 60;
                ingameHighscoreManager.isCoop = false;
                ingameHighscoreManager.ToggleCounter(true);
                timerGO.SetActive(true);
                break;
            case(2):
                isTimed = false;
                ingameHighscoreManager.isCoop = true;
                ingameHighscoreManager.ToggleCounter(true);
                timerGO.SetActive(false);
                break;
            case(3):
                isTimed = false;
                ingameHighscoreManager.isCoop = false;
                ingameHighscoreManager.ToggleCounter(false);
                timerGO.SetActive(false);
                break;
        }
    }

    public void InitHP(int l, int r)
    {
        print("l " + l + " r " + r);
        startLeft = l;
        startRight = r;
        ResetHitPoints();
    }
    
    public void ResetHitPoints()
    {
        hitPointsLeft = startLeft;
        hitPointsRight = startRight;
        
        healthbar.InitHitPoints(true,hitPointsLeft);
        healthbar.InitHitPoints(false,hitPointsRight);
    }
    
    private void Update()
    {
        if (gameStarted && isTimed)
        {
            timer -= Time.deltaTime;
            if (timerText != null)
            {
                timerText.text = timer.ToString("F2");
            }

            if (timer <= 0)
            {
                EndGame(ingameHighscoreManager.leftPlayerScore > ingameHighscoreManager.rightPlayerScore);
            }
        }
    }

    public void EndGame(bool isLeftPlayer)
    {
        DestroyBalls();
        ResetPipes();
        gameStarted = false;
        endGameTimerText.text = timer.ToString("F2");
        timer = 0;

        if (isLeftPlayer)
        {
            winnerText.text = "RIGHT PLAYER WON";
        }
        else
        {
            winnerText.text = "LEFT PLAYER WON";
        }
        
        endScreen.SetActive(true);
        if (ingameHighscoreManager != null && currentGameMode == 0)
        {
            ingameHighscoreManager.SaveHighScore();
        }
    }
    
    public void OnStart()
    {
        ResetHitPoints();
        endScreen.SetActive(false);
        if (!gameStarted && !isPlaying && !isPaused)
        {
            TogglePipes(true);
            if (startPipeLeft != null)
            {
                startPipeLeft.StartGame();
            }

            if (startPipeRight != null)
            {
                startPipeRight.StartGame();
            }

            gameStarted = true;
            isPlaying = true;
        }    
    }

    public void PlayAgain()
    {
        endScreen.SetActive(false);
        timerText.text = "0.00";
        isPlaying = false;
        if (ingameHighscoreManager != null)
        {
            ingameHighscoreManager.ResetPlayerPoints();
        }
        ResetPipes();
        ChangeGameMode();
    }

    void OnPause()
    {
        if (!isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }
    
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
    }

    public void RestartGame()
    {
        TogglePipes(false);
        ResetPipes();
        DestroyBalls();
        ResetHitPoints();
        timer = 0;
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
        gameStarted = false;
        isPaused = false;
        PlayAgain();
        ChangeGameMode();
        
        if (ingameHighscoreManager != null)
        {
            ingameHighscoreManager.ResetPlayerPoints();
        }
    }

    public void DestroyBalls()
    {
        foreach (GameObject ball in GameObject.FindGameObjectsWithTag("Ball")) 
        {
            Destroy(ball);
        }
    }

    private void ClearQueue()
    {
        if (startPipeLeft != null)
        {
            startPipeLeft.ClearQueue();
        }
        
        if (startPipeRight != null)
        {
            startPipeRight.ClearQueue();
        }
    }

    private void ResetPipes()
    {
        ClearQueue();
        if (startPipeLeft != null)
        {
            startPipeLeft.StopGame();
            startPipeLeft.LoadBalls();
        }
        
        if (startPipeRight != null)
        {
            startPipeRight.StopGame();
            startPipeRight.LoadBalls();
        }
    }

    public void TogglePipes(bool val)
    {
        if (startPipeLeft != null)
        {
            startPipeLeft.canShoot = val;
        }
        
        if (startPipeRight != null)
        {
            startPipeRight.canShoot = val;
        }
    }

    public void OnHitLeft()
    {
        hitPointsLeft--;
        healthbar.RemoveHeart(0, hitPointsLeft);
    }

    public void OnHitRight()
    {
        hitPointsRight--;
        healthbar.RemoveHeart(1, hitPointsRight);
    }
}
