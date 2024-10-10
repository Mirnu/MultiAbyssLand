using System;
using Assets.Scripts.Resources.Crafting;
using Assets.Scripts.Resources.Data;
using Mirror;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.World.Blocks
{
    public class Block : MonoBehaviour
    {
        public UnityEvent OnLeftClick;
        public UnityEvent OnRightClick;
        public UnityEvent OnMiddleClick;
        public UnityEvent OnDestroyed;
        public UnityEvent OnFixedUpdate;
        public UnityEvent OnDamaged;

        public RecipeComponent resource;
        
        protected SpriteRenderer _renderer;

        private void Awake() {
            _renderer = GetComponent<SpriteRenderer>();
            //_renderer.sprite = resource.SpriteInInventary;
        }

        public void Destroy()
        {
            OnDestroyed?.Invoke();
        }

    }

    [Serializable]
    public class BlockInWorld {
        public Block block;
        public Vector2 pos;
    }
}
