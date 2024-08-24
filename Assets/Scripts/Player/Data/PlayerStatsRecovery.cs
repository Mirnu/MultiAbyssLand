using Mirror;
using System;

namespace Assets.Scripts.Player.Data
{
    public class PlayerStatsRecovery : NetworkBehaviour
    {
        [SyncVar(hook = nameof(OnHealthRecoveryChanged))] private float _healthRecovery = 0;
        [SyncVar(hook = nameof(OnManaRecoveryChanged))] private float _manaRecovery = 0;

        public event Action<float> HealthRecoveryChanged;
        public event Action<float> ManaRecoveryChanged;

        public event Action StatsRecoveryChanged;

        private void OnHealthRecoveryChanged(float oldValue, float newValue)
        {
            HealthRecoveryChanged?.Invoke(newValue);
            StatsRecoveryChanged?.Invoke();
        }

        private void OnManaRecoveryChanged(float oldValue, float newValue)
        {
            ManaRecoveryChanged?.Invoke(newValue);
            StatsRecoveryChanged?.Invoke();
        }

        public float HealthRecoveryPerSec
        {
            get { return _healthRecovery; }
            set
            {
                _healthRecovery = value;
                HealthRecoveryChanged?.Invoke(_healthRecovery);
                StatsRecoveryChanged?.Invoke();
            }
        }

        public float ManaRecoveryPerSec
        {
            get { return _manaRecovery; }
            set
            {
                _manaRecovery = value;
                ManaRecoveryChanged?.Invoke(_manaRecovery);
                StatsRecoveryChanged?.Invoke();
            }
        }
    }
}