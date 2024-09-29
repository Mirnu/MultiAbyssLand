using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Menu.View
{
    public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Vector2 _defaultSize;
        [SerializeField] private Sprite _defaultSprite;

        [SerializeField] private Vector2 _hoverSize;
        [SerializeField] private Sprite _hoverSprite;

        private Image _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<Image>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _spriteRenderer.sprite = _hoverSprite;
            _spriteRenderer.rectTransform.sizeDelta = _hoverSize;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _spriteRenderer.sprite = _defaultSprite;
            _spriteRenderer.rectTransform.sizeDelta = _defaultSize;
        }
    }
}