using Assets.Scripts.World.Blocks;
using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;
using UnityEngine;
using Mirror;
using System.Linq;
using UnityEngine.Events;

namespace Assets.Scripts.World.Managers {
    // 1 система блоков
    public class FirstTypeManager : NetworkBehaviour
    {
        [SerializeField] private List<InteractableGO> blocks = new List<InteractableGO>();

        private static FirstTypeManager _singleton;

        public static FirstTypeManager Singleton => _singleton;

        public override void OnStartServer()
        {
            if (_singleton == null)
            {
                _singleton = this;
            }
            blocks.ForEach(x => { 
                RegisterBlock(x.Go, x.Pos, out Block inWorld);
                x.Init(delegate { inWorld.transform.Rotate(0, 0, 25); x.Damage(1); }, delegate{ Debug.LogWarning("DESTROY"); Destroy(inWorld.gameObject); }, inWorld);
            });

            base.OnStartServer();
        }

        public void RegisterBlock(Block orig, Vector2 pos, out Block inWorld) {
            var l = Instantiate(orig, pos, orig.transform.rotation);
            NetworkServer.Spawn(l.gameObject);
            inWorld = l;
        }

        [Command(requiresAuthority = false)]
        public void AnyClickCmd(Vector2 mousePos2D, float m) {
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if(hit.collider != null && blocks.Any(x => x.Pos == hit.collider.transform.position)) {
                switch (m) {
                    case 0 : { 
                        blocks.Find(x =>  x.Pos == hit.collider.transform.position).Go.OnLeftClick?.Invoke();
                        break; 
                    }
                    case 1 : {
                        blocks.Find(x =>  x.Pos == hit.collider.transform.position).Go.OnRightClick?.Invoke();
                        break; 
                    }
                    case 2 : {
                        blocks.Find(x =>  x.Pos == hit.collider.transform.position).Go.OnMiddleClick?.Invoke();
                        break; 
                    }
                }
            }
        }
    }

    [Serializable]
    public class InteractableGO {
        public Block Go;
        public int Health;
        public int MaxHealth;

        public Action OnLeftClick;
        public Action OnRightClick;
        public Vector3 Pos;

        public void Init(Action onDamaged, Action onDestroyed, Block inWorld) {
            Go = inWorld;
            Health = MaxHealth;
            Go.OnLeftClick.AddListener(delegate {onDamaged?.Invoke();});
            Go.OnDestroyed.AddListener(delegate {onDestroyed?.Invoke();});
        }

        public void Damage(int amount) {
            Debug.LogWarning("NIGGER:" + amount);
            if(Health > amount) { OnLeftClick?.Invoke(); Health -= amount; }
            else { Go.OnDestroyed?.Invoke(); }
        }
    }
}