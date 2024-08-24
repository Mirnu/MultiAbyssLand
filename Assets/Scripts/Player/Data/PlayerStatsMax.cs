using Mirror;
using System;

namespace Assets.Scripts.Player.Data
{
    public class PlayerStatsMax : NetworkBehaviour
    {
        [SyncVar(hook = nameof(OnHealthMaxChanged))] private int _healthMax;
        [SyncVar(hook = nameof(OnManaMaxChanged))] private int _manaMax;
        [SyncVar] private int _foodMax;

        public event Action<int> HealthChanged;
        public event Action<int> ManaChanged;

        public event Action StatsMaxChanged;

        private void OnHealthMaxChanged(int oldValue, int newValue)
        {
            HealthChanged?.Invoke(_healthMax);
        }

        private void OnManaMaxChanged(int oldValue, int newValue)
        {
            ManaChanged?.Invoke(_manaMax);
        }

        public void Init(Template settings)
        {
            HealthMax = settings.HealthMax;
            ManaMax = settings.ManaMax;
            FoodMax = settings.FoodMax;
        }

        public int HealthMax
        {
            get { return _healthMax; }
            set
            {
                _healthMax = value;
                HealthChanged?.Invoke(_healthMax);
                StatsMaxChanged?.Invoke();
            }
        }

        public int ManaMax
        {
            get { return _manaMax; }
            set
            {
                _manaMax = value;
                ManaChanged?.Invoke(_manaMax);
                StatsMaxChanged?.Invoke();
            }
        }

        public int FoodMax
        {
            get { return _foodMax; }
            set
            {
                _foodMax = value;
                StatsMaxChanged?.Invoke();
            }
        }

        public class Template
        {
            public int HealthMax = 100;
            public int ManaMax = 100;
            public int FoodMax = 100;
        }
    }
}