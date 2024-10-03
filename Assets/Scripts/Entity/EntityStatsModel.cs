using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Entity
{
    [RequireComponent(typeof(EntityMaxStatsModel)), RequireComponent(typeof(Humanoid))]
    public class EntityStatsModel: NetworkBehaviour
    {
        [ReadOnly, SerializeField] private int _HP;
        [ReadOnly, SerializeField] private float _Speed;
        [ReadOnly, SerializeField] private int _Damage;
        [ReadOnly, SerializeField] private bool _CanDie;
        [ReadOnly, SerializeField] private bool _HasAI;
        [ReadOnly, SerializeField] private bool _CanAttack;

        protected EntityMaxStatsModel _MaxStats;

        public event Action<int> HpChanged;
        public event Action<float> SpeedChanged;
        public event Action<int> DamageChanged;
        public event Action<bool> CanDieChanged;
        public event Action<bool> HasAIChanged;
        public event Action<bool> CanAttackChanged;

        public event Action StatsChanged;

        public void Awake()
        {
            _MaxStats = GetComponent<EntityMaxStatsModel>();
            _HP = _MaxStats.HP;
            _Speed = _MaxStats.Speed;
            _Damage = _MaxStats.Damage;
            _CanDie = _MaxStats.CanDie;
            _HasAI = _MaxStats.HasAI;
            _CanAttack = _MaxStats.CanAttack;
        }

        public int HP
        {
            get => _HP;
            set
            {
                int new_value = value > 0 ? value : 0;
                if (new_value == _HP) return;
                _HP = new_value > _MaxStats.HP ? _MaxStats.HP : new_value;
                HpChanged?.Invoke(_HP);
                StatsChanged?.Invoke();
            }
        }
        public float Speed
        {
            get => _Speed;
            set
            {
                float new_value = value > 0 ? value : 0;
                if (new_value == _Speed) return;
                _Speed = new_value > _MaxStats.Speed ? _MaxStats.Speed : new_value;
                SpeedChanged?.Invoke(_Speed);
                StatsChanged?.Invoke();
            }
        }
        public int Damage
        {
            get => _Damage;
            set
            {
                int new_value = value > 0 ? value : 0;
                if (new_value == _Damage) return;
                _Damage = new_value > _MaxStats.Damage ? _MaxStats.Damage : new_value;
                DamageChanged?.Invoke(_Damage);
                StatsChanged?.Invoke();
            }
        }
        public bool CanDie
        {
            get => _CanDie;
            set
            {
                _CanDie = value;
                CanDieChanged?.Invoke(_CanDie);
                StatsChanged?.Invoke();
            }
        }
        public bool HasAI
        {
            get => _HasAI;
            set
            {
                _HasAI = value;
                HasAIChanged?.Invoke(_HasAI);
                StatsChanged?.Invoke();
            }
        }
        public bool CanAttack
        {
            get => _CanAttack;
            set
            {
                _CanAttack = value;
                CanAttackChanged?.Invoke(_CanAttack);
                StatsChanged?.Invoke();
            }
        }
    }
}
