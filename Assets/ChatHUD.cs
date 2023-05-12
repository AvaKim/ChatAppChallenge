using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FishNet.Managing;
using FishNet.Object;
using FishNet.Object.Synchronizing;

public class ChatHUD : MonoBehaviour
{

    private NetworkManager _networkManager;



    [SerializeField]
    private GameObject chatTextUI; // to instantiate new text ui instance
    [SerializeField]
    private Transform chatPanel; // to spawn the new text UI under
    [SerializeField]
    private TMP_Text chatText;
    
    [SerializeField]
    private TMP_Dropdown chatOptionDropdown;
    
    // Start is called before the first frame update
    void Start()
    {
        // _networkManager = FindObjectOfType<NetworkManager>();
        // if (_networkManager == null)
        // {
        //     Debug.LogError("NetworkManager not found, HUD will not function.");
        //     return;
        // }
        //
        
        
        chatOptionDropdown.onValueChanged.AddListener(delegate { OnDropdownValueChanged(chatOptionDropdown); });
    }

    // Update is called once per frame
    void Update()
    {
        // // Only execute on the local client side
        // if (!IsOwner)
        //     return;
        
        if (Input.GetKeyDown(KeyCode.Return)) // On Enter key
        {
            // Send msg to server
            ChatManager.Instance.SendChatMessage("name",chatText.text);
            
        }
        
    }

    /// <summary>
    /// Updates Chat HUD so that new message gets displayed on client.
    /// </summary>
    /// <param name="msg"></param>
    public void UpdateChatPanel(Message msg)
    {
        GameObject newTextUI = Instantiate(chatTextUI, chatPanel.transform); 
        newTextUI.GetComponent<TMP_Text>().text = chatText.text;

    }

    public void OnMessageReceived(string message)
    {

    }
    private void OnDropdownValueChanged(TMP_Dropdown dropdown)
    {
        chatText.text = dropdown.captionText.text;
    }
}
