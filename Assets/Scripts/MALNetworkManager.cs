using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Assets.Scripts.Player;

public class MALNetworkManager : NetworkManager
{
    // [SerializeField] private WorldEntrypoint worldEntrypoint;
    //[SerializeField] private PlayerEntryPoint playerEntrypoint;z
    [SerializeField] private Texture2D _cursor;

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);
    }

    public override void OnStartServer()
    {
       // worldEntrypoint.Entry();
        base.OnStartServer();
    }

    private void OnEnable()
    {
        Cursor.SetCursor(_cursor, Vector2.zero, CursorMode.Auto);
    }
}
