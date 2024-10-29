using Assets.Scripts.Player.Inventory.Controllers;
using Assets.Scripts.Resources.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.World.Containers
{
    public class ItemService : MonoBehaviour, IEnumerable<Resource>
    {
        [SerializeField] private Resource[] _items;
        private ItemServiceEnumerator _enumerator;

        public Resource this[int index]
        {
            get => _items[index];
        }

        public IEnumerator<Resource> GetEnumerator()
        {
            if (_enumerator == null)
            {
                _enumerator = new ItemServiceEnumerator(_items);
            }

            return _enumerator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class ItemServiceEnumerator : IEnumerator<Resource>
    {
        private Resource[] _items;
        private int _position = -1;

        public ItemServiceEnumerator(Resource[] items)
        {
            _items = items;
        }

        public Resource Current => _items[_position];

        object IEnumerator.Current => Current;

        public void Dispose() { }

        public bool MoveNext()
        {
            _position++;
            return _position < _items.Length;
        }

        public void Reset()
        {
            _position = -1;
        }
    }
}