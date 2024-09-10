using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Entity.Pathfinding
{
    public interface IPathfindingStrategy
    {
        public void MoveTo(Transform target, GameObject self);
        public void MoveToPreviousPoint(GameObject self);
    }
}
