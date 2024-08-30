using Assets.Scripts.ILifeCycle;
using Assets.Scripts.Player.Data;
using Mirror;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player.Components.Controllers
{
    public class HealController : PlayerComponent
    {
        [SerializeField] private PlayerStatsMax _statsMax;
        [SerializeField] private PlayerStats _stats;
        [SerializeField] private PlayerStatsRecovery _recovery;

        private Dictionary<string, HealData> _healMap = new();

        [Server]
        public void LockByName(string name)
        {
            if (!_healMap.ContainsKey(name) || _healMap[name].IsLocked) return;
            _healMap[name].IsLocked = true;
            removeRecovery(name);
        }

        [Server]
        public void UnLockByName(string name)
        {
            if (!_healMap.ContainsKey(name) || !_healMap[name].IsLocked) return;
            _healMap[name].IsLocked = false;
            addRecovery(name);
        }

        public bool IsHealedByName(string name) => _healMap.ContainsKey(name);

        [Server]
        public void HealByTime(string name, int health, float delta = 1)
        {
            _healMap[name] = new HealData
            {
                StartTime = Time.time,
                Delta = delta,
                Heal = health
            };

            _recovery.HealthRecoveryPerSec += (float)health / delta;
        }

        [Server]
        public void StopHealByName(string name)
        {
            if (!_healMap.ContainsKey(name)) return;
            removeRecovery(name);
            _healMap.Remove(name);
        }

        [Server]
        private void removeRecovery(string name)
        {
            HealData heal = _healMap[name];
            _recovery.HealthRecoveryPerSec -= (float)heal.Heal / heal.Delta;
        }

        [Server]
        private void addRecovery(string name)
        {
            HealData heal = _healMap[name];
            _recovery.HealthRecoveryPerSec += (float)heal.Heal / heal.Delta;
        }

        public override void ServerTick()
        {
            List<string> changed = new();
            foreach (var (name, healData) in _healMap)
            {
                if (Time.time >= healData.StartTime + healData.Delta &&
                    !healData.IsLocked)
                {
                    changed.Add(name);
                }
            }

            foreach (var name in changed)
            {
                HealData healData = _healMap[name];
                StopHealByName(name);
                HealByTime(name, healData.Heal, healData.Delta);
                _stats.Health += healData.Heal;
            }
        }
    }

    public class HealData
    {
        public float StartTime;
        public float Delta;
        public int Heal;
        public bool IsLocked;
    }

}