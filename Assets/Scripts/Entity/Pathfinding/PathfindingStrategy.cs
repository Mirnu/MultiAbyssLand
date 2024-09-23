using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Assets.Scripts.Entity.Pathfinding
{
    [System.Serializable]
    public class PathfindingStrategy: NetworkBehaviour
    {
        public virtual void MoveTo(Transform target, GameObject self) {
            throw new NotImplementedException();
        }
        public virtual void MoveToPreviousPoint(GameObject self) {
            throw new NotImplementedException();
        }
    }
}
