using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Entity;
using Assets.Scripts.Entity.Pathfinding;
using Mirror;
using Assets.Scripts.Entity.Cow;
using UnityEngine.AI;

namespace Assets.Scripts.Entity.Fox
{
    public class FoxAttackState : EntityState
    {
        [SerializeField] private CowAnimator _animator;

        [SerializeField] private new FoxStateMachine stateMachine;
        [SerializeField] private new FoxFacade entityModel;
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
            
        }

        public override bool Exit()
        {
            pathfindingStrategy.MoveToPreviousPoint(entityModel.gameObject);
            return true;
        }

        public override void Tick()
        {
            pathfindingStrategy.MoveTo(entityModel.CurrentTarget.transform, entityModel.gameObject);
        }
    }
}
