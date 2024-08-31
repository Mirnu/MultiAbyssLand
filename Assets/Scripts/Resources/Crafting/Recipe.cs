using System;
using System.Collections.Generic;
using Assets.Scripts.Resources.Data;

namespace Assets.Scripts.Resources.Crafting {
    [Serializable]
    public class Recipe {
        public List<RecipeComponent> RecipeRequirements = new List<RecipeComponent>();
        public RecipeComponent Result;
    }
    [Serializable]
    public class RecipeComponent {
        public Resource resource;
        public int count = 1;
        public RecipeComponent(Resource res, int c) {
            resource = res;
            count = c;
        }
    }
}