using UnityEngine;
using FishNet.Object;
using FishNet.Object.Synchronizing;

public class Player : NetworkBehaviour
{
    [SyncVar] public string username;
}
