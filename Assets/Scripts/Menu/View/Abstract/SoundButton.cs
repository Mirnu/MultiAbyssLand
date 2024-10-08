using Assets.Scripts.Misc.Managers;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Menu.View.Abstract
{
    public class SoundButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
    {
        protected SoundManager soundManager => SoundManager.Instance;

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            soundManager.PlaySound(Sounds.ClickUI);
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            soundManager.PlaySound(Sounds.HoverUI);
        }
    }
}