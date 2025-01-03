﻿using Assets.Scripts.ILifeCycle;
using Mirror;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Entity
{
    public class EntityManager : NetworkBehaviour
    {
        private Dictionary<Type, List<EntityComponent>> _componentsMap = new();

        public void RegisterComponent<T>(EntityComponent component) 
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

        private void Start()
        {
            if (isServer)
                ServerStart();

            if (isLocalPlayer)
                ClientStart();
        }

        private void ServerStart()
        {
            List<EntityComponent> serverInitializables = GetComponentsByType<IServerInitializable>();

            foreach (IServerInitializable serverInitializable in serverInitializables)
            {
                serverInitializable.ServerInitialize();
            }
        }

        private void ClientStart()
        {
            List<EntityComponent> clientInitializables = GetComponentsByType<IClientInitializable>();

            foreach (IClientInitializable clientInitializable in clientInitializables)
            {
                clientInitializable.ClientInitialize();
            }
        }

        private void Update()
        {
            if (isServer)
                ServerUpdate();

            if (isLocalPlayer)
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