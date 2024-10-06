using Mirror;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game.Utils
{
    public class ServerGameStateObserver : NetworkBehaviour
    {
        [SerializeField] private ServerGameManager _gameManager;

        private Dictionary<ServerGameState, List<Action>> _serverSubscribersMap = new();
        private Dictionary<ServerGameState, List<Action>> _clientSubscribersMap = new();


        ///<summary>
        ///Если не отписаться, то отпишется автоматически, при выходе из игры
        ///</summary>
        [Server]
        public void ServerSubscribe(ServerGameState state, Action subcriber)
        {
            Debug.Log(state.ToString());
            List<Action> subscribers = _serverSubscribersMap.ContainsKey(state) ?
                _serverSubscribersMap[state] :
                new List<Action>();
            subscribers.Add(subcriber);
            _serverSubscribersMap[state] = subscribers;
        }

        [Server]
        public void ServerUnsubscribe(ServerGameState state, Action subcriber)
        {
            _serverSubscribersMap[state].Remove(subcriber);
        }

        [Client]
        public void ClientSubscribe(ServerGameState state, Action subcriber)
        {
            Debug.Log(state.ToString());
            List<Action> subscribers = _clientSubscribersMap.ContainsKey(state) ?
                _clientSubscribersMap[state] :
                new List<Action>();
            subscribers.Add(subcriber);
            _clientSubscribersMap[state] = subscribers;
        }

        [Client]
        public void ClientUnsubscribe(ServerGameState state, Action subcriber)
        {
            _clientSubscribersMap[state].Remove(subcriber);
        }

        private void Awake()
        {
            _gameManager.GameStateChanged += OnGameStateChanged;
        }

        private void OnGameStateChanged(ServerGameState state)
        {
            if (isServer)
            {
                if (!_serverSubscribersMap.ContainsKey(state)) return;
                CallSubscribers(_serverSubscribersMap[state]);
            }
            
            if (isLocalPlayer)
            {
                if (!_clientSubscribersMap.ContainsKey(state)) return;
                CallSubscribers(_clientSubscribersMap[state]);
            }
        }

        private void CallSubscribers(List<Action> map)
        {
            foreach (var subscriber in map)
            {
                subscriber();
            }
        }

        private void OnDestroy()
        {
            _gameManager.GameStateChanged -= OnGameStateChanged;
        }
    }
}