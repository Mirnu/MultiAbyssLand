using Assets.Scripts.Player.Data;
using Mirror;
using UnityEngine;

namespace Assets.Scripts.Player.Components.Handlers
{
    public class PlayerMoveHandler : PlayerComponent
    {
        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private PlayerStats _statsModel;

        private float _timeWalk = 0;

        [Server]
        private void OnDestroy()
        {
            _movement.Moved -= OnMoved;
        }

        public override void ServerInitialize()
        {
            _movement.Moved += OnMoved;
        }

        [Server]
        private void OnMoved()
        {
            _timeWalk += Time.deltaTime;
            if (_timeWalk > 5)
            {
                _statsModel.Food -= 1;
                _timeWalk = 0;
            }
        }
    }
}