using Mirror;
using System;

namespace Assets.Scripts.Game
{
    public enum ServerGameState : byte
    {
        Loading, // Состояние означает, что началась загрузка геймплея
        Generating, // Состояние означает, что мир начал генерироваться
        Gameplay, // Состояние означает, что игрок начал игровой цикл
    }

    public class ServerGameManager : NetworkBehaviour
    {
        [SyncVar(hook = nameof(OnGameStateChanged))] private ServerGameState _currentState;
        public ServerGameState CurrentState
        {
            get { return _currentState; }
            private set
            {
                _currentState = value;
                GameStateChanged?.Invoke(value);
            }
        }

        public event Action<ServerGameState> GameStateChanged;

        [Client]
        private void OnGameStateChanged(ServerGameState old, ServerGameState newState)
        {
            GameStateChanged?.Invoke(newState);
        }

        [Server]
        public void StartLoading()
        {
            CurrentState = ServerGameState.Loading;
        }

        [Server]
        public void StartGameplay()
        {
            CurrentState = ServerGameState.Gameplay;
        }

        [Server]
        public void StartGenerate()
        {
            CurrentState = ServerGameState.Generating;
        }
    }
}