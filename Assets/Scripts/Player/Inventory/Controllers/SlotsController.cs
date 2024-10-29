using Assets.Scripts.Game.Services;
using Assets.Scripts.Player.Inventory.Containers;
using Assets.Scripts.Player.Inventory.UI;
using Assets.Scripts.Resources.Data;
using Assets.Scripts.World.Containers;
using System;
using UnityEngine;

namespace Assets.Scripts.Player.Inventory.Controllers
{
    public class SlotsController : MonoBehaviour
    {
        /*

        Если отпустить предмет за границы рюкзака, то игрок его выбросит([ЛКМ] - всё количество, [ПКМ] - 1 штучку).
        Но его можно подобрать, подойдя к предмету;

        */
        
        [SerializeField] private SlotView[] _slotsInventory;
        [SerializeField] private AccessorySlot[] _slotsAccessory;
        [SerializeField] private ItemContainer _itemContainer;
        [SerializeField] private GameObject _inventoryView;

        private ItemService _itemService => ServiceLocator.GetService<ItemService>();

        private ItemData _currentItem = new();
        private SlotView _selectedView;

        private bool CurrentResourceIsEmpty => _currentItem.Count == 0;

        private int _index = 0;

        private void Awake()
        {
            foreach (var slot in _slotsInventory)
            {
                slot.Index = _index++;
                slot.OnRightClick += OnInventorySlotRightClick;
                slot.OnLeftClick += OnInventorySlotLeftClick;
                slot.OnHold += OnInventorySlotHold;
                slot.OnRelease += OnInventorySlotRelease;
            }

            foreach (var slot in _slotsAccessory)
            {
                slot.Index = _index;
                slot.View.Index = _index++;
                slot.View.OnRightClick += (int index) => OnAccessorySlotClick(index, slot.Type);
                slot.View.OnLeftClick += (int index) => OnAccessorySlotClick(index, slot.Type);
            }
        }

        private void UpdateSelectView(int index)
        {
            SlotView view = _slotsInventory[index] ?? _slotsAccessory[index];

            if (_selectedView != null)
            {
                _selectedView.Deselect();
            }

            _selectedView = view;
            _selectedView.Select();
        }

        private void OnInventorySlotRelease(int index)
        {
            // 21:32 блять пока я эту херню херачил я научился матом ругаться
            // 22:46 я хочу умереть
            
            ItemData item = _itemContainer[index];
            _currentItem.IsRealised = false;

            if (item != null)
            {
                item.IsRealised = !CurrentResourceIsEmpty;
            }
        }

        private void OnInventorySlotHold(int index)
        {
            if (!_inventoryView.activeSelf) return;
            UpdateSelectView(index);
            ItemData item = _itemContainer[index];
            if (!CurrentResourceIsEmpty && !_currentItem.IsRealised)
            {
                if (item != null && (item.Count == _itemService[item.Id].MaxItemsInStack ||
                    item.Id != _currentItem.Id)) return;
                int newCount = item == null ? 1 : item.Count + 1;
                _itemContainer[index] = GetNewData(count: newCount, isRealised: !CurrentResourceIsEmpty);
                _currentItem.Count--;
            }
            else if (item != null && (item.Id == _currentItem.Id || CurrentResourceIsEmpty))
            {
                Resource resource = _itemService[item.Id];
                _currentItem.IsRealised = true;

                if (item.IsRealised)
                {
                    if (!CurrentResourceIsEmpty && item.Count < resource.MaxItemsInStack &&
                        _currentItem.Count > 0)
                    {
                        _itemContainer[index] = GetNewData(count: item.Count + 1, isRealised: true);
                        _currentItem.Count--;
                    }
                }
                else if (_currentItem.Count < resource.MaxItemsInStack && item.Count > 0)
                {
                    _currentItem.Id = item.Id;
                    _currentItem.Count++;
                    _itemContainer[index] = --item.Count > 0 ? item : null;
                }
            }
        }

        private void OnAccessorySlotClick(int index, ArmorSlotType slotType)
        {
            ItemData item = _itemContainer[index];

            if (CurrentResourceIsEmpty && item != null)
            {
                _currentItem.Id = item.Id;
                _currentItem.Count = 1;
                _itemContainer[index] = null;
            }
            else if (!CurrentResourceIsEmpty)
            {
                Resource resource = _itemService[_currentItem.Id];
                ArmorResource armorResource = resource as ArmorResource;
                if (!armorResource || armorResource.SlotType != slotType) return;

                if (item == null)
                {
                    _itemContainer[index] = GetNewData();
                    _currentItem.Id = -1;
                    _currentItem.Count = 0;
                }
                else
                {
                    _itemContainer[index] = null;
                    _currentItem.Id = item.Id;
                    _currentItem.Count = 1;
                }
            }
        }

        private void OnInventorySlotRightClick(int index)
        {
            UpdateSelectView(index);
            if (!_inventoryView.activeSelf || _currentItem.IsRealised) return;
            ItemData item = _itemContainer[index];

            if (item != null && CurrentResourceIsEmpty)
            {
                _currentItem.Id = item.Id;
                _currentItem.Count++;
                _itemContainer[index] = --item.Count > 0 ? item : null;
            }
            else
            {
                if (item == null && !CurrentResourceIsEmpty)
                {
                    _itemContainer[index] = GetNewData(count: 1);
                    DecrementCurrentItem();
                }
                else if (item != null)
                {
                    Resource resource = _itemService[item.Id];

                    if (resource.MaxItemsInStack == item.Count || item.Id != _currentItem.Id)
                    {
                        SwapItems(index, item);
                    }
                    else if (item.Id == _currentItem.Id)
                    {
                        _itemContainer[index] = --item.Count > 0 ? item : null;
                        _currentItem.Count++;
                    }
                }
            }
        }

        private void DecrementCurrentItem()
        {
            if (--_currentItem.Count <= 0)
            {
                ResetCurrentItem();
            }
        }

        private void ResetCurrentItem()
        {
            _currentItem.Count = 0;
            _currentItem.Id = -1;
        }

        private void SwapItems(int index, ItemData item)
        {
            _itemContainer[index] = GetNewData();
            _currentItem.Id = item.Id;
            _currentItem.Count = item.Count;
        }

        private void OnInventorySlotLeftClick(int index)
        {
            UpdateSelectView(index);
            if (!_inventoryView.activeSelf) return;
            ItemData item = _itemContainer[index];

            if (item != null && CurrentResourceIsEmpty)
            {
                _currentItem.Id = item.Id;
                _currentItem.Count = item.Count;
                _itemContainer[index] = null;
            }
            else
            {
                if (item == null)
                {
                    _itemContainer[index] = GetNewData();
                    ResetCurrentItem();
                }
                else
                {
                    Resource resource = _itemService[item.Id];

                    if (resource.MaxItemsInStack == item.Count || item.Id != _currentItem.Id)
                    {
                        SwapItems(index, item);
                    }
                    else if (item.Id == _currentItem.Id)
                    {
                        int newCount = item.Count + _currentItem.Count;
                        _itemContainer[index] = GetNewData(item.Id, newCount);

                        if (newCount > resource.MaxItemsInStack)
                        {
                            _currentItem.Count = newCount - resource.MaxItemsInStack;
                        }
                        else
                        {
                            ResetCurrentItem();
                        }             
                    }
                }
            }
        }

        private ItemData GetNewData(int id = -1, int count = -1, bool isRealised = false)
        {
            ItemData data = new ItemData();
            data.Id = id == -1 ? _currentItem.Id : id;
            data.Count = count == -1 ? _currentItem.Count : count;
            data.IsRealised = isRealised;

            return data.Count == 0 ? null : data;
        }
    }

    [Serializable]
    public class ItemData
    {
        [SerializeField] private int _id = -1;
        [SerializeField] private int _count = 0;

        public bool IsRealised = false;

        private ItemService _service => ServiceLocator.GetService<ItemService>();

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        public int Count
        {
            get => _count;
            set
            {
                if (value <= 0 || Id < 0)
                {
                    _count = 0;
                    return;
                }

                Resource resource = _service[Id];
                _count = Math.Clamp(value, 1, resource.MaxItemsInStack);
            }
        }
    }

    [Serializable]
    public class AccessorySlot
    {
        public int Index { get; set; }
        public SlotView View;
        public ArmorSlotType Type;

        public static implicit operator SlotView(AccessorySlot slot) => slot.View;
    }
}