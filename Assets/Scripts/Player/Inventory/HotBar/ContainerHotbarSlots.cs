using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Inventory.View;
using Assets.Scripts.Player.Components;
using Assets.Scripts.Player.Inventory.View;
using Assets.Scripts.Resources.Data;
using Assets.Scripts.Resources.Tools;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Player.Inventory.Hotbar
{
    public class ContainerHotbarSlots : NetworkBehaviour, IDisposable
    {
        [SerializeField] private List<HotbarSlotView> _slots = new List<HotbarSlotView>();
        [SerializeField] private GameObject _inventory;
        public PlayerInput _input;
        [SerializeField] private Resource mock;
        [SerializeField] private Resource mock1;
        [SerializeField] private ToolContainer _hand;

        public override void OnStartClient()
        {
            //
            _slots[0].TrySet(mock);
            _slots[0].GetComponent<SelectableSlotView>().TrySet(mock);
            _slots[0].GetComponent<SelectableSlotView>().SetCount(52);
            _slots[1].TrySet(mock1);
            _slots[1].GetComponent<SelectableSlotView>().TrySet(mock1);
            //
          
            _input = new PlayerInput();
            _input.Enable();
            _slots.ForEach(x => x.GetComponent<SelectableSlotView>().enabled = false);
            _input.Gameplay.Hotbar.performed += HotbarChangeState;
            _input.Gameplay.Inventory.performed += HotbarChangeSelectability;
            _input.Gameplay.Inventory.performed += OnInventoryChangedState;
        }

        private void OnInventoryChangedState(InputAction.CallbackContext context)
        {
            Debug.Log("is changed state");
            _inventory.SetActive(!_inventory.activeSelf);
        }

        public void Dispose()
        {
            _input.Gameplay.Hotbar.performed -= HotbarChangeState;
            _input.Gameplay.Inventory.performed -= HotbarChangeSelectability;
            _input.Gameplay.Inventory.performed -= OnInventoryChangedState;
        }

        private void HotbarChangeSelectability(InputAction.CallbackContext context) {
            if(_slots.Any(x => x.IsSelected)) { _slots.Find(x => x.IsSelected).Deselect(); }
            _slots.ForEach(x => x.GetComponent<SelectableSlotView>().enabled = !x.GetComponent<SelectableSlotView>().enabled);
            _slots.ForEach(x => x.GetComponent<HotbarSlotView>().enabled = !x.GetComponent<HotbarSlotView>().enabled);
        }

        private void HotbarChangeState(InputAction.CallbackContext context) {
            Debug.Log("PP: " + context.ReadValue<float>());
            var index = context.ReadValue<float>();
            if(index < _slots.Count) {
                if(_slots.Any(x => x.IsSelected)) { _slots.Find(x => x.IsSelected).Deselect(); }
                var slot = _slots[(int)index];
                slot.Select();
                if(slot.TryGet(out Resource res)) { _hand.Equip(res); }
                else { _hand.EmptyHand(); }
            }
        }
    }
}