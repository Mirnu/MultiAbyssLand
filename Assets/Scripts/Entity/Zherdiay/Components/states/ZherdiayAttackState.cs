using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Entity;
using Assets.Scripts.Entity.Pathfinding;
using Mirror;
using Assets.Scripts.Entity.Cow;
using UnityEngine.AI;

namespace Assets.Scripts.Entity.Zherdiay
{
    public class ZherdiayAttackState : EntityState
    {
        [SerializeField] private CowAnimator _animator;

        [SerializeField] private new ZherdiayStateMachine stateMachine;
        [SerializeField] private new ZherdiayFacade entityModel;
        [SerializeField] private NavMeshAgent _agent;

        public override void ServerTick()
        {
            if (_agent.velocity.magnitude == 0.0f)
            {
                _animator.PlayIdle();
            } else _animator.PlayByDirection(_agent.velocity.normalized, true);
        }

        public override void Enter()
        {
            entityStats.Speed *= 3;
        }

        public override bool Exit()
        {
            entityStats.Speed /= 3;
            pathfindingStrategy.MoveToPreviousPoint(entityModel.gameObject);
            return true;
        }

        public override void Tick()
        {
            pathfindingStrategy.MoveTo(entityModel.CurrentTarget.transform, entityModel.gameObject);
        }
    }
}
