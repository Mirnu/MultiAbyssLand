using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Entity.Pathfinding;

namespace Assets.Scripts.Entity
{
    public abstract class EntityState: EntityComponent
    {
        protected EntityStateMachine stateMachine;
        protected EntityFacade entityModel;
        [SerializeField] protected EntityStatsModel entityStats;
        [SerializeField] protected IPathfindingStrategy pathfindingStrategy;

        public abstract void Enter();

        public abstract void Tick();

        public abstract bool Exit();
    }
}
