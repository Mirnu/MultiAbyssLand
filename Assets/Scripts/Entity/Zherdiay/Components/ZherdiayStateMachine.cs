using Assets.Scripts.Entity;
using Assets.Scripts.Entity.Pathfinding;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using Mirror;


namespace Assets.Scripts.Entity.Zherdiay
{

    public class ZherdiayStateMachine : EntityStateMachine
    {
        [SerializeField] public ZherdiayAttackState AttackState;
        [SerializeField] public ZherdiaySearchState SearchState;
        [SerializeField] public ZherdiayHitState HitState;

        private EntityState _curState;
        private EntityState _prevState;

        public override void ServerInitialize()
        {
            Debug.Log("Fox sm  init");
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
