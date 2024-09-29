using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Resources.Crafting {
    [CreateAssetMenu(fileName = "RecipeContainer", menuName = "Data/RecipeContainer", order = 0)]
    public class RecipeContainer : ScriptableObject {
        public List<Recipe> AllRecipes = new List<Recipe>();

        public RecipeContainer(List<Recipe> recipes) {
            AllRecipes = recipes;
        }

        public void TryFindCraft(List<RecipeComponent> l, out RecipeComponent res) {
            if(AllRecipes.Any(x => x.RecipeRequirements.OrderBy(y => y).SequenceEqual(l.OrderBy(x => x)))) {
                res = AllRecipes.Find(x => x.RecipeRequirements.OrderBy(y => y).SequenceEqual(l.OrderBy(x => x))).Result;
            }
            res = null;
        }

        public List<Recipe> RetrieveAllAvailable(List<RecipeComponent> components) {
            return AllRecipes.Where(x => x.RecipeRequirements.All(k => components.Any(j => j.resource == k.resource)) && x.RecipeRequirements.All(o => components.Find(p => p.resource == o.resource).count >= o.count)).ToList();
        }
    }
}