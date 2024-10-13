using Assets.Scripts.Game;
using UnityEngine;
using Mirror;
using UnityEngine.AI;
using Org.BouncyCastle.Asn1;
using System.Collections.Generic;

namespace Assets.Scripts.Entity.Cow
{

    public class CowFacade : EntityFacade
    {
        [SerializeField] protected new CowStateMachine stateMachine;

        private List<PlayerFacade> players;

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

        [Server]
        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            stateMachine.ChangeState(stateMachine.PanicState);
        }

        private void Update()
        {
            /*stateMachine.Update();*/
        }
    }
}


