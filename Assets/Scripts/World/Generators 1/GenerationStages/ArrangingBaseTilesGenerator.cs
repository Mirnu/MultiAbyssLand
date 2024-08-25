using Assets.Scripts.World.Biomes;
using Assets.Scripts.World.Internal;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Mirror;

namespace Assets.Scripts.World.Generators.GenerationStages
{
    public class ArrangingBaseTilesGenerator : AbstractGenerator
    {
        private const int COST_GENERATION = 10;
        private const string NAME_GENEARATION = "Arranging Ground";

        private int[,] map;

        [SerializeField] private WorldModel model;

        public override int CostGeneration => COST_GENERATION;

        public override string NameGeneration => NAME_GENEARATION;

        public override int Order => 2;

        public List<Tile> Tiles;
        public Tilemap BackgroundTiles;

        public override void OnStartServer()
        {
            map = model.Map;
            BackgroundTiles = model.BackgroundTiles;
            Tiles = model.Tiles;
            base.OnStartServer();
        }

        public override IEnumerator Generate()
        {
            GenerateTilemap(map, BackgroundTiles);

            yield return null;
        }

        private void GenerateTilemap(int[,] map, Tilemap tilemap)
        {
            //tilemap.ClearAllTiles();
            for (int x = 0; x < map.GetUpperBound(0); x++)
            {
                for (int y = 0; y < map.GetUpperBound(1); y++)
                {
                    if (map[x, y] != -1 && tilemap.GetTile(new Vector3Int(x, y)) == null)
                    {
                        tilemap.SetTile(new Vector3Int(x, y, 0), Tiles[map[x, y]]);
                    }
                }
            }
        }

    }
}
