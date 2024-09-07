using Assets.Scripts.Resources.Data;
using Mirror;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player.Components
{
    public enum Animations
    {
        UpIdle,
        RightIdle,
        DownIdle,
        LeftIdle,
        UpWalk,
        RightWalk,
        DownWalk,
        LeftWalk
    }

    public sealed class PlayerAnimationController : PlayerComponent
    {
        public event Action<Animations> AnimationChanged;

        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerDirectionController _directionController;
        [SerializeField] private ToolContainer _hand;
        [SerializeField] private Animator _animator;
        [SerializeField] private Animator _armAnimator;

        private int _currentAnimationPosition;
        private bool isWalk = false;

        private Dictionary<Animations, string> _animationMap = new()
        {
            { Animations.UpIdle, "UpIdle" },
            { Animations.RightIdle, "RightIdle" },
            { Animations.DownIdle, "DownIdle" },
            { Animations.LeftIdle, "LeftIdle" },
            { Animations.UpWalk, "UpWalk" },
            { Animations.RightWalk, "RightWalk" },
            { Animations.DownWalk, "DownWalk" },
            { Animations.LeftWalk, "LeftWalk" }
        };

        [Server]
        private void OnDestroy()
        {
            _playerMovement.StartMoved -= OnStartWalk;
            _playerMovement.StopMoved -= OnStopWalk;
            _directionController.DirectionChanged -= OnDirectionChanged;
            _hand.ToolChanged -= OnToolChanged;
        }

        public override void ServerInitialize()
        {
            ChangeAnimation((int)Animations.DownIdle);
            _directionController.DirectionChanged += OnDirectionChanged;
            _playerMovement.StartMoved += OnStartWalk;
            _playerMovement.StopMoved += OnStopWalk;
            _hand.ToolChanged += OnToolChanged;
        }

        [Server]
        private void OnToolChanged(Resource resource) => ReplayAnimation();

        [Server]
        private void OnDirectionChanged(Direction direction)
        {
            ChangeAnimation((int)direction);
        }

        public void ReplayAnimation()
        {
            ChangeAnimation(_currentAnimationPosition);
        }

        [Command]
        public void ChangeAnimation(int animation)
        {
            _currentAnimationPosition = isWalk ? animation > 3
                ? animation : animation + 4 :
                animation > 3 ? animation - 4 : animation;

            if (_currentAnimationPosition != animation)
            {
                AnimationChanged?.Invoke((Animations)_currentAnimationPosition);
            }

            _animator.Play(_animationMap[(Animations)_currentAnimationPosition], -1, 0);

            if (_hand.IsEmpty || !_hand.CurrentResource.IsTakenInHand)
                _armAnimator.SetInteger("State", _currentAnimationPosition);
        }

        [Server]
        private void OnStartWalk()
        {
            isWalk = true;
            ReplayAnimation();
        }

        [Server]
        private void OnStopWalk()
        {
            isWalk = false;
            ReplayAnimation();
        }
    }
}