using Assets.Scripts.Menu.View.Abstract;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menu.View.GameMenu
{
    public class ContinueGameMenuButton : SoundButton
    {
        [SerializeField] private GameMenuStateController _gameMenuStateController;
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            _gameMenuStateController.SwitchState();
        }
    }
}