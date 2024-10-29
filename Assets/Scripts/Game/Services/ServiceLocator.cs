using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game.Services
{
    public class ServiceLocator : MonoBehaviour
    {
        [SerializeField] private List<MonoBehaviour> _servicesBehaviours;

        private static ServiceLocator @this;
        public Dictionary<Type, object> Services = new Dictionary<Type, object>();

        private void Awake()
        {
            @this = this;
            DontDestroyOnLoad(this);

            foreach (var service in _servicesBehaviours)
            {
                Services[service.GetType()] = service;
            }
        }

        public static void RegisterService<T>(T service)
        {
            @this.Services[typeof(T)] = service;
        }

        public static T GetService<T>() 
        {
            return (T)@this.Services[typeof(T)];
        }
    }
}