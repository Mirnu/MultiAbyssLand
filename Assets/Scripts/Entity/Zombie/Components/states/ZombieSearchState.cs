using Assets.Scripts.Entity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditorInternal;
using UnityEngine;
using Assets.Scripts.Entity.Pathfinding;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine.AI;

namespace Assets.Scripts.Entity.Zombie
{
    public class ZombieSearchState : EntityState
    {
        [SerializeField] private new ZombieStateMachine stateMachine;
        [SerializeField] private new ZombieFacade entityModel;
        private float _cooldownTime = 5f;
        private float _checkpointTime= 0f;
        private float _searchRadius = 5f;
        private bool _isSearch = false;
        private NavMeshAgent _agent;

        public override void Tick()
        {
            if (_agent.velocity.magnitude == 0.0f) _isSearch = true;
            if (!_isSearch) return;
            if ((Time.time - _checkpointTime) >= _cooldownTime)
            {
                var point = (Vector2)entityModel.gameObject.transform.position + Random.insideUnitCircle * _searchRadius;
                Vector3 new_point = new Vector3(point.x, point.y, entityModel.gameObject.transform.position.z);
                pathfindingStrategy.MoveTo(new_point, entityModel.gameObject);
                _checkpointTime = Time.time;
                _cooldownTime = Random.RandomRange(3f, 6f);
            }
        }
        public override bool Exit()
        {
            _isSearch = false;
            return true;
        }

        public override void Enter() {         
            pathfindingStrategy.MoveToPreviousPoint(entityModel.gameObject);
        }

        private void Awake()
        {
            _agent = entityModel.gameObject.GetComponentInParent<NavMeshAgent>();
        }
    }
}
