using Assets.Scripts.World.Blocks;
using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;
using UnityEngine;
using Mirror;

namespace Assets.Scripts.World.Managers {
    // 1 система блоков
    public class FirstTypeManager : NetworkBehaviour
    {
        private List<InteractableGO> _interactables = new List<InteractableGO>();
        private Block _abstractBlock;
        //private Resource _res;

        public FirstTypeManager(List<InteractableGO> interactables, Block block) {
            _interactables = interactables;
            _abstractBlock = block;
        }

        // public void Place(Resource resource, Vector2 pos)
        // {
        //     _ = new InteractableGO(delegate {}, delegate {}, resource, Object.Instantiate(_abstractBlock), pos);
        //     Debug.Log("Placed: " + resource);

        //     // return false;
        // }

        public void Initialize()
        {
            _interactables.ForEach(x => { 
                // Типа рофл плэйсхолдер пон да?
                // x.Init(delegate { x.Go.transform.Rotate(0, 0, 25); }, 
                //     delegate{ Object.Destroy(x.Go.gameObject); });
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

        public InteractableGO(Action onDamaged, Action onDestroyed, Block go, Vector3 pos) {
            Health = MaxHealth;
            Go = go;
            Go.transform.position = pos;
            Go.OnLeftClick.AddListener(delegate {onDamaged?.Invoke();});
            Go.OnDestroyed.AddListener(delegate {onDestroyed?.Invoke();});
        }

        public void Damage(int amount) {
            if(Health > amount) { OnDamaged?.Invoke(); }
            else { OnDestroyed?.Invoke(); }
        }
    }
}