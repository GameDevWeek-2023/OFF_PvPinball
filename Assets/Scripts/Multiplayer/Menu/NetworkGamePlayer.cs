using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class NetworkGamePlayer : NetworkBehaviour
{
    
    [SyncVar]
    public string displayName = "Loading...";
    

    private bool isLeader;

    public ServerLeverManager serverLeverManager;
    

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
    
    public override void OnStartServer()
    {
        base.OnStartServer();
        //serverLeverManager = FindObjectOfType<ServerLeverManager>();
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
    /*
   void OnLeftTrigger(InputValue value)
    {
        float val = value.Get<float>();
        if (val > 0)
        {
            FindObjectOfType<AudioManager>().Play("FlipperUp");
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("FlipperDown");
        }
        print("sending command");
        CmdLeftTrigger(val);
    }
    
    
    void OnLeftTriggerLayer2(InputValue value)
    {
        float val = value.Get<float>();
        if (val > 0)
        {
            FindObjectOfType<AudioManager>().Play("FlipperUp");
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("FlipperDown");
        }
        print("sending command");
        CmdLeftTriggerLayer2(val);
    }
    
    
    void OnRightTrigger(InputValue value)
    {
        float val = value.Get<float>();
        if (val > 0)
        {
            FindObjectOfType<AudioManager>().Play("FlipperUp");
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("FlipperDown");
        }
        print("sending command");
        CmdRightTrigger(val);
    }
    
    
    void OnRightTriggerLayer2(InputValue value)
    {
        float val = value.Get<float>();
        if (val > 0)
        {
            FindObjectOfType<AudioManager>().Play("FlipperUp");
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("FlipperDown");
        }
        print("sending command");
        CmdRightTriggerLayer2(val);
    }
    
    
    [Command]
    public void CmdLeftTrigger(float val)
    {
        serverLeverManager.LeftTrigger(val);
    }

    [Command]
    public void CmdLeftTriggerLayer2(float val)
    {
        serverLeverManager.LeftTriggerL2(val);
    }

    [Command]
    public void CmdRightTrigger(float val)
    {
        serverLeverManager.RightTrigger(val);
    }

    [Command]
    public void CmdRightTriggerLayer2(float val)
    {
        serverLeverManager.RightTriggerL2(val);
    }
    
    */
}
