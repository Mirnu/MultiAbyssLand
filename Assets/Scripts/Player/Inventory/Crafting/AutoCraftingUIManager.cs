using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Player.Inventory.BackPack;
using Assets.Scripts.Player.Inventory.View;
using Assets.Scripts.Resources.Crafting;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Inventory.Crafting {
    public class AutoCraftingUIManager : MonoBehaviour, IDisposable
    {
        [SerializeField] private GridLayoutGroup _group;
        [SerializeField] private AutoCraftingButton _prefab;
        [SerializeField] private List<AutoCraftingButton> _currentPrefabs = new List<AutoCraftingButton>();
        [SerializeField] private ContainerSelectableSlots _selectableSlots;
        [SerializeField] private RecipeContainer _recipeContainer;

        public void UpdateCraftMenu() {
            Debug.Log("UpdateCraftMenu");
            _currentPrefabs.ForEach(x => UnityEngine.Object.Destroy(x.gameObject));
            _currentPrefabs.Clear();
            var _retrieved = _selectableSlots.components;
            // need to check 4 resources
            var all = _recipeContainer.RetrieveAllAvailable(_retrieved);
            all.ForEach(x => {
                var t = Instantiate(_prefab, _group.transform);
                t.Init(x.Result);
                t.SetEvent(delegate{ 
                    if(!x.RecipeRequirements.All(y => _selectableSlots.components.Any(a => a.resource == y.resource && a.count >= y.count))) {
                        return;
                    } 
                    Craft(x);
                });
                _currentPrefabs.Add(t);
            });
        }

        private void Craft(Recipe res) {
            _selectableSlots.RemoveCraftRes(res);
            _selectableSlots.AddToFirst(res.Result);
        }

        public void Dispose() { _selectableSlots.onInvChanged -= delegate { UpdateCraftMenu(); }; }

        public void Awake() { _selectableSlots.onInvChanged += delegate { UpdateCraftMenu(); }; }
    }
}