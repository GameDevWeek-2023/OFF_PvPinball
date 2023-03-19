using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public CustomNetworkManager networkManager;
    public GameObject panel;
    public GamePreferencesManager gamePreferencesManager;
    private void Start()
    {
    }
    public void HostLobby()
    {
        networkManager.StartHost();
        //panel.SetActive(false);
    }

    public void PlayDuo()
    {
        if (gamePreferencesManager.selectedGameMode == 2)
        {
            SceneManager.LoadScene(2);
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }

    public void PlaySolo()
    {
        if (gamePreferencesManager.selectedGameMode == 2)
        {
            SceneManager.LoadScene(4);
        }
        else
        {
            SceneManager.LoadScene(3);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
