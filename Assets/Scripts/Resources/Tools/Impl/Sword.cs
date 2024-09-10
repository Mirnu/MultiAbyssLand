using Assets.Scripts.Game;
using Assets.Scripts.Misc;
using Assets.Scripts.Misc.CD;
using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.Resources.Tools.Impl
{
    public class Sword : ToolBehaviour
    {
        [SerializeField] private float _speedAttack = 0.6f;
        [SerializeField] private List<Transform> _armTransforms;

        private PlayerFacade _playerFacade;
        private Action attack;
        private WaitForFixedUpdate _waitForAttack = new WaitForFixedUpdate();
        private SpriteRenderer _spriteRenderer;

        private bool _canAttack = true;

        private Dictionary<int, (int, int)> _angleMap = new() 
        {
            {0, (0, 90) },
            {1, (0, -90) },
            {2, (-180, -90) },
            {3, (-270, -180) },
        };

        private void OnEnable()
        {
            
            _playerFacade = FacadeLocator.Singleton.GetFacade<PlayerFacade>();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _playerFacade.ArmAnimator.AnimationEnded += OnAnimationEnded;
            _spriteRenderer.gameObject.SetActive(false);
        }

        private void OnAnimationEnded()
        {
            _canAttack = true;
        }

        [Client]
        protected override void OnHold()
        {
            if (!_canAttack) return;
            _canAttack = false;
            Attack();
        }

        private void Attack()
        {
            int interval = AngleUtils.GetInterval();
            _playerFacade.ArmAnimator.Play(interval + 20, 10);
            transform.localPosition = _armTransforms[interval].localPosition;
            _spriteRenderer.gameObject.SetActive(true);
            StopAllCoroutines();
            StartCoroutine(RotateSword(interval));
        }

        private IEnumerator RotateSword(int interval)
        {
            for (float i = 0; i < 1; i += Time.deltaTime * _speedAttack)
            {
                yield return null;
                (int, int) angle = _angleMap[interval];
                transform.eulerAngles = Vector3.forward * 
                    math.lerp(angle.Item1, angle.Item2, i);
            }
            _spriteRenderer.gameObject.SetActive(false);
        }
    }
}