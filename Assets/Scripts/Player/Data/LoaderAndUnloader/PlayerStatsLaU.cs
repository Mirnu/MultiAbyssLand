using Mirror;
using UnityEngine;

namespace Assets.Scripts.Player.Data.LoaderAndUnloader
{
    public class PlayerStatsLaU : PlayerComponent
    {

        [SerializeField] private PlayerStatsMax _playerStatsMaxModel;
        [SerializeField] private PlayerStats _playerStatsModel;

        public override void ServerInitialize()
        { 
            _playerStatsMaxModel.Init(new PlayerStatsMax.Template { FoodMax = 100, HealthMax = 100, ManaMax = 100 });
            _playerStatsModel.Init(new PlayerStats.Template { Food = 100, Health = 100, Mana = 100, Speed = 5 });
        }
    }
}