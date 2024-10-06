using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Entity;
using Assets.Scripts.Entity.Pathfinding;
using Mirror;
using Assets.Scripts.Entity.Cow;
using UnityEngine.AI;

namespace Assets.Scripts.Entity.Zombie
{
    public class ZombieAttackState : EntityState
    {
        [SerializeField] private CowAnimator _animator;

        [SerializeField] private new ZombieStateMachine stateMachine;
        [SerializeField] private new ZombieFacade entityModel;
        [SerializeField] private NavMeshAgent _agent;

        public override void ServerTick()
        {
            _animator.PlayByDirection(_agent.velocity.normalized, true);
        }

        public override void Enter()
        {
            
        }

        public override bool Exit()
        {
            Debug.Log("Attack exit");
            pathfindingStrategy.MoveToPreviousPoint(entityModel.gameObject);
            return true;
        }

        public override void Tick()
        {
            Debug.Log("Attack tick");
            pathfindingStrategy.MoveTo(entityModel.CurrentTarget.transform, entityModel.gameObject);
        }
    }
}
