using Assets.Scripts.Entity;
using Assets.Scripts.Resources.Data;
using Assets.Scripts.World.Blocks;
using Assets.Scripts.World.Managers;
using Mirror;
using System.Drawing;
using UnityEngine;

namespace Assets.Scripts.Resources.Tools.Impl
{
    public class BreakingBlockTool : ToolBehaviour
    {
        [SerializeField] protected Vector3 center;
        [SerializeField] protected Vector3 size;

        [Client]
        protected override void OnUse() => Attack();

        private void Attack()
        {
            Collider[] colliders = Physics.OverlapBox(gameObject.transform.position + center, size);

            foreach (Collider collider in colliders)
            {
                Debug.Log("ну коллайдер, ок");
                if (collider.TryGetComponent(out Block block))
                {
                    Debug.Log("АФИГЕЕЕЕТТЬ ОН БЛООООК НАШЕЛ НЕ ЗРЯ Я МОЛИЛСЯ");
                    FirstTypeManager.Singleton.DamageBlock(block);
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = UnityEngine.Color.red;
            Gizmos.DrawWireCube(gameObject.transform.position + center, size);
        }
    }
}