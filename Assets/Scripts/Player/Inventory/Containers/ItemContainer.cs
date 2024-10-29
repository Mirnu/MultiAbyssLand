using Assets.Scripts.Player.Inventory.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player.Inventory.Containers
{
    public class ItemContainer : MonoBehaviour, IEnumerable<ItemData>
    {
        [SerializeField] private ItemData[] _itemsDefault;
        private ItemData[] _items = new ItemData[64];
        private ItemEnumerator _enumerator;

        public event Action<int, ItemData> OnItemsChanged;

        public ItemData this[int index]
        {
            get => _items[index]; 
            set
            {
                _items[index] = value;
                OnItemsChanged?.Invoke(index, value);
            }
        }

        private void Awake()
        {
            if (_itemsDefault == null) return;
            for (int i = 0; i < _itemsDefault.Length; i++)
            {
                _items[i] = _itemsDefault[i];
            }
        }

        public ItemData GetItem(int id)
        {
            return Array.Find(_items, x => x.Id == id);
        }

        public IEnumerator<ItemData> GetEnumerator()
        {
            if (_enumerator == null)
            {
                _enumerator = new ItemEnumerator(_items);
            }
            return _enumerator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class ItemEnumerator : IEnumerator<ItemData>
        {
            private ItemData[] _items;
            private int _position = -1;

            public ItemEnumerator(ItemData[] items)
            {
                _items = items;
            }

            public ItemData Current => _items[_position];

            object IEnumerator.Current => Current;

            public void Dispose() { }

            public bool MoveNext()
            {
                return ++_position < _items.Length;
            }

            public void Reset()
            {
                _position = -1;
            }
        }
    }
}