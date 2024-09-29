using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.World.Blocks;
using Mirror;
using UnityEngine;

namespace Assets.Scripts.World.Blocks {
    public class BlockManager : NetworkBehaviour {

        [SerializeField] private List<BlockInWorld> blocks = new List<BlockInWorld>();

        private static BlockManager _singleton;

        public static BlockManager Singleton => _singleton;

        public override void OnStartServer()
        {
            if (_singleton == null)
            {
                _singleton = this;
            }
            blocks.ForEach(x => RegisterBlock(x.block, x.pos));
            base.OnStartServer();
        }

        public void RegisterBlock(Block block, Vector2 pos) {
            var l = Instantiate(block, pos, block.transform.rotation);
            l.OnLeftClick.AddListener(delegate { Debug.Log("NIGGER BALLS"); });
            NetworkServer.Spawn(l.gameObject);
        }

        [Command(requiresAuthority = false)]
        public void AnyClickCmd(Vector2 mousePos2D, float m) {
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if(hit.collider != null && blocks.Any(x => x.pos == (Vector2)hit.collider.transform.position)) {
                Debug.Log("found a block: " + hit.collider.name + " : " + m);
                switch (m) {
                    case 0 : { 
                        blocks.Find(x =>  x.pos == (Vector2)hit.collider.transform.position).block.OnLeftClick?.Invoke();
                        break; 
                    }
                    case 1 : {
                        blocks.Find(x =>  x.pos == (Vector2)hit.collider.transform.position).block.OnRightClick?.Invoke();
                        break; 
                    }
                    case 2 : {
                        blocks.Find(x =>  x.pos == (Vector2)hit.collider.transform.position).block.OnMiddleClick?.Invoke();
                        break; 
                    }
                }
                
            }
        }

    }
}