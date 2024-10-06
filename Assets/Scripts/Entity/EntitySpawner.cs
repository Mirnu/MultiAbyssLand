using UnityEngine;
using Mirror;

public class EntitySpawner : NetworkBehaviour
{
    public GameObject EntityPrefab;
    public float spawnCooldown = 1f;
    public bool isSpawning = true;
    public float radius = 3f;
    private float _curTime = 0f;

    [Server]
    public void Update()
    {
        if (isSpawning)
        {
            if (Time.time - _curTime >= spawnCooldown)
            {
                Vector2 pos = (Vector2)transform.position + Random.insideUnitCircle * radius;
                var zombie = Instantiate(EntityPrefab, pos, new Quaternion());
                NetworkServer.Spawn(zombie);
                _curTime = Time.time;
            }
            
        }
    }

}
