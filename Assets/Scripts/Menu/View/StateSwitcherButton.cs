using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Menu.View
{
    public class StateSwitcherButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private MenuState _state;

        public void OnPointerClick(PointerEventData eventData)
        {
            StateSwitcher.Instance.SwitchState(_state);
        }
    }
}