using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

//facade
public class Player : NetworkBehaviour
{
    // facade
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Player started: " + this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
