using Assets.Scripts.Misc.Managers;
using UnityEngine;

namespace Assets.Scripts.Misc
{
    [RequireComponent(typeof(AudioSource))]
    public class MainMenuSoundController : MonoBehaviour
    {
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            _audioSource.volume = SoundSettings.MusicVolume * SoundSettings.MasterVolume;
        }
    }
}