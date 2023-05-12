using System;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using FishNet.Managing;
using System.Collections.Generic;
using FishNet;
using FishNet.Broadcast;
using FishNet.Connection;
using UnityEngine;

public class ChatManager : NetworkBehaviour
{
    private NetworkManager _networkManager;
    private ChatHUD chatHUD;
    public static ChatManager Instance { get; private set; } // singleton

    // List of messages
    [SyncObject] private readonly SyncList<Message> messageList = new ();
    
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

    public void SendChatMessage(string sender, string message)
    {
        Message msg = new Message
        {
            text = message,
            // timestamp = System.DateTime.Now.Hour + ":" + System.DateTime.Now.Minute,
            // sender = sender
        };
        messageList.Add(msg);

        if(InstanceFinder.IsServer)
        {
            InstanceFinder.ServerManager.Broadcast(msg);
        }
        else
        {
            InstanceFinder.ClientManager.Broadcast(msg);
        }
    }

    private void OnEnable()
    {
        InstanceFinder.ClientManager.RegisterBroadcast<Message>(OnServerMessageReceived);
        InstanceFinder.ServerManager.RegisterBroadcast<Message>(OnClientMessageReceived);
    }

    private void OnDisable()
    {
        InstanceFinder.ClientManager.UnregisterBroadcast<Message>(OnServerMessageReceived);
        InstanceFinder.ServerManager.UnregisterBroadcast<Message>(OnClientMessageReceived);
    }

    /// <summary>
    /// Called on client side when received a message from server
    /// </summary>
    /// <param name="msg"></param>
    private void OnServerMessageReceived(Message msg)
    {
        chatHUD.UpdateChatPanel(msg);
    }
    
    /// <summary>
    /// Called on server side when received a message from client
    /// </summary>
    /// <param name="networkConnection">username and other sensitive player data should be passed through NetworkConnection later on</param>
    /// <param name="msg"></param>
    private void OnClientMessageReceived(NetworkConnection networkConnection, Message msg)
    {
        InstanceFinder.ServerManager.Broadcast(msg);
    }


    private void Start()
    {
        _networkManager = FindObjectOfType<NetworkManager>();
        if (_networkManager == null)
        {
            Debug.LogError("NetworkManager not found, Chat will not function.");
            return;
        }
        
        chatHUD = FindObjectOfType<ChatHUD>();
        if (chatHUD == null)
        {
            Debug.LogError("Chat HUD not found, Chat will not function.");
            return;
        }
        
    }
}

public struct Message : IBroadcast
{
    public string text;
    // public string timestamp;
    // public string sender;
}
