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

        private void Awake()
        {
            tool = GetComponent<Tool>();
        }

        [Client]
        private void Start()
        {
            tool.Input.Gameplay.Activated.performed += (InputAction.CallbackContext context) => { Debug.Log("Activated"); };
        }

        [Client]
        private void OnDestroy()
        {
            tool.Input.Gameplay.Activated.performed -= OnActivated;
        }

        protected virtual void OnActivated(InputAction.CallbackContext context)
        {

        }
    }
}
