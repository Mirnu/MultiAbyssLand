using Assets.Scripts.Player.Data;
using System;
using UnityEngine;

namespace Assets.Scripts.Player.Components.Handlers
{
    public class PlayerDeathHandler : PlayerComponent
    {
        [SerializeField] private PlayerStats _stats;

        public override void ServerInitialize()
        {
            _stats.HealthChanged += OnHealthChanged;
        }

        private void OnHealthChanged(int health)
        {
            if (health == 0)
            {

            }
        }
    }
}