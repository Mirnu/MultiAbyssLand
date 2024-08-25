using System;
using System.Collections;
using Assets.Scripts.World.Generators.GenerationStages;
using UnityEngine;

namespace Assets.Scripts.World {
    public interface IWorld {
        public IEnumerator Generate(string seed);
        public event Action<GenerateStage> GenerateStageChanged;
        public event Action GenerationCompleted;
        //public void Place(Resource res, Vector2 pos);
    }
}