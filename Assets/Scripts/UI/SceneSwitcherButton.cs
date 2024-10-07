using Assets.Scripts.Misc.Constants;
using Mirror;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class SceneSwitcherButton : MonoBehaviour
    {
        [SerializeField] private ScenesEnum _targetScene;
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            string name = Enum.GetName(typeof(ScenesEnum), _targetScene);

            if (_targetScene == ScenesEnum.Menu)
            {
                NetworkServer.Shutdown();
                NetworkClient.Disconnect();
            }

            SceneManager.LoadScene(name);
        }
    }
}