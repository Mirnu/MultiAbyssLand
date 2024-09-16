using Assets.Scripts.Misc;
using Assets.Scripts.Misc.Constants;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Resources.Tools.Impl
{
    public class StabbingWeapon : MeleeEdgedWeapon
    {
        [SerializeField] private float _distance = 5f;
        [SerializeField] private float _timeAttack = 1;
        private int hours => AngleUtils.GetHours();
        private Action StopAnimation;

        private Vector3 _startPosition => armTransforms[hours].position;

        protected override void OnAnimationEnded() { }

        protected override void Attack()
        {
            StopAllCoroutines();

            StopAnimation = playerFacade.ArmAnimator.PlayUntilEnd(hours + 8, 10);
            spriteRenderer.gameObject.SetActive(true);

            StartCoroutine(RotateWeapon(hours));
        }

        protected override IEnumerator RotateWeapon(int hours)
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 direction = ((Vector2)mousePosition - Constants.Center).normalized; 
            Vector3 targetPosition = direction * _distance;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, targetPosition);

            yield return Move(_startPosition, targetPosition);
            yield return Move(targetPosition, _startPosition);
            canAttack = true;
            StopAnimation();
            spriteRenderer.gameObject.SetActive(false);
        }

        private IEnumerator Move(Vector3 from, Vector3 to)
        {
            float elapsedTime = 0;

            while (elapsedTime < _timeAttack / 2)
            {
                transform.localPosition = Vector3.Lerp(from, to, elapsedTime / _timeAttack * 2);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            transform.localPosition = to;
        }
    }
}
