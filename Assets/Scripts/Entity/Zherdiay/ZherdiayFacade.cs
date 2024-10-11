using Assets.Scripts.Game;
using UnityEngine;
using Mirror;
using UnityEngine.AI;

namespace Assets.Scripts.Entity.Zherdiay
{

    public class ZherdiayFacade : EntityFacade
    {
        [SerializeField] protected new ZherdiayStateMachine stateMachine;

        [Server]
        private void Start()
        {
            FacadeLocator.Singleton.RegisterFacade(this);
            Debug.Log(pathfindingStrategy);
            var agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
            agent.updateUpAxis = false;
            agent.speed = statsModel.Speed;
            transform.rotation = new Quaternion();
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            Collider[] colliders = Physics.OverlapSphere(transform.position, 10f);
            foreach (Collider coll in colliders)
            {
                var player = coll.gameObject.GetComponentInParent<PlayerFacade>();
                if (player)
                {
                    CurrentTarget = player.Character;
                    stateMachine.ChangeState(stateMachine.AttackState);
                    break;
                }
            }
        }

        private float attackTime = 0f;
        public float AttackCooldown = 1;
        [Server]
        private void OnCollisionStay(Collision other)
        {
            Debug.Log("Attacked");
            var player = other.gameObject.GetComponentInParent<PlayerFacade>();
            if (player)
            {
                if (Time.time - attackTime > AttackCooldown)
                {              
                    player.TakeDamage(statsModel.Damage);
                    attackTime = Time.time;
                } 
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


