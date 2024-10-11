using Assets.Scripts.Entity.Cow;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Entity.Zherdiay
{
    public class ZherdiaySearchState : EntityState
    {
        [SerializeField] private CowAnimator _animator;

        [SerializeField] private new ZherdiayStateMachine stateMachine;
        [SerializeField] private new ZherdiayFacade entityModel;
        public float cooldownTimeMax = 6f;
        public float cooldownTimeMin = 3f;
        private float _cooldownTime = 0f;
        private float _checkpointTime = 0f;
        private float _searchRadius = 5f;
        private bool _isSearch = false;
        private NavMeshAgent _agent;

        public override void Tick()
        {
            if (_agent.velocity.magnitude == 0.0f)
            {
                _isSearch = true;
                _animator.PlayIdle();
            } else _animator.PlayByDirection(_agent.velocity.normalized, true);
            if (!_isSearch) return;
            if ((Time.time - _checkpointTime) >= _cooldownTime)
            {        
                var point = (Vector2)entityModel.gameObject.transform.position + Random.insideUnitCircle * _searchRadius;
                Vector3 new_point = new Vector3(point.x, point.y, entityModel.gameObject.transform.position.z);
                pathfindingStrategy.MoveTo(new_point, entityModel.gameObject);
                _checkpointTime = Time.time;
                _cooldownTime = Random.RandomRange(cooldownTimeMin, cooldownTimeMax);
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
