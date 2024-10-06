using Assets.Scripts.World.Blocks;
using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;
using UnityEngine;
using Mirror;
using System.Linq;

namespace Assets.Scripts.World.Managers {
    // 1 система блоков
    public class FirstTypeManager : NetworkBehaviour
    {
        [SerializeField] private Dictionary<InteractableGO, BlockInWorld> blocks = new Dictionary<InteractableGO, BlockInWorld>();

        private static FirstTypeManager _singleton;

        public static FirstTypeManager Singleton => _singleton;

        public override void OnStartServer()
        {
            if (_singleton == null)
            {
                _singleton = this;
            }
            //blocks.ForEach(x => RegisterBlock(x.Go, x.pos));
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
            if(hit.collider != null && blocks.Keys.ToList().Any(x => x.pos == hit.collider.transform.position)) {
                Debug.Log("found a block: " + hit.collider.name + " : " + m);
                switch (m) {
                    case 0 : { 
                        blocks.Keys.ToList().Find(x =>  x.pos == hit.collider.transform.position).Go.OnLeftClick?.Invoke();
                        break; 
                    }
                    case 1 : {
                        blocks.Keys.ToList().Find(x =>  x.pos == hit.collider.transform.position).Go.OnRightClick?.Invoke();
                        break; 
                    }
                    case 2 : {
                        blocks.Keys.ToList().Find(x =>  x.pos == hit.collider.transform.position).Go.OnMiddleClick?.Invoke();
                        break; 
                    }
                }
                
            }
        }

        public void Initialize()
        {
            foreach(var x in blocks)
            {
                x.Key.Init(delegate { x.Key.Go.transform.Rotate(0, 0, 25); }, 
                    delegate{ Object.Destroy(x.Key.Go.gameObject); }, x.Value.block, x.Value.pos);
                //Console.WriteLine($"key: {person.Key}  value: {person.Value}");
            }
            blocks.Keys.ToList().ForEach(x => { 
                // Типа рофл плэйсхолдер пон да?
                
            });
        }
    }

    [Serializable]
    public class InteractableGO {
        public Block Go;
        public int Health;
        public int MaxHealth;

        public Action OnDamaged;
        public Action OnDestroyed;
        public Vector3 pos { get { return Go.transform.position; } set { Go.transform.position = value; } }

        public void Init(Action onDamaged, Action onDestroyed, Block go, Vector3 pos) {
            Go = Object.Instantiate(go, pos, Quaternion.identity);
            Go.OnLeftClick.AddListener(delegate {onDamaged?.Invoke();});
            Go.OnDestroyed.AddListener(delegate {onDestroyed?.Invoke();});
        }

        // public InteractableGO(Action onDamaged, Action onDestroyed, Block go, Vector3 pos) {
        //     Health = MaxHealth;
        //     Go = go;
        //     Go.transform.position = pos;
        //     Go.OnLeftClick.AddListener(delegate {onDamaged?.Invoke();});
        //     Go.OnDestroyed.AddListener(delegate {onDestroyed?.Invoke();});
        // }

        public void Damage(int amount) {
            if(Health > amount) { OnDamaged?.Invoke(); }
            else { OnDestroyed?.Invoke(); }
        }
    }
}