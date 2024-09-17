using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.World.Blocks;
using Mirror;
using UnityEngine;

namespace Assets.Scripts.World.Blocks {
    public class BlockManager : NetworkBehaviour {

        [SerializeField] private List<Block> blocks = new List<Block>();

        private static BlockManager _singleton;

        public static BlockManager Singleton => _singleton;

        public override void OnStartServer()
        {
            if (_singleton == null)
            {
                _singleton = this;
            }
            // блять я сосал меня ебали
            blocks = FindObjectsByType<Block>(FindObjectsSortMode.None).ToList();
            base.OnStartServer();
        }

        [Command(requiresAuthority = false)]
        public void AnyClickCmd(Vector2 mousePos2D, float m) {
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if(hit.collider != null && blocks.Any(x => x.transform.position == hit.collider.transform.position)) {
                Debug.Log("found a block: " + hit.collider.name + " : " + m);
                switch (m) {
                    case 0 : { 
                        blocks.Find(x => x.transform.position == hit.collider.transform.position).OnLeftClick?.Invoke();
                        break; 
                    }
                    case 1 : {
                        blocks.Find(x => x.transform.position == hit.collider.transform.position).OnRightClick?.Invoke();
                        break; 
                    }
                    case 2 : {
                        blocks.Find(x => x.transform.position == hit.collider.transform.position).OnMiddleClick?.Invoke();
                        break; 
                    }
                }
                
            }
        }

        public bool TryGetBlockAtPos(Vector2 pos, out Block block) {
            if(!blocks.Any(x => (Vector2)x.transform.position == pos)) { block = null; return false; }
            block = blocks.Find(x => (Vector2)x.transform.position == pos);
            return true;
        }
    }
}