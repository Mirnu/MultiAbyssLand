using Assets.Scripts.Player.Components.Controllers;
using Assets.Scripts.Player.Data;
using Mirror;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player.Components.Handlers
{
    public class PlayerHealthHandler : PlayerComponent
    {
        [SerializeField] private PlayerStats _stats;
        [SerializeField] private PlayerStatsMax _statsMax;
        [SerializeField] private HealController _heal;

        private const string FOOD_HEAL = "food";

        [Server]
        private void OnDestroy()
        {
            _stats.HealthChanged -= OnHealthChanged;
        }

        public override void ServerInitialize()
        {
            _stats.HealthChanged += OnHealthChanged;
            OnHealthChanged(_stats.Health);
        }

        [Server]
        private void OnHealthChanged(int health)
        {
            if (health < _statsMax.HealthMax && _stats.Food > 70
                && !_heal.IsHealedByName(FOOD_HEAL))
            {
                _heal.HealByTime(FOOD_HEAL, 2, 4);
            }
            if (health == _statsMax.HealthMax)
            {
                _heal.StopHealByName(FOOD_HEAL);
            }
        }
    }
}