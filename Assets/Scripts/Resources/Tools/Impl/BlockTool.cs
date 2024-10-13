using Assets.Scripts.Entity;
using Assets.Scripts.Misc.Constants;
using Assets.Scripts.Resources.Data;
using Assets.Scripts.World.Blocks;
using Assets.Scripts.World.Managers;
using Mirror;
using System.Drawing;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Resources.Tools.Impl
{
    public class BlockTool : ToolBehaviour
    {
        [SerializeField] private Block inWorld;

        [Client]
        protected override void OnActivated(InputAction.CallbackContext context)
        {
            if(PlayerFacade.Singleton.hotbar.isInvOpen) { return; }
            Vector3 r = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            FirstTypeManager.Singleton.PutBlock(new Vector2(Mathf.Round(r.x), Mathf.Round(r.y)), inWorld);
            PlayerFacade.Singleton.hotbar.DeleteFromSlot();
        }

    }
}