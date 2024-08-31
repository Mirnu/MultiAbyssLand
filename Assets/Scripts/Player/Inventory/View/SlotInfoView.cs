using Assets.Scripts.Resources.Data;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Player.Inventory.View {
    public class SlotInfoView {

        private TextMeshProUGUI _nameText;
        private TextMeshProUGUI _infoText;

        public SlotInfoView(TextMeshProUGUI nameText, TextMeshProUGUI infoText) {
            _nameText = nameText;
            _infoText = infoText;
        }

        public void Update(Resource resource) {
            if(resource == null) { return; }
            _nameText.text = resource.Name;
            _infoText.text = resource.Info;
        }

        public void Empty() {
            _nameText.text = "";
            _infoText.text = "";
        }
    }
}