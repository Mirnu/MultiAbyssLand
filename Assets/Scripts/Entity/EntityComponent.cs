using Assets.Scripts.ILifeCycle;
using Mirror;
using UnityEngine;

namespace Assets.Scripts.Entity
{
    public class EntityComponent : NetworkBehaviour, IServerInitializable, IClientTickable, IServerTickable,
        IClientInitializable
    {
        [SerializeField] protected EntityManager entityManager;

        private void Awake()
        {
            if (entityManager == null)
            {
                entityManager = GetComponentInParent<EntityManager>();
            }

            entityManager.RegisterComponent<IServerTickable>(this);
            entityManager.RegisterComponent<IClientTickable>(this);
            entityManager.RegisterComponent<IClientInitializable>(this);
            entityManager.RegisterComponent<IServerInitializable>(this);
        }

        private T FindComponentInParents<T>(GameObject obj) where T : Component
        {
            T component = obj.GetComponent<T>();
            if (component != null)
            {
                return component;
            }

            Transform parentTransform = obj.transform.parent;
            if (parentTransform != null)
            {
                return FindComponentInParents<T>(parentTransform.gameObject);
            }

            return null;
        }

        public virtual void ClientInitialize() { }
        public virtual void ClientTick() { }
        public virtual void ServerInitialize() { }
        public virtual void ServerTick() { }
    }
}