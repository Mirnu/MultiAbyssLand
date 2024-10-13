using Assets.Scripts.Misc.Managers;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Menu.View.Abstract
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
    {
        protected AudioSource _audioSource;

        protected SoundContainer soundManager => SoundContainer.Instance;

        private void OnEnable()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.volume = SoundSettings.MusicVolume * SoundSettings.BackgroundVolume;
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
           PlaySound(SoundType.ClickUI);
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            PlaySound(SoundType.HoverUI);
        }

        private void PlaySound(SoundType type)
        {
            AudioClip clip = soundManager.GetSound(type);
            _audioSource.PlayOneShot(clip);
        }
    }
}