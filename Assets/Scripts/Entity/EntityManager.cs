using Assets.Scripts.ILifeCycle;
using Mirror;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Entity
{
    public class EntityManager : NetworkBehaviour
    {
        private Dictionary<Type, List<EntityComponent>> _componentsMap = new();

        public void AddComponent<T>(T component) where T : EntityComponent
        {
            if (_componentsMap.TryGetValue(typeof(T),
                out List<EntityComponent> components))
            {
                components.Add(component);
                return;
            }

            List<EntityComponent> list = new();
            list.Add(component);
            _componentsMap.Add(typeof(T), list);
        }

        private void Update()
        {
            if (isServer)
                ServerUpdate();
            else
                ClientUpdate();
        }

        private void ServerUpdate()
        {
            List<EntityComponent> serverTickables = GetComponentsByType<IServerTickable>();

            foreach (IServerTickable serverTickable in serverTickables)
            {
                serverTickable.ServerTick();
            }
        }

        private void ClientUpdate()
        {
            List<EntityComponent> clientTickables = GetComponentsByType<IClientTickable>();
 
            foreach (IClientTickable clientTickable in clientTickables)
            {
                clientTickable.ClientTick();
            }
        }

        private List<EntityComponent> GetComponentsByType<T>()
        {
            if (_componentsMap.TryGetValue(typeof(T), out List<EntityComponent> components))
            {
                return components;
            }
            else
            {
                return new();
            }
        }
    }
}