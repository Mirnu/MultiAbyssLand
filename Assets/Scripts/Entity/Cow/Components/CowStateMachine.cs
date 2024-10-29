using UnityEngine;


namespace Assets.Scripts.Entity.Cow {

    public class CowStateMachine : EntityStateMachine
    {
        [SerializeField] public CowSearchState SearchState;
        [SerializeField] public CowPanicState PanicState;

        private EntityState _curState;
        private EntityState _prevState;

        public override void ServerInitialize()
        {
            Init(SearchState);
        }

        private bool Init(EntityState state)
        {
            _curState = state;
            return ChangeState(state);
        }

        public override bool ChangeState(EntityState newState)
        {
            if (_curState == newState) return false;
            if (!_curState.Exit()) return false;
            _prevState = _curState;
            _curState = newState;
            _curState.Enter();
            return true;
        }
        
        public override void ServerTick()
        {
            _curState.Tick();
        }
    }
}
