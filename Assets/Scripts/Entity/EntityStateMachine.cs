using UnityEngine;
using Assets.Scripts.Entity.Pathfinding;

namespace Assets.Scripts.Entity
{
    public abstract class EntityStateMachine: EntityComponent
    {
        [SerializeField] protected EntityStatsModel _Stats;
        [SerializeField] protected EntityFacade _EntityModel;
        [SerializeField] protected PathfindingStrategy _PathfindingStrategy;

/*        public abstract void Initialize();*/
        public abstract bool ChangeState(EntityState new_state);
    }
}
