using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Entity
{
    [System.Serializable]
    public class SpawnData
    {
        public GameObject Prefab;
        public float SpawnChance;
        public int MaxSimCount;
    }
}
