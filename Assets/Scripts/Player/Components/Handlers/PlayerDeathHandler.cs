using Assets.Scripts.Player.Data;
using Mirror;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player.Components.Handlers
{
    public class PlayerDeathHandler : PlayerComponent
    {
        [SerializeField] private PlayerStats _stats;
        [SerializeField] private GameObject _character;
        [SerializeField] private GameObject _controllers;

        public override void ServerInitialize()
        {
            _stats.HealthChanged += OnHealthChanged;
        }

        [Server]
        private void OnHealthChanged(int health)
        {
            if (health == 0)
            {
                Camera.main.transform.SetParent(playerManager.transform);
                _character.SetActive(false);
                _controllers.SetActive(false);
                StartCoroutine(Spawn());
            }
        }

        private IEnumerator Spawn()
        {
            yield return new WaitForSeconds(5);
            Camera.main.transform.SetParent(_character.transform);
            _character.transform.position = new Vector3(0, 0, 6.078432f);
            _character.SetActive(true);
            _controllers.SetActive(true);
            _stats.Health = 50;
            _stats.Mana = 50;
        }
    }
}