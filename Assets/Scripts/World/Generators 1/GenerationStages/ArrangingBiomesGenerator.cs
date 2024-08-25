using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.World.Biomes;
using Assets.Scripts.World.Internal;
using Mirror;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.World.Generators.GenerationStages
{
    public class ArrangingBiomesGenerator : AbstractGenerator
    {
        private const int COST_GENERATION = 10;
        private const string NAME_GENEARATION = "Arranging Biomes";

        public override int CostGeneration => COST_GENERATION;
        public override string NameGeneration => NAME_GENEARATION;
        public override int Order => 3;

        [SerializeField] private WorldModel model;

        private Tilemap BackgroundTiles;
        private List<Tilemap> DecorTiles;
        private List<Biome> biomes;

        private Dictionary<BiomeFeature, int> _lastGenerated = new Dictionary<BiomeFeature, int>();

        private Queue<Vector2Int> _border = new Queue<Vector2Int>();
        private List<Vector2Int> _processed = new List<Vector2Int>();
        private List<Vector2Int> _all = new List<Vector2Int>();

        private string seed;

        public override void OnStartServer()
        {
            BackgroundTiles = model.BackgroundTiles;
            DecorTiles = model.DecorTiles;
            biomes = model.Biomes;
            base.OnStartServer();
        }

        public override IEnumerator Generate()
        {
            biomes.ForEach(x => GenerateBiome(x));

            yield return new WaitForSeconds(0.1f);
        }

        private void GenerateBiome(Biome biome)
        {
            // Initial splotch
            _border.Enqueue(biome.Center);
            _all.Add(biome.Center);
            while (_border.Count > 0)
            {
                var _current = _border.Dequeue();
                if (Vector2.Distance(_current, biome.Center) <= biome.Size + Random.Range(0, biome.RandomProcent * 0.1f) / 2 && !_processed.Contains(_current))
                {
                    var _currentNeighbors = GetNeighbors(BackgroundTiles, _current);
                    _currentNeighbors.ForEach(x => {
                        if (Random.Range(0f, 100f) < biome.RandomProcent * 10)
                        {
                            BackgroundTiles.SetTile(new Vector3Int(x.x, x.y), biome.Ground);
                            _border.Enqueue(x);
                            _all.Add(x);
                        }
                    });
                    _processed.Add(_current);
                }
            }

            // edging (smoothing)
            _all.ForEach(_current => {
                var _currentNeighbors = GetNeighbors(BackgroundTiles, _current, biome.Ground);
                if (_currentNeighbors.Count() > 1)
                {
                    _currentNeighbors.ForEach(x => {
                        BackgroundTiles.SetTile(new Vector3Int(x.x, x.y), biome.Ground);
                        _processed.Add(x);
                    });
                }
            });

            // getting all cells into _all
            _processed.Except(_all).ToList().ForEach(x => _all.Add(x));

            // Spawning biome features
            _all.ForEach(x => {
                foreach (var f in biome.Features)
                {
                        if (Random.Range(0, 100f) < f.SpawnChance
                        && (GetNeighbors(DecorTiles[(int)f.Layer], new Vector2Int(x.x, x.y), f.FeatureTile[0]).Count() < 1
                        || Random.Range(0, 100) < f.NeighborChance))
                        {
                            DecorTiles[(int)f.Layer].SetTile(new Vector3Int(x.x, x.y), f.FeatureTile[Random.Range(0, f.FeatureTile.Count)]);
                        }
                }
            });
        }
        
        private List<Vector2Int> GetNeighbors(Tilemap _map, Vector2Int pos)
        {
            List<Vector2Int> n = new List<Vector2Int>();
            for (int i = pos.x == 0 ? 0 : pos.x - 1; i <= pos.x + 1; i++)
            {
                if (i != pos.x)
                {
                    n.Add(new Vector2Int(i, pos.y));
                }
            }
            for (int j = pos.y == 0 ? 0 : pos.y - 1; j <= pos.y + 1; j++)
            {
                if (j >= _map.cellBounds.max.y - 1) { break; }
                if (j != pos.y)
                {
                    n.Add(new Vector2Int(pos.x, j));
                }
            }
            return n;
        }

        private List<Vector2Int> GetNeighbors(Tilemap _map, Vector2Int pos, TileBase tile)
        {
            List<Vector2Int> n = new List<Vector2Int>();
            for (int i = pos.x == 0 ? 0 : pos.x - 1; i <= pos.x + 1; i++)
            {
                if (_map.GetTile(new Vector3Int(i, pos.y)) == tile && i != pos.x)
                {
                    n.Add(new Vector2Int(i, pos.y));
                }
            }
            for (int j = pos.y == 0 ? 0 : pos.y - 1; j <= pos.y + 1; j++)
            {
                if (j >= _map.cellBounds.max.y - 1) { break; }
                if (_map.GetTile(new Vector3Int(pos.x, j)) == tile && j != pos.y)
                {
                    n.Add(new Vector2Int(pos.x, j));
                }
            }
            return n;
        }
    }
}