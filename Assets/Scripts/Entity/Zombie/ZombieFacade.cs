using Assets.Scripts.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Assets.Scripts.Player;
using Assets.Scripts.Player.Components;
using UnityEngine.AI;

namespace Assets.Scripts.Entity.Zombie 
{

    public class ZombieFacade : EntityFacade
    {
        [SerializeField] protected new ZombieStateMachine stateMachine;

        [Server]
        private void Start()
        {
            FacadeLocator.Singleton.RegisterFacade(this);
            /*stateMachine.Initialize();*/
            Debug.Log(pathfindingStrategy);
            var agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
            agent.updateUpAxis = false;
            agent.speed = statsModel.Speed;
            transform.rotation = new Quaternion();
        }

        private void Update()
        {
            /*stateMachine.Update();*/
        }

        public void TakeDamage(int damage)
        {
            statsModel.HP -= damage;
        }

        [Server]
        private void OnCollisionEnter(Collision other) {
            if (!isServer) return;
            if (other.gameObject.TryGetComponent(out PlayerMovement player))
            {
                CurrentTarget = player.gameObject;
                stateMachine.ChangeState(stateMachine.HitState);
            }
        }

        [Server]
        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.TryGetComponent(out PlayerMovement player))
            {
                Debug.Log("Get it!");
                CurrentTarget = other.gameObject;
                Debug.Log(other.gameObject);
                stateMachine.ChangeState(stateMachine.AttackState);
            }
        }

        [Server]
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


