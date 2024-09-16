using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Resources.Data
{
    [CreateAssetMenu(menuName = "Resources/new WeaponResource")]
    public class WeaponResource : Resource
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _speed;

        public int Damage => _damage;
        public float Speed => _speed;
    }
}