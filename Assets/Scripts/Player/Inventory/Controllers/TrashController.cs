using Assets.Scripts.Player.Inventory.Containers;
using Assets.Scripts.Player.Inventory.UI;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Player.Inventory.Controllers
{
    public class TrashController : MonoBehaviour
    {
        [SerializeField] private SlotView _trashSlot;
        [SerializeField] private ItemContainer _itemContainer;
        [SerializeField] private Button _trashButton;
        private int _index => _trashSlot.Index;

        private void Awake()
        {
            _trashButton.onClick.AddListener(OnTrashButtonClick);
        }

        private void OnTrashButtonClick()
        {
            _itemContainer[_index] = null;
        }
    }
}