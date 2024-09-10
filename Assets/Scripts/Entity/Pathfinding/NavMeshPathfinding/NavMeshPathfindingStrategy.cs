using Assets.Scripts.Entity.Pathfinding;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Entity.Pathfinding
{
    public class NavMeshPathfindingStrategy : IPathfindingStrategy
    {
        private Vector3 _previousPoint { get; set; }

        public void MoveTo(Transform target, GameObject self)
        {
            if (!self.TryGetComponent<NavMeshAgent>(out NavMeshAgent agent))
            {
                Debug.LogError("NavMesh agent was missing in NavMeshPathfindingStrategy.MoveTo method");
                return;
            }
            _previousPoint = target.transform.position; 
            agent.SetDestination(new Vector3(target.transform.position.x, target.transform.position.y));
        }

        public void MoveToPreviousPoint(GameObject self)
        {
            if (!self.TryGetComponent<NavMeshAgent>(out NavMeshAgent agent))
            {
                Debug.LogError("NavMesh agent was missing in NavMeshPathfindingStrategy.MoveToPreviousPoint method");
                return;
            }
            agent.SetDestination(new Vector3(_previousPoint.x, _previousPoint.y));
        }
    }
}
