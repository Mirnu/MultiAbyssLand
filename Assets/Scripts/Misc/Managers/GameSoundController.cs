using UnityEngine;

namespace Assets.Scripts.Misc.Managers
{
    public class GameSoundController : MonoBehaviour
    {
        // Лес y = -22.5
        // Болото x = 32

        private GameObject _character => PlayerFacade.Instance;
        private Vector2 _characterPos => _character.transform.position;
        private SoundManager _soundManager => SoundManager.Instance;

        private void Update()
        {
            if (_characterPos.x > 32)
            {
                _soundManager.PlaySound(Sounds.SwampDayTheme);
            }
            else if (_characterPos.y < -22.5)
            {
                _soundManager.PlaySound(Sounds.ForestDayTheme);
            }
            else
            {
                _soundManager.PlaySound(Sounds.MeadowDayTheme);
            }

            //if (Random.Range(0, 100) == 1) {
            //    _soundManager.PlaySound(Sounds.WindAndTrees);
            //}
        }
    }
}
