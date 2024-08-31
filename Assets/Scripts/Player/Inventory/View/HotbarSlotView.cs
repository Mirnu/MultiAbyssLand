using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Inventory.View {
    public class HotbarSlotView : SlotView {

        [SerializeField] private Sprite selected_sprite;
        private Sprite default_sprite;

        private void Start() {
            default_sprite = GetComponent<Image>().sprite;
        }

        public bool IsSelected = false;

        public void Select() {
            IsSelected = true;
            slotBackground.sprite = selected_sprite;
        }

        public void Deselect() {
            IsSelected = false;
            slotBackground.sprite = default_sprite;
        }

    }
}