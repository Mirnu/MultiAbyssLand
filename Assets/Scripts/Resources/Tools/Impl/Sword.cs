using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Resources.Tools.Impl
{
    public class Sword : ToolBehaviour
    {
        [Client]
        protected override void OnActivated(InputAction.CallbackContext context)
        {
            Debug.Log("Sword activated");
        }
    }
}