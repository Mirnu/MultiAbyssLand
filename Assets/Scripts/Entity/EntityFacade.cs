using Assets.Scripts.Entity.Pathfinding;
using Mirror;
using UnityEngine;

namespace Assets.Scripts.Entity
{
    public abstract class EntityFacade : NetworkBehaviour
    {
        [SerializeField] protected EntityStatsModel statsModel;
        [SerializeField] protected EntityMaxStatsModel maxStatsModel;
        [SerializeField] protected IPathfindingStrategy pathfindingStrategy;

        protected EntityStateMachine stateMachine;
        public GameObject CurrentTarget { get; protected set; }
    }
}