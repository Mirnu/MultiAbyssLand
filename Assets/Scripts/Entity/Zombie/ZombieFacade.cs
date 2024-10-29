using Assets.Scripts.Game;
using UnityEngine;
using Mirror;
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


        private float attackTime = 0f;
        public float AttackCooldown = 1;
        [Server]
        private void OnCollisionStay(Collision other)
        {
            var player = other.gameObject.GetComponentInParent<PlayerFacade>();
            if (player)
            {
                
                if (Time.time - attackTime > AttackCooldown)
                {
                    CurrentTarget = other.gameObject;
                    player.TakeDamage(statsModel.Damage);
                    attackTime = Time.time;
                } 
            }
        }

        [Server]
        private void OnTriggerEnter(Collider other)
        {
            var player = other.gameObject.GetComponentInParent<PlayerFacade>();
            if (player)
            {
                Debug.Log("Get it!");
                CurrentTarget = other.gameObject;
                Debug.Log(other.gameObject);
                stateMachine.ChangeState(stateMachine.AttackState);
            }
        }

        [Server]
        private void OnTriggerExit(Collider other)
        {
            var player = other.gameObject.GetComponentInParent<PlayerFacade>();
            if (player)
            {
                CurrentTarget = null;
                stateMachine.ChangeState(stateMachine.SearchState);
            }
        }
    }
}


