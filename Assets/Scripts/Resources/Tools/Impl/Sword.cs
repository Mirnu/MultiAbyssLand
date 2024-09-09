using Assets.Scripts.Game;
using Assets.Scripts.Misc;
using Assets.Scripts.Misc.CD;
using Assets.Scripts.Player.Components.Controllers;
using Mirror;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Resources.Tools.Impl
{
    public class Sword : ToolBehaviour
    {
        private PlayerFacade _playerFacade;
        private Action attack;

        private void OnEnable()
        {
            attack = CDUtils.CycleWait(1, Attack);
            _playerFacade = FacadeLocator.Singleton.GetFacade<PlayerFacade>();
        }

        [Client]
        protected override void OnActivated(InputAction.CallbackContext context)
        {
            
        }

        [Client]
        protected override void OnHold()
        {
            Debug.Log(1);
            attack();
        }


        private void Attack()
        {
            Debug.Log("Attack");
            int interval = AngleUtils.GetInterval();
            _playerFacade.ArmAnimator.Play(interval + 20, 10);
        }
    }
}