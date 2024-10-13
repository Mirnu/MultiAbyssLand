using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.World.Generators.GenerationStages;
using Assets.Scripts.World.Internal;
using Assets.Scripts.World.Managers;
using Mirror;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace Assets.Scripts.World {
    public class UpperWorldGen : NetworkBehaviour {
        
        public float scale = 1.0F;
        private string seed = "";
        private int _size;
        private int[,] map;
        //Заглушка
        private int[,] _durability;

        [SerializeField] private List<FirstTypeManager> _firstTypeManagers;
        [SerializeField] private List<SecondTypeManager> _secondTypeManagers;

        [SerializeField] private List<AbstractGenerator> generators;
        [SerializeField] private WorldModel model;

        private Dictionary<AbstractGenerator, GenerateStage> _sequentialGeneration = new();
        public event Action<GenerateStage> GenerateStageChanged;
        public event Action GenerationCompleted;

        private Coroutine _routine;

        public override void OnStartServer()
        {
            base.OnStartServer();
            map = model.Map;
            _size = model.Size;
            _durability = model.Durability;
            AddGenerators(generators);
        }

        //public void Place(Vector2 pos) => _firstTypeManagers.FirstOrDefault().Place(resource, pos);

        private void AddGenerators(List<AbstractGenerator> generators)
        {
            int allCost = generators.Sum(x => x.CostGeneration);
            _sequentialGeneration = generators.OrderBy((x) => x.Order)
                .ToDictionary(x => x, x => new GenerateStage {
                    NameGeneration = x.NameGeneration, 
                    Cost = (float)x.CostGeneration / allCost
                });
        }

        public IEnumerator Generate(string seed)
        {
            yield return null; //пока нафек
            Debug.Log("Generate");
            Debug.Log(_sequentialGeneration.Count);
            this.seed = seed;
            Random.InitState(seed.GetHashCode());

            foreach (var generator in _sequentialGeneration)
            {
                Debug.Log(generator.Key.NameGeneration);
                GenerateStageChanged?.Invoke(generator.Value);
                yield return generator.Key.Generate();
            }

            GenerationCompleted?.Invoke();
        }
    }

    public struct GenerateStage 
    {
        public string NameGeneration;
        public float Cost;
    }
}