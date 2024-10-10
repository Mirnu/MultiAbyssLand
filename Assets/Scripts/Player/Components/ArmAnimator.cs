using Assets.Scripts.Misc.Collections;
using Mirror;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player.Components.Controllers
{
    public enum ArmAnimation
    {
        UpArm,
        RightArm,
        DownArm,
        LeftArm,
        UpWalkArm,
        RightWalkArm,
        DownWalkArm,
        LeftWalkArm,
        UpActionEndArm,
        UpActionMidArm,
        UpActionStartArm,
        RightActionStartArm,
        RightActionMidArm,
        RightActionEndArm,
        DownActionEndArm,
        DownActionMidArm,
        DownActionStartArm,
        LeftActionEndArm,
        LeftActionMidArm,
        LeftActionStartArm,
        UpActionArm,
        RightActionArm,
        DownActionArm,
        LeftActionArm,
    }

    public class ArmAnimator : PlayerComponent
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private PlayerDirectionController _directionController;
        [SerializeField] private AccesoryAnimator _accesoryAnimator;
        public Action AnimationEnded;
        public Action LateAnimationEnded;
        // private PriorityQueue<ArmConfigurableAnimation> _animationsQueue = new();

        public bool UntilAnimation { private set; get; } = false;

        public Direction ArmDirection { private set; get; }

        public ArmConfigurableAnimation CurrentAnimation { private set; get; }

        public void Play(ArmAnimation animation, int priority = 1)
        {
            if (CurrentAnimation.Priority <= priority && !UntilAnimation)
            {
                CurrentAnimation = new ArmConfigurableAnimation
                {
                    Animation = animation,
                    Priority = priority
                };
                ArmDirection = _directionController.Direction;
                _animator.Play(Enum.GetName(typeof(ArmAnimation), animation), 0, 0f);

                if ((int)animation >= 20)
                {
                    _accesoryAnimator.Play((int)animation - 20);
                }
            }
        }

        public Action PlayUntilEnd(ArmAnimation animation, int priority = 1)
        {
            Play(animation, priority);
            UntilAnimation = true;

            return () =>
            {
                UntilAnimation = false;
                EndAnimation();
            };
        }

        public override void ServerTick()
        {
            //wait while animation is playing
            float time = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (time > 0.99 && !UntilAnimation)
                EndAnimation();
        }

        private void EndAnimation()
        {
            AnimationEnded?.Invoke();
            LateAnimationEnded?.Invoke();
            CurrentAnimation = default;
        }

        public void Play(int animation, int priority = 0)
        {
            Play((ArmAnimation)animation, priority);
        }

        public Action PlayUntilEnd(int animation, int priority = 0)
        {
            return PlayUntilEnd((ArmAnimation)animation, priority);
        }
    }

    public struct ArmConfigurableAnimation
    {
        public ArmAnimation Animation;
        public int Priority;
    }
}