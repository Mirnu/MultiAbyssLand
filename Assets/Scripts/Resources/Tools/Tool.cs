using Assets.Scripts.Resources.Data;
using Mirror;
using System;

namespace Assets.Scripts.Resources.Tools
{
    public class Tool : NetworkBehaviour
    {
        private Resource _resource;

        protected PlayerFacade playerFacade;

        public void Init(Resource resource, PlayerFacade facade)
        {
            _resource = resource;
            playerFacade = facade;
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
