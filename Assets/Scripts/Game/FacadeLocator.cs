using Assets.Scripts.Entity;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Game
{
    public class FacadeLocator
    {
        private static FacadeLocator _singleton;
        public static FacadeLocator Singleton
        {
            get
            {
                if (_singleton == null)
                {
                    _singleton = new FacadeLocator();
                }
                return _singleton;
            }
        }

        private Dictionary<Type, object> _facades = new();

        public void RegisterFacade<T>(T facade) where T : EntityFacade
        {
            _facades[typeof(T)] = facade;
        }

        public T GetFacade<T>() where T : EntityFacade
        {
            return (T)_facades[typeof(T)] ?? default;
        }
    }
}
