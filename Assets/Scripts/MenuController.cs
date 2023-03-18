using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public CustomNetworkManager networkManager;
    public GameObject panel;
    private void Start()
    {
    }
    public void HostLobby()
    {
        networkManager.StartHost();
        //panel.SetActive(false);
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
