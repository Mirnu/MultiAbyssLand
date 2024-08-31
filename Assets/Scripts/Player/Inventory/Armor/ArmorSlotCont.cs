using System;
using Assets.Scripts.Inventory.View;
using Assets.Scripts.Resources.Armors;
using Assets.Scripts.Resources.Data;
using Mirror;
using UnityEngine;

namespace Assets.Scripts.Inventory.Armor {
    [Serializable]
    public class ArmorSlotCont {
        public GameObject Sprite;
        public ArmorSlotView Slot;
        public ArmorSlotView Cosmetic;

        private ArmorResource _current;
        private ArmorMB _currentMb;

        public ArmorResource CurrentResource => _current;

        [ServerCallback]
        public void Equip(ArmorResource armor) {
            _current = armor;
            if (_currentMb != null) 
                UnityEngine.Object.Destroy(_currentMb.gameObject);
            
            _currentMb = createArmor(_current);
        }

        [ServerCallback]
        private ArmorMB createArmor(ArmorResource resource)
        {
            if(resource == null) { return null; }
            ArmorMB armor = UnityEngine.Object.Instantiate(resource.EquippedSprite, Sprite.transform.position, Quaternion.identity);
            armor.Init(CurrentResource);
            NetworkServer.Spawn(armor.gameObject);
            return armor;
        }
    }
}