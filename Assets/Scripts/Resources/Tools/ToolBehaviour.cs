using Assets.Scripts.Entity;
using Assets.Scripts.Game;
using Assets.Scripts.World.Managers;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Resources.Tools
{
    [RequireComponent(typeof(Tool))]
    public class ToolBehaviour : NetworkBehaviour
    {
        protected Tool tool;

        protected T GetFacade<T>() where T : EntityFacade
        {
            return FacadeLocator.Singleton.GetFacade<T>();
        }

        [Client]
        private void Update()
        {
            if (tool.Input.Gameplay.Activated.IsPressed())
            {
                OnHold();
            }
        }

        private void Awake()
        {
            tool = GetComponent<Tool>();

            tool.Input.Gameplay.Activated.performed += OnActivated;
            tool.OnUse += OnUse;
        }

        [Client]
        private void OnDestroy()
        {
            tool.Input.Gameplay.Activated.performed -= OnActivated;
            tool.OnUse -= OnUse;
        }

        [Client]
        protected virtual void OnActivated(InputAction.CallbackContext context) { }

        [Client]
        protected virtual void OnHold() { }

        [Client]
        protected virtual void OnUse() { }
    }
}
