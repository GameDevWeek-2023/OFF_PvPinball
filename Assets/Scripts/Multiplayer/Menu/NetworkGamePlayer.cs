using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NetworkGamePlayer : NetworkBehaviour
{
    
    [SyncVar]
    public string displayName = "Loading...";
    

    private bool isLeader;
    

    private CustomNetworkManager room;
    private CustomNetworkManager Room
    {
        get
        {
            if (room != null)
            {
                return room;
            }

            return room = NetworkManager.singleton as CustomNetworkManager;
        }
    }
    
    public override void OnStartClient()
    {
        DontDestroyOnLoad(gameObject);
        Room.GamePlayers.Add(this);
        
    }
    private void OnDestroy()
    {
        Room.GamePlayers.Remove(this);
    }

    [Server]
    public void SetDisplayName(string displayName)
    {
        this.displayName = displayName;
    }
}
