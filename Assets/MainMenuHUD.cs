using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FishNet.Managing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using FishNet.Broadcast;
using FishNet.Managing.Server;
using FishNet.Object;
using FishNet.Transporting;
using Ping = System.Net.NetworkInformation.Ping;

public class MainMenuHUD : MonoBehaviour
{
    private NetworkManager _networkManager;
    [SerializeField] private string playerName;

    [SerializeField] private Button joinButton;
    [SerializeField] private Button hostButton;
    [SerializeField] private TMP_InputField playerNameField;

    // Start is called before the first frame update
    private void Start()
    {
        _networkManager = FindObjectOfType<NetworkManager>();
        if (_networkManager == null)
        {
            Debug.LogError("NetworkManager not found, HUD will not function.");
            return;
        }

        joinButton.onClick.AddListener(OnJoinButtonClick);
        hostButton.onClick.AddListener(OnHostButtonClick);
        playerNameField.onValueChanged.AddListener(OnPlayerNameChanged);
    }
    
    
    /// <summary>
    /// Invoked when the value of the text field changes.
    /// Disables the buttons if the player name is invalid.
    /// </summary>
    private void OnPlayerNameChanged(string str)
    {
        bool isValidName = hostButton.interactable = joinButton.interactable = CheckUsernameValidity();
        if (isValidName)
        {
            playerName = str;
        }
        
        bool CheckUsernameValidity() => str.Length > 3; // player name rules
    }

    private void OnJoinButtonClick()
    {
        JoinServer();
    }
    
    private void OnHostButtonClick()
    {
        StartServer();
    }

    private void StartServer()
    {
        Debug.Log("Starting Server");
        _networkManager.ServerManager.StartConnection();
        JoinServer();
    }

    private void JoinServer()
    {
        Debug.Log("Connecting to the server");
        _networkManager.ClientManager.StartConnection();
    }

}
