using Assets.Scripts.Resources.Data;
using Mirror;
using System;
using UnityEngine;

namespace Assets.Scripts.Resources.Tools
{
    public class Tool : NetworkBehaviour
    {
        private Resource _resource;
        private ToolInput _input;

        public ToolInput Input => _input;
        public Action OnUse;

        [Client]
        private void Awake()
        {
            _input = new ToolInput();
            _input.Enable();
        }

        [Server]
        public void Init(Resource resource)
        {
            _resource = resource;
        }

        public bool TryGetResource<T>(out T resource) where T : Resource
        {
            if (_resource is T)
            {
                resource = (T)_resource;
                return true;
            }

            resource = default;
            return false;
        }

        public T GetResource<T>() where T : Resource
        {
            if (_resource is T)
                return (T)_resource;

            throw new Exception("Resource not found");
        }
    }
}
