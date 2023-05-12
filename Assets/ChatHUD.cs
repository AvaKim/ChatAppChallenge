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
        
    }

    private void OnDropdownValueChanged(TMP_Dropdown dropdown)
    {
        chatText.text = dropdown.captionText.text;
    }
}
