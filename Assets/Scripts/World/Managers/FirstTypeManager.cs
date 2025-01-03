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
            
            base.OnStartServer();
        }

        public void AddToBlocks(Block pref, Vector2 pos, Sprite s) {
            var i = new InteractableGO();
            i.Go = pref;
            i.Go.GetComponent<SpriteRenderer>().sprite = s;
            i.Pos = pos;
            i.MaxHealth = pref.MaxHealth;
            blocks.Add(i);
        }

        public void Init() {
            blocks.ForEach(x => { 
                RegisterBlock(x.Go, x.Pos, x);
            });
        }

        // моя тупить
        public void Place(Block orig, Vector2 pos, InteractableGO iGo) {
            var l = Instantiate(orig, pos, orig.transform.rotation);
            NetworkServer.Spawn(l.gameObject);
            iGo = new InteractableGO(delegate { l.transform.Rotate(0, 0, 10); iGo.Damage(1); }, delegate{ Destroy(l.gameObject); }, l);
            blocks.Add(iGo);
        }

        [ServerCallback]
        public void RegisterBlock(Block orig, Vector2 pos, InteractableGO iGo) {
            iGo.Init(delegate { iGo.Damage(1); orig.GetComponent<AudioSource>().Play(); }, delegate{ orig.GetComponent<AudioSource>().Play(); DropBlock(orig.resource, pos); Destroy(orig.gameObject); blocks.Remove(iGo); }, orig);
        }

        public void DropBlock(RecipeComponent drop, Vector2 pos) {
            var l = Instantiate(blockOnGroundPrefab, pos, Quaternion.identity);
            l.SetComponent(drop);
            NetworkServer.Spawn(l.gameObject);
        }

        [Command(requiresAuthority = false)]
        public void LeftClick(float m, GameObject tool) {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && blocks.Any(x => x.Go == hit.collider.GetComponent<Block>())
             //&& Vector2.Distance(tool.transform.position, hit.collider.transform.position) < 2
             ) {
                Debug.LogWarning("hit block: " + hit.collider.name);
                blocks.Find(x => x.Go == hit.collider.GetComponent<Block>()).Go.OnLeftClick?.Invoke();
            }
        }

        public void DamageBlock(Block block) {
            blocks.Find(x => x.Go == block).Go.OnLeftClick?.Invoke();
        }

        public void PutBlock(Vector2 pos, Block orig) {
            var i = new InteractableGO();
            i.Go = Instantiate(orig, pos, Quaternion.identity);
            NetworkServer.Spawn(i.Go.gameObject);
            i.Pos = pos;
            i.MaxHealth = orig.MaxHealth;
            blocks.Add(i);
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

        public InteractableGO() {}

        public void Damage(int amount) {
            //Go.OnDamaged?.Invoke();
            if(Health > amount) { Health -= amount; }
            else { Go.OnDestroyed?.Invoke(); }
        }
    }
}