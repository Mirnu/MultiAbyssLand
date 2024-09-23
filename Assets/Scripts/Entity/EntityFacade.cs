using Assets.Scripts.Entity.Pathfinding;
using Mirror;
using UnityEngine;

namespace Assets.Scripts.Entity
{
    public abstract class EntityFacade : NetworkBehaviour
    {
        [SerializeField] protected EntityStatsModel statsModel;
        [SerializeField] protected EntityMaxStatsModel maxStatsModel;
        [SerializeField] protected PathfindingStrategy pathfindingStrategy;

        protected EntityStateMachine stateMachine;

        [SyncVar] private GameObject _currentTarget;
        public GameObject CurrentTarget { get => _currentTarget; protected set { _currentTarget = value; } }
    }
}