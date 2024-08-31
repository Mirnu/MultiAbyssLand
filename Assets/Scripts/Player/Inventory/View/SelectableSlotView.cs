using System;
using Assets.Scripts.Inventory.View;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Player.Inventory.View
{
    public class SelectableSlotView : SlotView, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public event Action LeftMouseClick;
        public event Action RightMouseClick;
        public event Action OnCursorEnter;
        public event Action OnCursorExit;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                LeftMouseClick?.Invoke();
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                RightMouseClick?.Invoke();
            }
        }

        public void OnPointerEnter(PointerEventData pointerEventData) { OnCursorEnter?.Invoke(); }

        public void OnPointerExit(PointerEventData pointerEventData) { OnCursorExit?.Invoke(); }
    }
}
