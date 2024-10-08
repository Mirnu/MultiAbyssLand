using Assets.Scripts.Menu.View.Abstract;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Menu.View
{
    public class StateSwitcherButton : SoundButton, IPointerClickHandler
    {
        [SerializeField] private MenuState _state;

        public override void OnPointerClick(PointerEventData eventData)
        {
            StateSwitcher.Instance.SwitchState(_state);
            base.OnPointerClick(eventData);
        }
    }
}