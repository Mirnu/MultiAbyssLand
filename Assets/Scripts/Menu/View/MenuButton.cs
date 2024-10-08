using Assets.Scripts.Menu.View.Abstract;
using Assets.Scripts.Misc.Managers;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Menu.View
{
    public class MenuButton : SoundButton, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Vector2 _defaultSize;
        [SerializeField] private Sprite _defaultSprite;

        [SerializeField] private Vector2 _hoverSize;
        [SerializeField] private Sprite _hoverSprite;

        private Image _spriteRenderer;

        private void OnDisable() => OnExit();

        private void Awake()
        {
            _spriteRenderer = GetComponent<Image>();
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            _spriteRenderer.sprite = _hoverSprite;
            _spriteRenderer.rectTransform.sizeDelta = _hoverSize;
            base.OnPointerEnter(eventData);
        }

        public void OnPointerExit(PointerEventData eventData) => OnExit();

        private void OnExit()
        {
            _spriteRenderer.sprite = _defaultSprite;
            _spriteRenderer.rectTransform.sizeDelta = _defaultSize;
        }
    }
}