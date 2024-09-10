using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Entity;
using Assets.Scripts.Entity.Pathfinding;
using Mirror;

namespace Assets.Scripts.Entity.Zombie
{
    public class ZombieAttackState : EntityState
    {
        [SerializeField] private new ZombieStateMachine stateMachine;
        [SerializeField] private new ZombieFacade entityModel;

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
            Debug.Log(entityModel.CurrentTarget.transform);
            pathfindingStrategy.MoveTo(entityModel.CurrentTarget.transform, entityModel.gameObject);
        }
    }
}
