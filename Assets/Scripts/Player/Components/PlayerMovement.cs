using Assets.Scripts.ILifeCycle;
using System;
using UnityEngine;

namespace Assets.Scripts.Player.Components
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : PlayerComponent, IClientTickable
    {
        private PlayerInput _input;
        private PlayerManager _playerManager;
        private Rigidbody _rigidbody;

        public event Action StartMoved;
        public event Action StopMoved;
        public event Action Moved;

        public bool IsStaying { get; private set; } = true;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _playerManager = GetManager();
            _playerManager.RegisterComponent<IClientTickable>(this); 

            if (!isLocalPlayer) return;
            _input = _playerManager.PlayerInput;
        }

        //поверьте мне, когда-нибудь я сделаю entrypoint для клиента
        public override void OnStartClient()
        {
            Camera.main.transform.SetParent(transform);
            Camera.main.transform.localPosition = new Vector3(0f, 0f, -10f);
        }

        public void ClientTick()
        {
            Vector2 direction = _input.Gameplay.Movement.ReadValue<Vector2>();
            if (direction.x != 0 && direction.y != 0)
            {
                direction.x *= 0.7f;
                direction.y *= 0.7f;
            }
            if ((direction.x != 0 || direction.y != 0) && IsStaying)
            {
                StartMoved?.Invoke();
                IsStaying = false;
            }
            if (direction.x == 0 && direction.y == 0 && !IsStaying)
            {
                StopMoved?.Invoke();
                IsStaying = true;
            }
            if (direction.x != 0 || direction.y != 0)
            {
                Moved?.Invoke();
            }
            float speed = Time.deltaTime * 5; //fck mgk num cuz map not ready

            Vector3 deltaPos = new Vector3(direction.x * speed, direction.y * speed,
                direction.y * speed * 0.01f); //fck mgk num cuz map not ready

            _rigidbody.MovePosition(_rigidbody.position + deltaPos);
        }
    }
}