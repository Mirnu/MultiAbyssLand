using System.Collections;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class MovableElement : MonoBehaviour
    {
        [SerializeField] private Vector2 _startPosition;
        [SerializeField] private Vector2 _endPosition;
        [SerializeField] private float _time;
        [SerializeField] private AnimationCurve _curve;

        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            _rectTransform.anchoredPosition = 
                Vector2.Lerp(_startPosition, _endPosition, _curve.Evaluate(Mathf.PingPong(Time.time / _time, 1)));
        }
    }
}