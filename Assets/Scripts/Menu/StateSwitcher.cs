using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Menu
{
    [Serializable]
    public enum MenuState
    {
        MainMenu,
        Settings,
        LangSettings,
        ManageSettings,
        SoundSettings,
        GraphicSettings
    }

    public class StateSwitcher : MonoBehaviour
    {
        [SerializeField] private List<MenuStateView> _states;
        [SerializeField] private MenuStateView _defaultState;

        public static StateSwitcher Instance { get; private set; }

        public MenuStateView CurrentState { get; private set; }

        private void Awake() 
        {
            Instance = this;
            SwitchState(_defaultState.State);
        }

        public void SwitchState(MenuState state)
        {
            CurrentState.View?.SetActive(false);
            CurrentState = _states.Find(x => x.State == state);
            CurrentState.View.SetActive(true);
        }
    }

    [Serializable]
    public struct MenuStateView
    {
        public MenuState State;
        public GameObject View;
    }
}