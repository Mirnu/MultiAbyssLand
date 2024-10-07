using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Menu.View.GameMenu
{
    public class GameMenuStateController : PlayerComponent
    {
        [SerializeField] private GameObject _gameMenuView;
        private InputAction _menuStateChanged => playerManager.PlayerInput.Menu.MenuStateChanged;

        private void Awake()
        {
            _menuStateChanged.Enable();
            _menuStateChanged.performed += OnMenuStateChanged;
        }

        private void OnMenuStateChanged(InputAction.CallbackContext context) => SwitchState();

        public void SwitchState()
        {
            _gameMenuView.SetActive(!_gameMenuView.activeSelf);
            Time.timeScale = _gameMenuView.activeSelf ? 0 : 1;
        }
    }
}