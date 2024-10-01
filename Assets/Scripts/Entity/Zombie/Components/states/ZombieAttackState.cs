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
            Debug.Log("Attack exit");
            return true;
        }

        public override void Tick()
        {
            Debug.Log("Attack tick");
            pathfindingStrategy.MoveTo(entityModel.CurrentTarget.transform, entityModel.gameObject);
        }
    }
}
