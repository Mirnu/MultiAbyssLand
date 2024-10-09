using System.Collections.Generic;
using UnityEngine;
using Mirror;


namespace Assets.Scripts.Entity
{
    public class EntitySpawnManager : NetworkBehaviour
    {
        [SerializeField] public List<SpawnData> _mobSpawnData;
        [SerializeField] private float _spawnRateMax = 20f;
        [SerializeField] private float _spawnRateMin = 10f;
        [SerializeField] private float _playerSimDistance = 30f;
        [SerializeField] private float _playerViewDistance = 10f;

        private float spawnCircleTime = 0f;
        private float curSpawnRate = 0f;

        [HideInInspector] public EntitySpawnManager Instance;

        private void Awake()
        {
            if (!Instance) Instance = this;
            else Destroy(this);
        }

        private void Update()
        {
            if (Time.time - spawnCircleTime > curSpawnRate)
            {

            }
        }

        private void SpawnRandomly()
        {
            foreach (PlayerFacade player in FindObjectsByType(typeof(PlayerFacade), FindObjectsSortMode.None))
            {
                Vector3 playerPos = player.gameObject.transform.position;
                foreach (SpawnData mob in _mobSpawnData)
                {
                    float rate = Random.Range(1, 101);
                    if (rate < mob.SpawnChance * 100) continue;
                    /*float x_min = Random.Range(playerPos.x - _playerViewDistance, playerPos.x + _playerViewDistance);
                    float y = Random.Range(playerPos.y - _playerViewDistance, playerPos.y + _playerViewDistance);*/
                    // логика спавна и проч.
                }
            }
        }
    }
}
