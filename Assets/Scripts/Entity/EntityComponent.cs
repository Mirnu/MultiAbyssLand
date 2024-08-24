using Mirror;
using UnityEngine;

namespace Assets.Scripts.Entity
{
    public class EntityComponent : NetworkBehaviour
    {
        [SerializeField] protected EntityManager entityManager;
    }
}