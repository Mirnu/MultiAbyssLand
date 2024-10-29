using Assets.Scripts.Game.Services;
using Assets.Scripts.Misc.CD;
using Assets.Scripts.Player.Inventory.Containers;
using Assets.Scripts.Player.Inventory.Controllers;
using Assets.Scripts.Resources.Data;
using Assets.Scripts.World.Containers;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Player.Inventory.UI
{
    public class SlotView : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler,
        IPointerUpHandler, IPointerDownHandler
    {
        [Header("Configs")]
        [SerializeField] private float _accelerationByHalfSecond = 1f;

        [Header("Components")]
        [SerializeField] private ItemContainer _itemContainer;
        private ItemService _itemService => ServiceLocator.GetService<ItemService>();

        private int _index;
        public int Index
        {
            get
            {
                return _index;
            }
            set
            {
                _index = value;
                IndexChanged?.Invoke(_index);
            }
        }

        public event Action<int> IndexChanged;
        public event Action OnHover;
        public event Action<int> OnHold;
        public event Action<int> OnRelease;
        public event Action<int> OnLeftClick;
        public event Action<int> OnRightClick;

        private Image _imageView;
        private TMP_Text _textView;

        private Func<bool> _hoverAction;
        private float _hoverTime = 1;

        private void Hold()
        {
            OnHold?.Invoke(Index);
        }

        private void Start()
        {
            _imageView = Array.Find(GetComponentsInChildren<Image>(true), 
                x => x.transform.parent == transform);
            _textView = GetComponentInChildren<TMP_Text>();

            _itemContainer.OnItemsChanged += OnItemsChanged;
            UpdateView();
        }

        private void Update()
        {       
            if (_hoverAction != null && _hoverAction())
            {
                _hoverAction = CDUtils.CycleWait(0.5f / _hoverTime, Hold);
                _hoverTime += _accelerationByHalfSecond;
            }
        }

        private void OnItemsChanged(int index, ItemData data)
        {
            if (index != Index) return;
            UpdateView();
        }

        private void UpdateView()
        {
            ItemData item = _itemContainer[Index];
            if (item == null || item.Id == -1)
            {
                _imageView.enabled = false;

                if (_textView == null) return;
                _textView.text = string.Empty;
                return;
            }

            _imageView.enabled = true;  
            Resource resource = _itemService[item.Id];
            if (item.Count > 1)
            {
                _textView.gameObject.SetActive(true);
                _textView.text = item.Count.ToString();
            }
            else if (_textView != null)
            {
                _textView.gameObject.SetActive(false);
            }
            _imageView.sprite = resource.SpriteInInventory;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                OnLeftClick?.Invoke(Index);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnHover?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                _hoverAction = null;
                _hoverTime = 1;
                OnRightClick?.Invoke(Index);
                OnRelease?.Invoke(Index);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                _hoverAction = CDUtils.CycleWait(0.5f, Hold);
            }
        }
    }
}