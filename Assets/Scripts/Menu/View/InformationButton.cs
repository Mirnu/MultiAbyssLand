using Assets.Scripts.Menu.View.Abstract;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Menu.View
{
    public class InformationButton : SoundButton, IPointerClickHandler
    {
        [SerializeField] private GameObject _tablet;

        public override void OnPointerClick(PointerEventData eventData)
        {
            _tablet.SetActive(!_tablet.activeSelf);
            base.OnPointerClick(eventData);
        }
    }
}