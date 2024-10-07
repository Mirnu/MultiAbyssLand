using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.World;
using Mirror;
using UnityEngine;

public class WorldEntrypoint : NetworkBehaviour
{
    // Update is called once per frame
    void Update()
    {
        
    }

    [Server]
    private void Start()
    {
        GetComponent<WorldFacade>().Generate("test");
    }

    public void Entry() {
        Debug.Log("WorldEntrypoint");
        
    }
}
