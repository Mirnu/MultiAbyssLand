using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Assets.Scripts.Player;

public class MALNetworkManager : NetworkManager
{
    [SerializeField] private WorldEntrypoint worldEntrypoint;
    [SerializeField] private PlayerEntrypoint playerEntrypoint;

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        Debug.Log("Adding new player: " + conn);
        base.OnServerAddPlayer(conn);
    }

    public override void OnStartServer()
    {
        worldEntrypoint.Entry();
        Debug.Log("Server started");
        base.OnStartServer();
    }

}
