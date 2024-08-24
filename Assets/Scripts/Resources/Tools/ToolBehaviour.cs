using Assets.Scripts.Game;
using Mirror;
using UnityEngine;

namespace Assets.Scripts.Resources.Tools
{
    [RequireComponent(typeof(Tool))]
    public class ToolBehaviour : NetworkBehaviour
    {
        protected Tool tool;

        protected T GetFacade<T>()
        {
            return FacadeLocator.Singleton.GetFacade<T>();
        }

        private void Awake()
        {
            tool = GetComponent<Tool>();
        }
    }
}
