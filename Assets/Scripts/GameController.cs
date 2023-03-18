using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public GameObject endScreen;
    public GameObject pauseScreen;
    
    public Healthbar healthbar;

    public int totalStartingBalls = 3;

    public int ballCountLeft;
    public int ballCountRight;

    public IngameHighscoreManager ingameHighscoreManager;

    private void Start()
    {
        timerText.text = timer.ToString("F2");
        endScreen.SetActive(false);
        pauseScreen.SetActive(false);
        FindObjectOfType<AudioManager>().Play("BackgroundMusic");

        ResetBallCounts();
        ingameHighscoreManager.ResetPlayerPoints();
        
        healthbar.InitHitPoints(0,totalStartingBalls);
        healthbar.InitHitPoints(1,totalStartingBalls);
    }

    private void Update()
    {
        if (gameStarted)
        {
            timer += Time.deltaTime;
            timerText.text = timer.ToString("F2");
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
        ingameHighscoreManager.SaveHighScore();
    }
    
    public void OnStart()
    {
        ResetBallCounts();
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
        ingameHighscoreManager.ResetPlayerPoints();
        ResetPipes();
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
        ResetBallCounts();
        timer = 0;
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
        gameStarted = false;
        isPaused = false;
        PlayAgain();
        ingameHighscoreManager.ResetPlayerPoints();
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
        ballCountLeft--;
        healthbar.RemoveHeart(0, ballCountLeft);
    }

    public void OnHitRight()
    {
        ballCountRight--;
        healthbar.RemoveHeart(1, ballCountRight);
    }

    public void ResetBallCounts()
    {
        ballCountLeft = totalStartingBalls;
        ballCountRight = totalStartingBalls;
    }
}
