using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
using Random = UnityEngine.Random;
using Unity.VisualScripting;

namespace Assets.Scripts.Entity
{
    public class EntitySpawnManager : NetworkBehaviour
    {
        [SerializeField] public List<SpawnData> _mobSpawnData;
        [SerializeField] private float _spawnRateMax = 20f;
        [SerializeField] private float _spawnRateMin = 10f;
        [SerializeField] private float _playerSimDistance = 30f;
        [SerializeField] private float _playerViewDistance = 10f;
        [SerializeField] private int _maxEntitiesSimCount = 30;

        private float spawnCircleTime = 0f;
        private float curSpawnRate = 0f;

        private List<GameObject> _mobPool = new List<GameObject>();
        private List<PlayerFacade> players;

        [HideInInspector] public static EntitySpawnManager Instance;


        private void Awake()
        {
            if (!Instance) Instance = this;
            else Destroy(this);
        }

        private void Start()
        {
            
        }

        private void Update()
        {
            if (Time.time - spawnCircleTime > curSpawnRate)
            {
                SpawnRandomly();
                curSpawnRate = Random.Range(_spawnRateMin, _spawnRateMax);
                spawnCircleTime = Time.time;
            }
            UpdatePool();
        }

        [Server]
        private void SpawnRandomly()
        {
            Vector3 playerPos = PlayerFacade.Instance.transform.position;
            foreach (SpawnData mob in _mobSpawnData)
            {
                if (_mobPool.Count >= _maxEntitiesSimCount) continue;
                float rate = Random.Range(1, 101);
                if (rate < mob.SpawnChance * 100) continue;
                float x_min = Random.Range(Math.Min(playerPos.x - _playerViewDistance, playerPos.x - _playerSimDistance), Math.Max(playerPos.x - _playerViewDistance, playerPos.x - _playerSimDistance));
                float x_max = Random.Range(Math.Min(playerPos.x + _playerSimDistance, playerPos.x + _playerViewDistance), Math.Max(playerPos.x + _playerViewDistance, playerPos.x + _playerSimDistance));
                float y_min = Random.Range(Math.Min(playerPos.y - _playerSimDistance, playerPos.y - _playerViewDistance), Math.Max(playerPos.y - _playerSimDistance, playerPos.y - _playerViewDistance));
                float y_max = Random.Range(Math.Min(playerPos.y + _playerSimDistance, playerPos.y + _playerViewDistance), Math.Min(playerPos.y + _playerSimDistance, playerPos.y + _playerViewDistance));
                float x = Random.Range(0f, 1f) < 0.5 ? x_min : x_max;
                float y = Random.Range(0f, 1f) < 0.5 ? y_min : y_max;
                GameObject entity = Instantiate(mob.Prefab, new Vector3(x, y, 0), new Quaternion());
                _mobPool.Add(entity);
                NetworkServer.Spawn(entity);
            }
        }

        /*private void OnDrawGizmos()
        {

                Debug.Log("DRAWWWW");
                var player = PlayerFacade.Instance;
                Gizmos.color = Color.green;
                Gizmos.DrawWireCube(player.gameObject.transform.position, new Vector3(_playerSimDistance * 2, _playerSimDistance * 2, 1));
                Gizmos.DrawWireCube(player.gameObject.transform.position, new Vector3(_playerViewDistance * 2, _playerViewDistance * 2, 1));

        }*/

        [Server]
        private void UpdatePool()
        {
            
            foreach (GameObject entity in new List<GameObject>(_mobPool))
            {
                if (!entity) continue;
                /*if (Vector2.Distance(entity.transform.position, player.transform.position) > _playerSimDistance)*/
                if (Vector2.Distance(entity.transform.position, PlayerFacade.Instance.transform.position) > _playerSimDistance)
                {
                    _mobPool.Remove(entity);
                    Destroy(entity);
                }
            }
        }
    }
}
