using Assets.Scripts.Player.Components.Controllers;
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
        [SerializeField] private ArmAnimator _armAnimator;


        private int _currentAnimationPosition;
        private bool isWalk = false;

        [Server]
        private void OnDestroy()
        {
            _playerMovement.StartMoved -= OnStartWalk;
            _playerMovement.StopMoved -= OnStopWalk;
            _directionController.DirectionChanged -= OnDirectionChanged;
            _hand.ToolChanged -= OnToolChanged;
            _armAnimator.AnimationEnded -= OnArmAnimationEnded;
        }

        public override void ServerInitialize()
        {
            ChangeAnimation((int)Animations.DownIdle);
            _directionController.DirectionChanged += OnDirectionChanged;
            _playerMovement.StartMoved += OnStartWalk;
            _playerMovement.StopMoved += OnStopWalk;
            _hand.ToolChanged += OnToolChanged;
            _armAnimator.AnimationEnded += OnArmAnimationEnded;
        }

        private void OnArmAnimationEnded(ArmConfigurableAnimation animation)
        {
            if ((int)animation.Animation < 20) return;
            ReplayAnimation();
        }

        [Server]
        private void OnToolChanged(Resource resource) => ReplayAnimation();

        [Server]
        private void OnDirectionChanged(Direction direction)
        {
            ChangeAnimation((int)direction);
        }

        public override void ServerTick()
        {
            if (_hand.IsEmpty || !_hand.CurrentResource.IsTakenInHand)
                _armAnimator.Play(_currentAnimationPosition);
        }

        [Server]
        public void ReplayAnimation()
        {
            ChangeAnimation((int)_directionController.Direction);
        }

        [Command]
        public void ChangeAnimation(int animation)
        {
            if ((int)_armAnimator.CurrentAnimation.Animation >= 20) return;
            _currentAnimationPosition = isWalk ? animation > 3
                ? animation : animation + 4 :
                animation > 3 ? animation - 4 : animation;

            if (_currentAnimationPosition != animation)
            {
                AnimationChanged?.Invoke((Animations)_currentAnimationPosition);
            }

            _animator.Play(Enum.GetName(typeof(Animations), _currentAnimationPosition), -1, 0);
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