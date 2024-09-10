using Assets.Scripts.Entity;
using Assets.Scripts.Entity.Pathfinding;
using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

namespace Assets.Scripts.Entity.Zombie
{
    public class ZombieHitState : EntityState
    {
        [SerializeField] private new ZombieStateMachine stateMachine;
        [SerializeField] private new ZombieFacade entityModel;

        public override void Enter()
        {
            if (entityModel.CurrentTarget.TryGetComponent<PlayerFacade>(out PlayerFacade target))
            {
                Debug.Log($"{target} was attacked");
            }
            stateMachine.ChangeState(stateMachine.AttackState);
        }

        public override bool Exit()
        {
            return true;
        }

        public override void Tick()
        {
        }
    }
}
