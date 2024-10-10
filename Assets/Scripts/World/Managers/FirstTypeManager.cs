using Assets.Scripts.World.Blocks;
using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;
using UnityEngine;
using Mirror;
using System.Linq;
using UnityEngine.Events;
using Assets.Scripts.Resources.Crafting;

namespace Assets.Scripts.World.Managers {
    // 1 система блоков
    public class FirstTypeManager : NetworkBehaviour
    {
        [SerializeField] private List<InteractableGO> blocks = new List<InteractableGO>();
        [SerializeField] private BlockOnGround blockOnGroundPrefab;

        private static FirstTypeManager _singleton;

        public static FirstTypeManager Singleton => _singleton;

        public override void OnStartServer()
        {
            if (_singleton == null)
            {
                _singleton = this;
            }
            blocks.ForEach(x => { 
                RegisterBlock(x.Go, x.Pos, x);
            });

            base.OnStartServer();
        }

        // моя тупить
        // public void Place(Block orig, Vector2 pos, InteractableGO iGo) {
        //     var l = Instantiate(orig, pos, orig.transform.rotation);
        //     NetworkServer.Spawn(l.gameObject);
        //     iGo = new InteractableGO(delegate { l.transform.Rotate(0, 0, 10); iGo.Damage(1); }, delegate{ Destroy(l.gameObject); }, l);
        //     blocks.Add(iGo);
        // }

        public void RegisterBlock(Block orig, Vector2 pos, InteractableGO iGo) {
            var l = Instantiate(orig, pos, orig.transform.rotation);
            NetworkServer.Spawn(l.gameObject);
            iGo.Init(delegate { iGo.Damage(1); }, delegate{ DropBlock(orig.resource, pos); NetworkServer.Destroy(l.gameObject); blocks.Remove(iGo); }, l);
        }

        public void DropBlock(RecipeComponent drop, Vector2 pos) {
            var l = Instantiate(blockOnGroundPrefab, pos, Quaternion.identity);
            l.SetComponent(drop);
            NetworkServer.Spawn(l.gameObject);
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

        public Vector3 Pos;

        public void Init(Action onDamaged, Action onDestroyed, Block inWorld) {
            Go = inWorld;
            Health = MaxHealth;
            Go.OnLeftClick.AddListener(delegate {onDamaged?.Invoke();});
            Go.OnDestroyed.AddListener(delegate {onDestroyed?.Invoke();});
        }

        public InteractableGO(Action onDamaged, Action onDestroyed, Block inWorld) {
            Go = inWorld;
            Health = MaxHealth;
            Go.OnLeftClick.AddListener(delegate {onDamaged?.Invoke();});
            Go.OnDestroyed.AddListener(delegate {onDestroyed?.Invoke();});
        }

        public void Damage(int amount) {
            Go.OnDamaged?.Invoke();
            if(Health > amount) { Health -= amount; }
            else { Go.OnDestroyed?.Invoke(); }
        }
    }
}