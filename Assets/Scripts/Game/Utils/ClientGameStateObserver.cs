using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Game.Utils
{
    public class ClientGameStateObserver : MonoBehaviour
    {
        [SerializeField] private ClientGameManager _gameManager;

        private Dictionary<ClientGameState, List<Action>> _subscribersMap = new();

        ///<summary>
        ///Если не отписаться, то отпишется автоматически, при выходе из игры
        ///</summary>
        public void Subscribe(ClientGameState state, Action subcriber)
        {
            Debug.Log(state.ToString());
            List<Action> subscribers = _subscribersMap.ContainsKey(state) ?
                _subscribersMap[state] :
                new List<Action>();
            subscribers.Add(subcriber);
            _subscribersMap[state] = subscribers;
        }

        public void Unsubscribe(ClientGameState state, Action subcriber)
        {
            _subscribersMap[state].Remove(subcriber);
        }

        private void Awake()
        {
            _gameManager.GameStateChanged += OnGameStateChanged;
        }

        private void OnGameStateChanged(ClientGameState state)
        {
            if (!_subscribersMap.ContainsKey(state)) return;
            foreach (var subscriber in _subscribersMap[state])
            {
                Debug.Log(-1);
                subscriber();
            }
        }

        private void OnDestroy()
        {
            _gameManager.GameStateChanged -= OnGameStateChanged;
        }
    }
}