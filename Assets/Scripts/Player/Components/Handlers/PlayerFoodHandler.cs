using Assets.Scripts.ILifeCycle;
using Assets.Scripts.Player.Components.Controllers;
using Assets.Scripts.Player.Data;
using Mirror;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player.Components.Handlers
{
    public class PlayerFoodHandler : PlayerComponent
    {
        [SerializeField] private PlayerStats _stats;
        [SerializeField] private PlayerBoost _boost;
        [SerializeField] private HealController _heal;

        private bool isRecoveryHealth = false;
        private const string FOOD_HEAL = "food";

        [Server]
        private void OnDestroy()
        {
            _stats.FoodChanged -= OnFoodChanged;
        }

        public override void ServerInitialize()
        {
            _stats.FoodChanged += OnFoodChanged;
        }

        [Server]
        private void OnFoodChanged(int food)
        {
            _boost.SpeedBoost = food > 30 ? 1 : 0.5f;

            if (food == 0)
            {
                StartCoroutine(startStarve());
            }
            else 
            {
                StopCoroutine(startStarve());
            }

            if (food <= 70)
            {
                _heal.LockByName(FOOD_HEAL);
            }
            else
            {
                _heal.UnLockByName(FOOD_HEAL);
            }
        }

        [Server]
        private IEnumerator startStarve()
        {
            WaitForSeconds delta = new WaitForSeconds(2);
            while (true)
            {
                yield return delta;

                if (_stats.Health > 3)
                    _stats.Health--;
            }
        }
    }
}