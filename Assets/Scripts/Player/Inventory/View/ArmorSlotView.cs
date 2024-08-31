using System;
using Assets.Scripts.Player.Inventory.View;
using Assets.Scripts.Resources.Data;
using UnityEngine;

namespace Assets.Scripts.Inventory.View {
    [Serializable]
    public class ArmorSlotView : SelectableSlotView {
        
        public ArmorSlotType SlotType;
        public event Action OnArmorChanged;

        public override bool TrySet(Resource newResource) {
            if(newResource.GetType() != typeof(ArmorResource) || ((ArmorResource)newResource).SlotType != SlotType) { return false; }
            base.TrySet(newResource);
            OnArmorChanged?.Invoke();
            return true;
        }

        public override int GetCount() { return 1; }

        public override void Delete()
        {
            base.Delete();
            OnArmorChanged?.Invoke();            
        }

        //Типа заглушка потому что я долбоеб пон да?
        protected override void UpdateCount() { }
        public override void SetCount(int amount) { }
        public override void Increment() { }
        public override void Decrement() { }
    }
}