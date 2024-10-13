using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Misc.Managers
{
    public class SoundSettingsController : MonoBehaviour
    {
        [SerializeField] private Slider _masterVolumeSlider;
        [SerializeField] private Slider _BackgroundVolumeSlider;
        [SerializeField] private Slider _musicVolumeSlider;

        private void Awake()
        {
            _masterVolumeSlider.onValueChanged.AddListener((float value) => {
                SoundSettings.MasterVolume = value;
            });

            _BackgroundVolumeSlider.onValueChanged.AddListener((float value) => {
                SoundSettings.BackgroundVolume = value;
            });

            _musicVolumeSlider.onValueChanged.AddListener((float value) => {
                SoundSettings.MusicVolume = value;
            });
        }

        private void OnEnable()
        {
            _masterVolumeSlider.value = SoundSettings.MasterVolume;
            _BackgroundVolumeSlider.value = SoundSettings.BackgroundVolume;
            _musicVolumeSlider.value = SoundSettings.MusicVolume;
        }
    }
}
