using Assets.Scripts.ILifeCycle;
using Assets.Scripts.World.Blocks;
using Assets.Scripts.World.Managers;
using Mirror;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Player.Components
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : PlayerComponent
    {
        private PlayerInput _input;
        private Rigidbody _rigidbody;
        [SerializeField] private float speedMult = 1f;

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
            playerManager.RegisterComponent<IClientTickable>(this); 

            if (!isLocalPlayer) return;
            _input = playerManager.PlayerInput;
        }

        // поверьте мне, когда-нибудь я сделаю entrypoint для клиента
        // верим
        public override void OnStartClient()
        {
            // Camera.main.transform.SetParent(transform);
            // Camera.main.transform.localPosition = new Vector3(0f, 0f, -10f);
        }

        [Command]
        private void OnMoved() => Moved?.Invoke();
        [Command]
        private void OnStopMoved() => StopMoved?.Invoke();
        [Command]
        private void OnStartMoved() => StartMoved?.Invoke();

        public override void ClientTick()
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
                OnStartMoved();
            }
            if (direction.x == 0 && direction.y == 0 && !IsStaying)
            {
                StopMoved?.Invoke();
                IsStaying = true;
                OnStopMoved();
            }
            if (direction.x != 0 || direction.y != 0)
            {
                Moved?.Invoke();
                OnMoved();
            }
            float speed = Time.deltaTime * speedMult; //fck mgk num cuz map not ready
            float oldZ = _rigidbody.position.z;
            float deltaZ = (_rigidbody.position.y + 62) / (40 + 62) * 10 - oldZ;
            Vector3 deltaPos = new Vector3(direction.x * speed, direction.y * speed,
                deltaZ); //fck mgk num cuz map not ready

            _rigidbody.MovePosition(_rigidbody.position + deltaPos);
        }
    }
}