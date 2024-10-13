using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Inventory.View;
using Assets.Scripts.Player.Inventory.View;
using Assets.Scripts.Resources.Crafting;
using Assets.Scripts.Resources.Data;
using Mirror;
using UnityEngine;

namespace Assets.Scripts.Player.Inventory.BackPack
{
    public class ContainerSelectableSlots : NetworkBehaviour
    {
        public List<RecipeComponent> components = new List<RecipeComponent>();
        public Action onInvChanged;

        [SerializeField] private List<SelectableSlotView> _slots = new List<SelectableSlotView>();
        [SerializeField] private SlotInfoView _slotInfoView;
        [SerializeField] private Sprite onHover;
        [SerializeField] private AudioClip onHoverClip;

        private Resource _cursorResource;
        private int _cursorCount = 0;
        private bool _;

        public void DoForAll(Action<SelectableSlotView> action) {
            _slots.ForEach(action);
        }

        public override void OnStartClient()
        {
            //
            _slots[0].SetCount(15);
            //
            _slots.ForEach(x => {
                x.LeftMouseClick += delegate { 
                    DoForAll(x => { if (!x.TryGet(out Resource res)) { x.SetTransparent(); } });
                    bindLeftClick(x);
                    UpdateDict();
                };
                x.RightMouseClick += delegate {
                    DoForAll(x => { if (!x.TryGet(out Resource res)) { x.SetTransparent(); } });
                    bindRightClick(x);
                    UpdateDict();
                };
                x.OnCursorEnter += delegate {
                    x.SetBackground(onHover);
                    _slotInfoView.UpdateRes(x.TryGet(out Resource res) ? res : null);
                    DoForAll(x => { if (!x.TryGet(out Resource res)) { x.SetTransparent(); } });
                };
                x.OnCursorExit += delegate {
                    DoForAll(x => { if (!x.TryGet(out Resource res)) { x.SetTransparent(); } });
                    x.ResetBackground();
                    _slotInfoView.Empty();
                };
            });
            DoForAll(x => { if (!x.TryGet(out Resource res)) { x.SetTransparent(); } });
        }

        private void bindLeftClick(SelectableSlotView slot) {
            if(_cursorResource != null) {
                if(slot.TryGet(out Resource res)) {
                    if(res != _cursorResource) { Replace(slot); } 
                    else { slot.SetCount(slot.GetCount() + _cursorCount); EmptyCursor(); }
                } else if(slot.TrySet(_cursorResource)) {
                    //?
                    slot.TrySet(_cursorResource);
                    slot.SetCount(_cursorCount);
                    EmptyCursor();
                }
            } else {
                slot.TryGet(out Resource res);
                _cursorResource = res;
                _cursorCount = slot.GetCount();
                slot.Delete();
            }
        }

        private void UpdateDict() {
            components.Clear();
            _slots.ForEach(slot => {
                if (slot.TryGet(out Resource res)) {
                    if(components.Any(x => x.resource == res)) { 
                        components.Find(x => x.resource == res).count += slot.GetCount();
                    } else {
                        components.Add(new RecipeComponent(res, slot.GetCount()));
                    }
                }
            });
            onInvChanged?.Invoke();
        }

        public void bindCraftLeft(SelectableSlotView slot) {
            if(_cursorResource != null) { return; }
             slot.TryGet(out Resource res);
                _cursorResource = res;
                _cursorCount = slot.GetCount();
                slot.Delete();
        }

        public void RemoveCraftRes(Recipe recipe) {
            Queue<RecipeComponent> q = new Queue<RecipeComponent>();
            recipe.RecipeRequirements.ForEach(x => q.Enqueue(x));
            while (q.Count > 0) {
                var l = q.Dequeue();
                foreach(var slot in _slots) {
                    if(slot.TryGet(out Resource res) && res == l.resource) {
                        if(slot.GetCount() > l.count) {
                            slot.SetCount(slot.GetCount() - l.count);
                        } else {
                            if(slot.GetCount() < l.count) {
                                q.Enqueue(new RecipeComponent(l.resource, l.count - slot.GetCount()));
                            }
                            slot.Delete();
                        }
                        break;
                    }
                }
            }
            UpdateDict();
        }

        public void AddToFirst(RecipeComponent recipeComponent) {
            foreach(var slot in _slots) {
                if(!slot.TryGet(out Resource res) || (res == recipeComponent.resource)) {
                    slot.TrySet(recipeComponent.resource);
                    if(res == recipeComponent.resource) {
                        slot.SetCount(slot.GetCount() + recipeComponent.count);
                    } else {
                        slot.SetCount(recipeComponent.count);
                    }
                    if (slot.TryGetComponent(out HotbarSlotView hotbar)) {
                        hotbar.DesetTransparent();
                        hotbar.TrySet(recipeComponent.resource);
                        if(res == recipeComponent.resource) {
                            hotbar.SetCount(hotbar.GetCount() + recipeComponent.count);
                        } else {
                            hotbar.SetCount(recipeComponent.count);
                        }
                    }
                    UpdateDict();
                    return;
                }
            }
        }

        private void bindRightClick(SelectableSlotView slot) {
            _ = slot.TryGet(out Resource res);
            if (_cursorResource == null) {
                if(_) {
                    _cursorResource = res;
                    _cursorCount = 1;
                    slot.Decrement();
                }
            } else {
                if (!_) {
                    slot.TrySet(_cursorResource);
                    slot.Increment();
                    if(_cursorCount > 1) { _cursorCount--; } 
                    else { EmptyCursor(); }
                } else if(res == _cursorResource) {
                    //Бесконечная хуйня
                    _cursorCount++; slot.Decrement();
                } else {
                    Replace(slot);
                }
            }
        }

        private void EmptyCursor() {
            _cursorResource = null;
            _cursorCount = 0;
        }

        private void Replace(SelectableSlotView slot) {
            slot.TryGet(out Resource res);
            var _temp = res;
            var __ = slot.GetCount();
            slot.TrySet(_cursorResource);
            slot.SetCount(_cursorCount);
            _cursorResource = _temp;
            _cursorCount = __;
        }
    
        
    }
}
