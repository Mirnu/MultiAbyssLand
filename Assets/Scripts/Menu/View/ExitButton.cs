using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Menu.View
{
    public class ExitButton : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            Application.Quit();
        }
    }
}