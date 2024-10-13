using Mirror;
using System;
using UnityEngine;

namespace Assets.Scripts.Player.Data
{
    [RequireComponent(typeof(PlayerStatsMax), typeof(PlayerBoost))]
    public class PlayerStats : NetworkBehaviour
    {
        [SyncVar(hook = nameof(OnHealthChanged))] private int _health;
        [SyncVar(hook = nameof(OnManaChanged))] private int _mana;
        [SyncVar(hook = nameof(OnFoodChanged))] private int _food;
        [SyncVar(hook = nameof(OnSpeedChanged))] private float _speed;

        public event Action<int> HealthChanged;
        // Типа сигма заглушка пон да?
        public event Func<(int o, int n), int> ArmorHandle = (db) => { return db.Item2; };
        public event Action<int> ManaChanged;
        public event Action<int> FoodChanged;
        public event Action<float> SpeedChanged;

        public event Action StatsChanged;
        private PlayerStatsMax _maxModel;
        private PlayerBoost _boostModel;

        private void Awake()
        {
            _maxModel = GetComponent<PlayerStatsMax>();
            _boostModel = GetComponent<PlayerBoost>();
        }

        private void OnHealthChanged(int oldValue, int newValue)
        {
            HealthChanged?.Invoke(newValue);
        }

        private void OnManaChanged(int oldValue, int newValue)
        {
            ManaChanged?.Invoke(newValue);
        }

        private void OnFoodChanged(int oldValue, int newValue)
        {
            FoodChanged?.Invoke(newValue);
        }

        private void OnSpeedChanged(float oldValue, float newValue)
        {
            SpeedChanged?.Invoke(newValue);
        }

        public void Init(Template template)
        {
            Health = template.Health;
            Mana = template.Mana;
            Food = template.Food;
            Speed = template.Speed;
        }

        public int Health
        {
            get { return _health; }
            set
            {
                int newValue = Math.Clamp(value, 0, _maxModel.HealthMax);
                newValue = ArmorHandle.Invoke((_health, newValue));
                if (newValue == _health) return;
                _health = newValue;
                HealthChanged?.Invoke(_health);
                StatsChanged?.Invoke();
            }
        }

        public int Mana
        {
            get { return _mana; }
            set
            {
                int newValue = Math.Clamp(value, 0, _maxModel.ManaMax);
                if (newValue == _mana) return;
                _mana = newValue;
                ManaChanged?.Invoke(_mana);
                StatsChanged?.Invoke();
            }
        }

        public int Food
        {
            get { return _food; }
            set
            {
                Debug.Log("Я КОРМЛЮСЬБСЬСЬЬ!~~~  " + value);
                int newValue = Math.Clamp(value, 0, _maxModel.FoodMax);
                if (newValue == _food) return;
                _food = newValue;
                FoodChanged?.Invoke(_food);
                StatsChanged?.Invoke();
            }
        }

        public float Speed
        {
            get { return _speed * _boostModel.SpeedBoost; }
            set
            {
                if (value == _speed) return;
                _speed = value;
                SpeedChanged?.Invoke(_speed);
                StatsChanged?.Invoke();
            }
        }

        public class Template
        {
            public int Health = 100;
            public int Mana = 100;
            public int Food = 100;
            public float Speed = 8;
        }
    }
}