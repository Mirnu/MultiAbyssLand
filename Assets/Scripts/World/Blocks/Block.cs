using System;
using Assets.Scripts.Resources.Data;
using Mirror;
using UnityEngine;

namespace Assets.Scripts.World.Blocks
{
    public class Block : NetworkBehaviour
    {
        public Action OnLeftClick;
        public Action OnRightClick;
        public Action OnMiddleClick;
        public Action OnDestroyed;

        public Resource resource;
        public int Health { get; private set; }
        
        private SpriteRenderer _renderer;

        private void Awake() {
            _renderer = GetComponent<SpriteRenderer>();
            _renderer.sprite = resource.SpriteInInventary;
        }

        public void Destroy()
        {
            OnDestroyed?.Invoke();
        }
    }
}
