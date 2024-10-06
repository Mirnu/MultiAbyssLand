using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Menu.View
{
    public class InformationButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private GameObject _tablet;

        public void OnPointerClick(PointerEventData eventData)
        {
            _tablet.SetActive(!_tablet.activeSelf);
        }
    }
}