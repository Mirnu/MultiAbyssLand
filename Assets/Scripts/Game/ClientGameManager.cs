using System;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public enum ClientGameState
    {
        Loading, // Состояние означает, что началась загрузка геймплея
        Gameplay, // Состояние означает, что игрок начал игровой цикл
    }

    public class ClientGameManager : MonoBehaviour
    {
        private ClientGameState _currentState;
        public ClientGameState CurrentState
        {
            get { return _currentState; }
            private set
            {
                _currentState = value;
                GameStateChanged?.Invoke(value);
            }
        }

        public event Action<ClientGameState> GameStateChanged;

        public void StartLoading()
        {
            CurrentState = ClientGameState.Loading;
        }

        public void StartGameplay()
        {
            CurrentState = ClientGameState.Gameplay;
        }
    }
}