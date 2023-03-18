using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public CustomNetworkManager networkManager;
    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("MainTheme");
    }
    public void HostLobby()
    {
        networkManager.StartHost();
        
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
