using UnityEngine;

namespace Assets.Scripts.Misc.Managers
{
    public class GameSoundController : MonoBehaviour
    {
        // Лес y = -22.5
        // Болото x = 32
        [SerializeField] private AudioSource _themeAudioSource;
        [SerializeField] private AudioSource _windAudioSource;

        private GameObject _character => PlayerFacade.Instance;
        private Vector2 _characterPos => _character.transform.position;
        private SoundContainer _container => SoundContainer.Instance;

        private void Update()
        {
            if (_characterPos.x > 32)
            {
                PlaySound(SoundType.SwampDayTheme);
            }
            else if (_characterPos.y < -22.5)
            {
                PlaySound(SoundType.ForestDayTheme);
            }
            else
            {
                PlaySound(SoundType.MeadowDayTheme);
            }

            if (Random.Range(0, 10000) == 1) {
                PlaySound(SoundType.WindAndTrees);
            }
        }

        public void PlaySound(SoundType type)
        {
            AudioClip clip = _container.GetSound(type);

            if (type == SoundType.WindAndTrees)
            {
                _windAudioSource.PlayOneShot(clip);
            }
            else
            {
                _themeAudioSource.Stop();
                _themeAudioSource.PlayOneShot(clip);
            }
        }
    }
}
