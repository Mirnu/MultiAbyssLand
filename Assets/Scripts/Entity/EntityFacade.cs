using Assets.Scripts.Entity.Pathfinding;
using Mirror;
using UnityEngine;

namespace Assets.Scripts.Entity
{
    public abstract class EntityFacade : NetworkBehaviour
    {
        public EntityStatsModel statsModel;
        [SerializeField] protected EntityMaxStatsModel maxStatsModel;
        [SerializeField] protected PathfindingStrategy pathfindingStrategy;
        [SerializeField] protected EntitySoundManager soundManager;
        [SerializeField] protected AudioSource audioSource;

        protected EntityStateMachine stateMachine;

        /*[SyncVar] private*/ public GameObject CurrentTarget;
        /*public GameObject CurrentTarget { get => _currentTarget; protected set { _currentTarget = value; } }*/

        public virtual void TakeDamage(int damage)
        {
            statsModel.HP -= damage;
            soundManager.PlayHitSound();
        }

        public virtual void Die(int hp)
        {
            if (hp > 0) return;
            statsModel.HpChanged -= Die;
            Destroy(gameObject);
        }

        public void Awake()
        {
            statsModel.HpChanged += Die;
        }   

        public void Update()
        {
            /*Debug.Log($"Entity hp: {statsModel.HP}");*/
        }
    }
}