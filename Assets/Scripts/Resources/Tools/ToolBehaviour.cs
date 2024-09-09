using Assets.Scripts.Game;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

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
        }

        [Client]
        private void Start()
        {
            tool.Input.Gameplay.Activated.performed += OnActivated;
        }

        [Client]
        private void OnDestroy()
        {
            tool.Input.Gameplay.Activated.performed -= OnActivated;
        }

        [Client]
        protected virtual void OnActivated(InputAction.CallbackContext context) { }

        [Client]
        protected virtual void OnHold() { }
    }
}
