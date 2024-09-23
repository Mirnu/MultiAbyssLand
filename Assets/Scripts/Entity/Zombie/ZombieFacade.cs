using Assets.Scripts.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Assets.Scripts.Player;
using Assets.Scripts.Player.Components;

namespace Assets.Scripts.Entity.Zombie 
{

    public class ZombieFacade : EntityFacade
    {
        [SerializeField] protected new ZombieStateMachine stateMachine;

        private void OnServer()
        {
            FacadeLocator.Singleton.RegisterFacade(this);
            /*stateMachine.Initialize();*/
            Debug.Log(pathfindingStrategy);
        }

        private void Update()
        {
            /*stateMachine.Update();*/
        }

        public void TakeDamage(int damage)
        {
            statsModel.HP -= damage;
        }

        private void OnCollisionEnter(Collision other) {
            if (!isServer) return;
            if (other.gameObject.TryGetComponent(out PlayerMovement player))
            {
                CurrentTarget = player.gameObject;
                stateMachine.ChangeState(stateMachine.HitState);
            }
        }

        private void OnTriggerEnter(Collider other) {
            if (!isServer) return;
            if (other.gameObject.TryGetComponent(out PlayerMovement player))
            {
                Debug.Log("Get it!");
                CurrentTarget = player.gameObject;
                Debug.Log(stateMachine.AttackState);
                stateMachine.ChangeState(stateMachine.AttackState);
            }
        }

        private void OnTriggerExit(Collider other) {
            if (!isServer) return;
            if (other.gameObject.TryGetComponent(out PlayerMovement player))
            {
                CurrentTarget = null;
                stateMachine.ChangeState(stateMachine.SearchState);
            }
        }
    }
}


