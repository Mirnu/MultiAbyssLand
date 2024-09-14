using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.World.Blocks;
using Mirror;
using UnityEngine;

public class BlockManager : NetworkBehaviour {

    [SerializeField] private List<Block> blocks = new List<Block>();

    public override void OnStartServer()
    {
        base.OnStartServer();
    }

    public bool TryGetBlockAtPos(Vector2 pos, out Block block) {
        if(!blocks.Any(x => (Vector2)x.transform.position == pos)) { block = null; return false; }
        block = blocks.Find(x => (Vector2)x.transform.position == pos);
        return true;
    }
}