# PlayTodayChatAppChallenge


Things to note:



[MAIN MENU]
The Join button....
The Host button ...

enabled when the input field...

DefaultScene component in NetworkManager opens the specified Scene when the ClientManager.StartConnection method is called. In this case, it is the Chat scene (Scene Index 1 in the Build Settings).

PlayerSpawner component in NetworkManager spawns the specifed player prefab to the scene when a client connects to the server.

NetworkObject component is attached to Player GameObject, as the Player component implements NetworkBehaviour.

NetworkBehaviour - https://fish-networking.gitbook.io/docs/manual/guides/network-behaviour-guides

The [SyncBar] attribute
- If the value of the variable changes on the server, the change gets applied to the connected clients.

The [ServerRpc] attribute allows code to be executed on the server side from the client.

[CHAT SCENE]

Inside the Chat Scene is a GameManager object. This is a singleton object that syncs data to all the players.

