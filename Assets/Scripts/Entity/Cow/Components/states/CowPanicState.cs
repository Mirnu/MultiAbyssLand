using Assets.Scripts.Entity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditorInternal;
using UnityEngine;
using Assets.Scripts.Entity.Pathfinding;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine.AI;

namespace Assets.Scripts.Entity.Cow
{
    public class CowPanicState : EntityState
    {
        [SerializeField] private new CowStateMachine stateMachine;
        [SerializeField] private new CowFacade entityModel;
        [SerializeField] private float _cooldownTimeMax = 6f;
        [SerializeField] private float _cooldownTimeMin = 3f;
        [SerializeField] private float panicDuration = 5f;
        private float panicDurationTime = 0f;
        private float _cooldownTime = 0f;
        private float _checkpointTime= 0f;
        private float _searchRadius = 5f;
        private bool _isSearch = false;
        private NavMeshAgent _agent;

        public override void Tick()
        {
            if (Time.time - panicDurationTime >= panicDuration) stateMachine.ChangeState(stateMachine.SearchState);
            if (_agent.velocity.magnitude == 0.0f) _isSearch = true;
            if (!_isSearch) return;
            if ((Time.time - _checkpointTime) >= _cooldownTime)
            {
                var point = (Vector2)entityModel.gameObject.transform.position + Random.insideUnitCircle * _searchRadius;
                Vector3 new_point = new Vector3(point.x, point.y, entityModel.gameObject.transform.position.z);
                pathfindingStrategy.MoveTo(new_point, entityModel.gameObject);
                _checkpointTime = Time.time;
                _cooldownTime = Random.RandomRange(_cooldownTimeMin, _cooldownTimeMax);
            }
        }

        public override bool Exit()
        {
            entityModel.statsModel.Speed = entityModel.statsModel.Speed / (1f + 2f);
            _isSearch = false;
            return true;
        }

        public override void Enter() {
            panicDurationTime = Time.time;
            entityModel.statsModel.Speed = entityModel.statsModel.Speed * (1f + 2f);
            pathfindingStrategy.MoveToPreviousPoint(entityModel.gameObject);
        }

        private void Awake()
        {
            _agent = entityModel.gameObject.GetComponentInParent<NavMeshAgent>();
        }
    }
}
