using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player.Components
{
    public class AccesoryAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _hairAnimator;
        [SerializeField] private Animator _paintsAnimator;
        [SerializeField] private Animator _shirtsAnimator;
        [SerializeField] private Animator _eyesAnimator;

        private Dictionary<int, string> _hairAnimationMap = new()
        {
            {0, "Up" },
            {1, "Right" },
            {2, "Down" },
            {3, "Left" }
        };

        public void Play(int animation)
        {
            string animationName = Enum.GetName(typeof(Animations), animation);

            _paintsAnimator.Play(animationName, 0, 0);
            _shirtsAnimator.Play(animationName, 0, 0);

            string AnimationName = _hairAnimationMap[animation > 3 ? animation - 4 : animation];
            _hairAnimator.Play(AnimationName, 0, 0);

            _eyesAnimator.gameObject.SetActive(animation != 0 && animation != 4);
            if (animation == 0) return;
            _eyesAnimator.Play(AnimationName, 0);
        }
    }
}