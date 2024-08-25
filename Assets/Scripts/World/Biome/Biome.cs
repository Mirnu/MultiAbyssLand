using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.World.Biomes {
    [Serializable]
    public class Biome {
        public List<BiomeFeature> Features = new List<BiomeFeature>();
        public float Size = 0;
        public Vector2Int Center;
        public float RandomProcent = 10f;
        public TileBase Ground;
    }
}