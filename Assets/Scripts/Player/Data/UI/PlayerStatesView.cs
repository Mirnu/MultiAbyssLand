using Mirror;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Player.Data.UI
{
    public class PlayerStatesView : NetworkBehaviour
    {
        [Header("States")]
        [SerializeField] private TMP_Text _healthStateView;
        [SerializeField] private TMP_Text _manaStateView;
        [SerializeField] private TMP_Text _foodStateView;

        [Header("Recovery")]
        [SerializeField] private TMP_Text _healthRecoveryView;
        [SerializeField] private TMP_Text _manaRecoveryView;


        [SerializeField] private PlayerStats _playerStatsModel;
        [SerializeField] private PlayerStatsMax _playerStatsMaxModel;
        [SerializeField] private PlayerStatsRecovery _playerStatsRecoveryModel;

        [Client]
        private void OnEnable()
        {
            _playerStatsModel.StatsChanged += OnStatsChanged;
            _playerStatsMaxModel.StatsMaxChanged += OnStatsChanged;
            _playerStatsRecoveryModel.StatsRecoveryChanged += OnStatsRecoveryChanged;
            OnStatsChanged();
            OnStatsRecoveryChanged();
        }

        [Client]
        private void OnDisable()
        {
            _playerStatsModel.StatsChanged -= OnStatsChanged;
            _playerStatsMaxModel.StatsMaxChanged -= OnStatsChanged;
            _playerStatsRecoveryModel.StatsRecoveryChanged -= OnStatsRecoveryChanged;
        }

        private void OnStatsChanged()
        {
            _healthStateView.text = $"{_playerStatsModel.Health}/" +
                $"{_playerStatsMaxModel.HealthMax}";
            _manaStateView.text = $"{_playerStatsModel.Mana}/" +
                $"{_playerStatsMaxModel.ManaMax}";
            _foodStateView.text = $"{_playerStatsModel.Food}/" +
                $"{_playerStatsMaxModel.FoodMax}";
        }

        private void OnStatsRecoveryChanged()
        {
            _healthRecoveryView.text = "+" +
                _playerStatsRecoveryModel.HealthRecoveryPerSec + " здоровья/сек";
            _manaRecoveryView.text = "+" +
                _playerStatsRecoveryModel.ManaRecoveryPerSec + " здоровья/сек";
        }
    }
}