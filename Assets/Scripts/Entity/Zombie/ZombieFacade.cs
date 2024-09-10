using Assets.Scripts.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Entity.Zombie 
{

    public class ZombieFacade : EntityFacade
    {
        [SerializeField] protected new ZombieStateMachine stateMachine;

        private void Start()
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

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out PlayerFacade player))
            {
                CurrentTarget = player.gameObject;
                stateMachine.ChangeState(stateMachine.HitState);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out PlayerFacade player))
            {
                CurrentTarget = player.gameObject;
                stateMachine.ChangeState(stateMachine.AttackState);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent(out PlayerFacade player))
            {
                CurrentTarget = null;
                stateMachine.ChangeState(stateMachine.SearchState);
            }
        }
    }
}


