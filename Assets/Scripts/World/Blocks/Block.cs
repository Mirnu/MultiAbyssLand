using System;
using Assets.Scripts.Resources.Data;
using UnityEngine;

namespace Assets.Scripts.World.Blocks
{
    public class Block : MonoBehaviour
    {
        public event Action OnLeftClick;
        public event Action OnRightClick;
        public event Action OnMiddleClick;
        public event Action OnDestroyed;

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
