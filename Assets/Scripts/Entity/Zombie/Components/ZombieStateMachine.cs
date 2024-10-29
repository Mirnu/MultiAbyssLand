using UnityEngine;

namespace Assets.Scripts.Entity.Zombie {

    public class ZombieStateMachine : EntityStateMachine
    {
        [SerializeField] public  ZombieAttackState AttackState;
        [SerializeField] public  ZombieSearchState SearchState;
        [SerializeField] public  ZombieHitState HitState;

        private EntityState _curState;
        private EntityState _prevState;

        public override void ServerInitialize()
        {
            Debug.Log("ZM sm  init");
            Init(SearchState);
        }

        private bool Init(EntityState state)
        {
            _curState = state;
            Debug.Log(_curState);
            return ChangeState(state);
        }

        public override bool ChangeState(EntityState newState)
        {
            if (_curState == newState) return false;
            if (!_curState.Exit()) return false;
            _prevState = _curState;
            _curState = newState;
            Debug.Log(_curState);
            _curState.Enter();
            return true;
        }
        
        public override void ServerTick()
        {
            _curState.Tick();
        }
    }
}
