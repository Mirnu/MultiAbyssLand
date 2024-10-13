using Assets.Scripts.Player.Inventory.Hotbar;
using Assets.Scripts.Resources.Data;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Resources.Tools.Impl
{
    public class FoodTool : ToolBehaviour
    {
        private PlayerFacade _facade => PlayerFacade.Singleton;
        private ContainerHotbarSlots _hotbarSlots => _facade.hotbar;

        protected override void OnActivated(InputAction.CallbackContext context)
        {
            _facade.Stats.Food += tool.GetResource<FoodResource>().Satiety;
            _hotbarSlots.DeleteFromSlot();
        }
    }
}