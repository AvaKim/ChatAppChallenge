using UnityEngine;
using System.Collections.Generic;
using FishNet.Object;
using FishNet.Object.Synchronizing;

public class Player : NetworkBehaviour
{
    [SyncVar] public string playerName;
    [SyncVar] private List<string> _chatHistory;
    
    #region NetworkBehaviour Callbacks
    // https://fish-networking.gitbook.io/docs/manual/guides/network-behaviour-guides
    
    public override void OnStartServer()
    {
        base.OnStartServer();
        ChatManager.Instance.players.Add(this);
    }
    
    public override void OnStopServer()
    {
        base.OnStopServer();
        ChatManager.Instance.players.Remove(this);
    }

    
    // Called on joining the server to receive the chat history from server
    [ServerRpc]
    public void LoadChatHistory(List<string> data)
    {
        _chatHistory = data;
    }
    
    #endregion

}
