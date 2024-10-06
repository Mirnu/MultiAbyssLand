using Assets.Scripts.Player.Components;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Entity.Cow
{
    public class CowAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private Animations lastWalkAnimation;

        private Vector2[] _targets = new Vector2[]
        {
            Vector2.up,
            Vector2.right,
            Vector2.down,
            Vector2.left
        };

        public void PlayIdle()
        {
            PlayAnimation(lastWalkAnimation - 4);
        }

        public void PlayAnimation(Animations animation)
        {
            string animationName = Enum.GetName(typeof(Animations), animation);
            _animator.Play(animationName);
        }

        public void PlayAnimation(int animation) => PlayAnimation((Animations)animation);

        public void PlayByDirection(Vector3 direction, bool isWalk = false)
        {
            Vector2 target = FindClosestVector(direction);
            int id = Array.IndexOf(_targets, target);
            lastWalkAnimation = (Animations)id;
            PlayAnimation(isWalk ? id + 4 : id);
        }

        private Vector2 FindClosestVector(Vector2 input)
        {
            var closest = _targets[0];
            float minDistance = Vector2.Distance(input, closest);

            foreach (var target in _targets)
            {
                float distance = Vector2.Distance(input, target);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closest = target;
                }
            }
            Debug.Log(closest);
            return closest;
        }
    }
}