using Assets.Scripts.Entity;
using Assets.Scripts.Game;
using Assets.Scripts.Misc;
using Assets.Scripts.Misc.CD;
using Assets.Scripts.Misc.Managers;
using Assets.Scripts.Resources.Data;
using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.Resources.Tools.Impl
{
    public class MeleeEdgedWeapon : ToolBehaviour
    {
        [SerializeField] protected float speedAttack = 2;
        [SerializeField] protected List<Transform> armTransforms;
        [SerializeField] protected Vector3 center;
        [SerializeField] protected Vector3 size;
        [SerializeField] private AudioSource _audioSource;

        protected PlayerFacade playerFacade;
        protected Action attack;
        protected WaitForFixedUpdate waitForAttack = new WaitForFixedUpdate();
        protected SpriteRenderer spriteRenderer;


        protected bool canAttack = true;

        private Dictionary<int, (int, int)> _angleMap = new() 
        {
            {0, (0, 90) },
            {1, (0, -90) },
            {2, (-180, -90) },
            {3, (-270, -180) },
        };

        private void OnEnable()
        {
            
            playerFacade = FacadeLocator.Singleton.GetFacade<PlayerFacade>();
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            playerFacade.ArmAnimator.AnimationEnded += OnAnimationEnded;
            spriteRenderer.gameObject.SetActive(false);
        }

        protected virtual void OnAnimationEnded()
        {
            canAttack = true;
        }

        [Client]
        protected override void OnHold()
        {
            if (!canAttack) return;
            canAttack = false;
            Attack();
            tool.OnUse?.Invoke();
        }

        protected virtual void Attack()
        {
            _audioSource?.Play();
            Collider[] colliders = Physics.OverlapBox(gameObject.transform.position + center, size)
                .Where(x => x.isTrigger == false).ToArray();
            WeaponResource weaponResource = tool.GetResource<WeaponResource>();

            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent(out EntityFacade facade))
                {
                    facade.TakeDamage(weaponResource.Damage);
                }
            }

            VisualAttack();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(gameObject.transform.position + center, size);
        }

        private void VisualAttack()
        {
            StopAllCoroutines();
            int interval = AngleUtils.GetInterval();
            playerFacade.ArmAnimator.Play(interval + 20, 10);
            transform.localPosition = armTransforms[interval].position;
            transform.rotation = armTransforms[interval].rotation;
            transform.localScale = armTransforms[interval].localScale;
            spriteRenderer.gameObject.SetActive(true);
            StartCoroutine(RotateWeapon(interval));
        }

        protected virtual IEnumerator RotateWeapon(int interval)
        {
            for (float i = 0; i < 1; i += Time.deltaTime * speedAttack)
            {
                yield return null;
                (int, int) angle = _angleMap[interval];
                Vector3 rotation = armTransforms[interval].eulerAngles;
                transform.eulerAngles = new Vector3(rotation.x, rotation.y,
                    math.lerp(angle.Item1, angle.Item2, math.abs(rotation.y / 180) == 1 ? 1 - i : i));
            }
            spriteRenderer.gameObject.SetActive(false);
        }
    }
}