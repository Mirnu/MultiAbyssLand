using Assets.Scripts.Misc.CD;
using Assets.Scripts.Player.Data;
using Mirror;
using System;
using UnityEngine;

namespace Assets.Scripts.Player.Components.Handlers
{
    public class PlayerMoveHandler : PlayerComponent
    {
        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private PlayerStats _statsModel;

        private float _timeWalk = 0;

        private Action Starve;

        [Server]
        private void OnDestroy()
        {
            _movement.Moved -= Starve;
        }

        public override void ServerInitialize()
        {
            Starve = CDUtils.CycleAccumulatingWait(5, () => _statsModel.Food -= 1);
            _movement.Moved += Starve;
        }
    }
}