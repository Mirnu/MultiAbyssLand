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

    public void Entry() {
        Debug.Log("WorldEntrypoint");
        GetComponent<WorldFacade>().Generate( "test");
    }
}
