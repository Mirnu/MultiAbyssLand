using Assets.Scripts.Misc.Constants;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Menu.View
{
    public class PlayButton : MonoBehaviour
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();

            _button.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(Scenes.Game);
            });
        }
    }
}