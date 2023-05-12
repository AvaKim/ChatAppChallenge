using FishNet.Object;
using FishNet.Object.Synchronizing;
using FishNet.Managing;
using System.Collections.Generic;
using UnityEngine;

public class ChatManager : NetworkBehaviour
{
    private NetworkManager _networkManager;
    public static ChatManager Instance { get; private set; } // singleton
    
    // Entire chat record of the server during application runtime. Destroyed on scene exit.
    [SyncObject] public readonly SyncList<ChatHistory> chatHistory = new();
    
    // List of all connected players
    [SyncObject] public readonly SyncList<Player> players = new();

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
        {
            Debug.LogWarning("ChatManager instance already exists");
            Destroy(this);
        }
    }

    private void Start()
    {
        _networkManager = FindObjectOfType<NetworkManager>();
        if (_networkManager == null)
        {
            Debug.LogError("NetworkManager not found, Chat will not function.");
            return;
        }
    }

    // private override void OnStartClient()
    // {
    //     _networkManager.ClientManager.RegisterBroadcast<PlayerInfo>(OnPlayerJoined);
    // }
    //
    // private override void OnStopClient()
    // {
    //     _networkManager.ClientManager.UnregisterBroadcast<PlayerInfo>(OnPlayerJoined);
    // }


    // private void OnPlayerJoined(string playerName)
    // {
    //     Debug.Log($"Player {playerName} has joined");
    // }
}



public class ChatHistory : NetworkBehaviour
{
    
}
