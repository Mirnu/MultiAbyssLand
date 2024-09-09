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
        UpActionStartArm,
        UpActionMidArm,
        UpActionEndArm,
        RightActionStartArm,
        RightActionMidArm,
        RightActionEndArm,
        DownActionStartArm,
        DownActionMidArm,
        DownActionEndArm,
        LeftActionStartArm,
        LeftActionMidArm,
        LeftActionEndArm,
        UpActionArm,
        RightActionArm,
        DownActionArm,
        LeftActionArm,
    }

    public class ArmAnimator : PlayerComponent
    {
        [SerializeField] private Animator _animator;
        public event Action<ArmConfigurableAnimation> AnimationEnded;
        // private PriorityQueue<ArmConfigurableAnimation> _animationsQueue = new();

        public ArmConfigurableAnimation CurrentAnimation { private set; get; }

        public void Play(ArmAnimation animation, int priority = 1)
        {
            if (CurrentAnimation.Priority <= priority)
            {
                CurrentAnimation = new ArmConfigurableAnimation
                {
                    Animation = animation,
                    Priority = priority
                };
                _animator.Play(Enum.GetName(typeof(ArmAnimation), animation));
            }
        }

        public override void ServerTick()
        {
            //wait while animation is playing
            if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.99)
            {
                AnimationEnded?.Invoke(CurrentAnimation);
                CurrentAnimation = default;
            }
        }

        public void Play(int animation, int priority = 0)
        {
            Play((ArmAnimation)animation, priority);
        }
    }

    public struct ArmConfigurableAnimation
    {
        public ArmAnimation Animation;
        public int Priority;
    }
}