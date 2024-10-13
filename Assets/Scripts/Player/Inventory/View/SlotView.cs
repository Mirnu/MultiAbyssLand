
using Assets.Scripts.Resources.Data;
using Mirror;
using TMPro;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Inventory.View {
    public class SlotView : MonoBehaviour
    {
        //tool прокинуть посмотреть работает ли
        [SerializeField] protected Image itemView;

        protected Image slotBackground;
        
        private TextMeshProUGUI _countDisplay;
        private Resource _currentResource;
        private int _currentAmount = 0;
        private Sprite oldBack;

        private void Awake() {
            slotBackground = GetComponent<Image>();   
            oldBack = slotBackground.sprite;
            _countDisplay = GetComponentInChildren<TextMeshProUGUI>(); 
            UpdateCount();
        }

        public void SetBackground(Sprite background) { if (itemView != null) { itemView.color = Color.white; } slotBackground.sprite = background; }

        public void ResetBackground() { if(oldBack == null) { return; } slotBackground.sprite = oldBack; } 

        public virtual bool TryGet(out Resource res) {
            res = _currentResource;
            return _currentResource != null;
        }

        public void SetTransparent() { UpdateCount(); if (itemView != null) { itemView.color = new Color(0, 0, 0, 0); } } 

        public virtual void Delete() {
            _currentAmount = 0;
            _currentResource = null;
            itemView.sprite = null;
            UpdateCount();
        }

        public virtual bool TrySet(Resource newResource) {
            if (newResource == null) return false;
            _currentResource = newResource;
            itemView.sprite = _currentResource.SpriteInInventary;
            return true;
        }

        public virtual int GetCount() {
            return _currentAmount;
        }

        public virtual void SetCount(int amount) {
            _currentAmount = amount;
            UpdateCount();
        }

        public virtual void Increment() { SetCount(_currentAmount + 1); }

        public virtual void Decrement() { 
            if(_currentAmount > 1) { 
                SetCount(_currentAmount - 1); 
            } else {
                Delete();
            }
        }

        public bool CanSubstract(int amount) {
            return _currentResource != null && _currentAmount > amount;
        }

        protected virtual void UpdateCount() {
            if(!_countDisplay) { _countDisplay = GetComponentInChildren<TextMeshProUGUI>(true);}
            if(_currentAmount == 0 || _currentAmount == 1) { _countDisplay.gameObject.SetActive(false); }
            else { _countDisplay.gameObject.SetActive(true); }
            _countDisplay.text = _currentAmount.ToString();
        }
    }
}