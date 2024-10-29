using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Player.Components.Controllers
{
    public class InventoryController : PlayerComponent
    {
        [SerializeField] private GameObject _inventory;
        private InputAction _inventoryAction => playerManager.PlayerInput.Gameplay.Inventory;

        private void Awake()
        {
            _inventoryAction.Enable();
            _inventoryAction.performed += OnInventory;
        }

        private void OnInventory(InputAction.CallbackContext context)
        {
            _inventory.SetActive(!_inventory.activeSelf);
        }
    }
}