using System;
using Assets.Scripts.Inventory.View;
using UnityEngine;

namespace Assets.Scripts.Inventory.Armor {
    [Serializable]
    public class AccessorySlotCont {
        public SpriteRenderer Sprite;
        public ArmorSlotView Slot;
    }
}