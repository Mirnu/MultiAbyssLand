using Assets.Scripts.Entity;
using Assets.Scripts.Entity.Pathfinding;
using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

namespace Assets.Scripts.Entity.Bear
{
    public class BearHitState : EntityState
    {
        [SerializeField] private new BearStateMachine stateMachine;
        [SerializeField] private new BearFacade entityModel;

        public override void Enter()
        {
            PlayerFacade player = entityModel.CurrentTarget.GetComponentInParent<PlayerFacade>();
            if (player)
            {
                Debug.Log($"{player} was attacked");
            }
            /*entityModel.CurrentTarget = player.gameObject;*/
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
