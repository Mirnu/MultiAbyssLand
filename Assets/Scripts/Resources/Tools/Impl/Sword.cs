using Assets.Scripts.Game;
using Assets.Scripts.Player.Components.Controllers;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Resources.Tools.Impl
{
    public class Sword : ToolBehaviour
    {
        private PlayerFacade _playerFacade;

        private void OnEnable()
        {
            _playerFacade = FacadeLocator.Singleton.GetFacade<PlayerFacade>();
        }

        [Client]
        protected override void OnActivated(InputAction.CallbackContext context)
        {
            Debug.Log("Sword OnActivated");
            _playerFacade.PlayArmAnimation(ArmAction.Right);
        }
    }
}