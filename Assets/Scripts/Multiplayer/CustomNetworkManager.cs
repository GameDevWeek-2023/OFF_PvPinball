using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mirror;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomNetworkManager : NetworkManager
{
    [SerializeField] private int minPlayers = 2;

    [Header("Room")] 
    [SerializeField] private NetworkRoomPlayer roomPlayerPrefab = null;
    [Header("Game")]
    [SerializeField] private NetworkGamePlayer gamePlayerPrefab = null;
    
    public static event Action OnClientConnected;
    public static event Action OnClientDisconnected;

    [SerializeField]public List<NetworkRoomPlayer> RoomPlayers { get; } = new List<NetworkRoomPlayer>();
    [SerializeField]public List<NetworkGamePlayer> GamePlayers { get; } = new List<NetworkGamePlayer>();


    public override void OnStartServer()
    {
        spawnPrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs").ToList();
    }

    public override void OnStartClient()
    {
        //var spawnablePrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs");
        spawnPrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs").ToList();
    }

    public override void OnClientConnect()
    {
        base.OnClientConnect();
        OnClientConnected?.Invoke();
    }
    

    public override void OnClientDisconnect()
    {
        base.OnClientDisconnect();
        OnClientDisconnected?.Invoke();
    }

    public override void OnServerConnect(NetworkConnectionToClient conn)
    {
        if (numPlayers >= maxConnections)
        {
            conn.Disconnect();
            return;
        }

        /*if (SceneManager.GetActiveScene().name != menuScene)
        {
            conn.Disconnect();
            return;
        }*/
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        if (conn.identity != null)
        {
            var player = conn.identity.GetComponent<NetworkRoomPlayer>();

            RoomPlayers.Remove(player);

            NotifyPlayersOfReadyState();
        }
        
        base.OnServerDisconnect(conn);
    }

    public void NotifyPlayersOfReadyState()
    {
        foreach (var player in RoomPlayers)
        {
            player.HandleReadyToStart(IsReadyToStart());
        }
    }

    private bool IsReadyToStart()
    {
        if (numPlayers < minPlayers)
        {
            return false;
        }

        foreach (var player in RoomPlayers)
        {
            if (!player.IsReady)
            {
                return false;
            }
        }

        return true;
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        bool isLeader = RoomPlayers.Count == 0;
        NetworkRoomPlayer roomPlayerInstance = Instantiate(roomPlayerPrefab);
        roomPlayerInstance.IsLeader = isLeader;
        NetworkServer.AddPlayerForConnection(conn, roomPlayerInstance.gameObject);
    }

    public override void OnStopServer()
    {
        RoomPlayers.Clear();
    }

    public void StartGame()
    {
        if (!IsReadyToStart())
        { 
            return;
        }
        ServerChangeScene("Scene_Map_01");
        
    }

    public override void ServerChangeScene(string newSceneName)
    {
        //From Menu to Game
        for (int i = RoomPlayers.Count - 1; i >= 0; i--)
        {
            var conn = RoomPlayers[i].connectionToClient;
            var gamePlayerInstance = Instantiate(gamePlayerPrefab);
            gamePlayerInstance.SetDisplayName(RoomPlayers[i].DisplayName);
            
            NetworkServer.Destroy(conn.identity.gameObject);

            NetworkServer.ReplacePlayerForConnection(conn, gamePlayerInstance.gameObject);
        }
        base.ServerChangeScene(newSceneName);
    }
}
