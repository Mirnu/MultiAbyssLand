using System;
using System.Collections.Generic;
using Assets.Scripts.World.Blocks;
using UnityEngine;

namespace Assets.Scripts.World.Blocks {
    public class TimeableBlock : Block, IDisposable {
        [SerializeField] private List<Sprite> Sprites = new List<Sprite>();
        [SerializeField] private float IterationLength = 100f;
        [SerializeField] private int currentIteration = 0;

        private float timr = 0;

        private void Awake() {
            OnFixedUpdate.AddListener(Iterate);
        }

        public void Iterate() {
            if(timr < IterationLength) {
                timr += Time.deltaTime;
            } else {
                timr = 0;
                currentIteration++;
                _renderer.sprite = Sprites[currentIteration];
            }

        }

        public void Dispose()
        {
            OnFixedUpdate.RemoveListener(Iterate);
        }
    }
}
