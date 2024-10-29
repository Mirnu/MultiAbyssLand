using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player.Inventory.Containers
{
    public class AccessoryContainer : MonoBehaviour, IEnumerable<int>
    {
        [SerializeField] private int[] _accessory = new int[4];
        private AccessoryEnumerator _enumerator;

        public int this[int index]
        {
            get => _accessory[index];
            set => _accessory[index] = value;
        }

        public IEnumerator<int> GetEnumerator()
        {
            if (_enumerator == null)
            {
                _enumerator = new AccessoryEnumerator(_accessory);
            }
            return _enumerator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class AccessoryEnumerator : IEnumerator<int>
        {
            private int[] _accessory;
            private int _position = -1;

            public AccessoryEnumerator(int[] accessory)
            {
                _accessory = accessory;
            }

            public int Current => _accessory[_position];

            object IEnumerator.Current => Current;

            public void Dispose() { }

            public bool MoveNext()
            {
                _position++;
                return _position < _accessory.Length;
            }

            public void Reset()
            {
                _position = -1;
            }
        }
    }
}