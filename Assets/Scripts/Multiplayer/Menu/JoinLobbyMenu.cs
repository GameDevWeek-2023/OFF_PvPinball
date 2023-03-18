using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JoinLobbyMenu : MonoBehaviour
{
    [SerializeField] private CustomNetworkManager networkManager = null;

    [Header("UI")] 
    [SerializeField] private GameObject landingPagePanel = null;
    [SerializeField] private GameObject buttonsPanel = null;
    [SerializeField] private GameObject warningLabel = null;
    [SerializeField] private TMP_InputField ipAddressInputField = null;
    [SerializeField] private Button joinButton = null;

    private void Start()
    {
        ipAddressInputField.text = "localhost";
    }

    private void OnEnable()
    {
        CustomNetworkManager.OnClientConnected += HandleClientConnected;
        CustomNetworkManager.OnClientDisconnected += HandleClientDisconnected;
    }

    private void OnDisable()
    {
        CustomNetworkManager.OnClientConnected -= HandleClientConnected;
        CustomNetworkManager.OnClientDisconnected -= HandleClientDisconnected;
    }

    public void JoinLobby()
    {
        warningLabel.SetActive(false);
        string ipAddress = ipAddressInputField.text;
        
        networkManager.networkAddress = ipAddress;
        networkManager.StartClient();

        joinButton.interactable = false;
    }

    private void HandleClientConnected()
    {
        warningLabel.SetActive(false);
        joinButton.interactable = true;
        
        gameObject.SetActive(false);
        landingPagePanel.SetActive(false);
        buttonsPanel.SetActive(true);
    }

    private void HandleClientDisconnected()
    {
        joinButton.interactable = true;
        warningLabel.SetActive(true);
    }
}
