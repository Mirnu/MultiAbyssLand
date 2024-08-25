using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.World {
    [Serializable]
    public class DmgTile {
        public int value;
        public Vector3Int Pos;

        public TileBase _default;
        public TileBase _dead;

        public int MaxHealth;

        public Action onDestroyed;

        private int _currentHealth;

        
        public void Init()
        {
            _currentHealth = MaxHealth;
        }

        public void Damage(int amount) {
            _currentHealth -= amount;
            if(_currentHealth <= 0) {
                onDestroyed?.Invoke();
            }
        }
    }
}
