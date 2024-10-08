using Assets.Scripts.Menu.View.Abstract;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Menu.View
{
    public class ExitButton : SoundButton, IPointerClickHandler
    {
        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            Application.Quit();
        }
    }
}