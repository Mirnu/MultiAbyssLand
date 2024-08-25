using System.Collections.Generic;
using Assets.Scripts.World.Biomes;
using Mirror;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.World.Internal
{
    public class WorldModel : NetworkBehaviour
    {
        private int _size;

        public int[,] Map = new int[101, 101];
        public int[,] Durability;

        public List<Tile> Tiles;
        public Tilemap BackgroundTiles;
        public List<Tilemap> DecorTiles;
        public List<Biome> Biomes;
        public WorldSaver Saver;

        // public WorldModel(int size, List<Tile> tiles, Tilemap backgroundTiles, 
        //     List<Tilemap> decorTiles, List<Biome> features, WorldSaver saver)
        // {
        //     _size = size;
        //     Map = new int[size, size];
        //     Durability = new int[size, size];
        //     Tiles = tiles;
        //     BackgroundTiles = backgroundTiles;
        //     DecorTiles = decorTiles;
        //     Biomes = features;
        //     Saver = saver;
        // }

        public int Size => _size;
    }
}
