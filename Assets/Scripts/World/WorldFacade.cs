using System;
using System.Collections;
using System.Linq;
using Assets.Scripts.Resources.Data;
using Mirror;
using UnityEngine;

namespace Assets.Scripts.World {
    public class WorldFacade : NetworkBehaviour, IWorld
    {
        [SerializeField] private UpperWorldGen _gen;

        public event Action<GenerateStage> GenerateStageChanged;
        public event Action GenerationCompleted;

        private void Start()
        {
            _gen.GenerateStageChanged += OnGenerateStageChanged;
            _gen.GenerationCompleted += OnGenerationCompleted;
        }

        private void OnDestroy()
        {
            _gen.GenerateStageChanged -= OnGenerateStageChanged;
            _gen.GenerationCompleted -= OnGenerationCompleted;
        }

        public void OnGenerationCompleted()
        {
            GenerationCompleted?.Invoke();
        }

        public override void OnStartServer()
        {
            StartCoroutine(Generate("test"));
        }

        public void OnGenerateStageChanged(GenerateStage stage)
        {
            GenerateStageChanged?.Invoke(stage);
        }

        public IEnumerator Generate(string seed) => _gen.Generate(seed);

        //public void Place(Resource res, Vector2 pos) => _gen.Place(res, pos);
    }
}