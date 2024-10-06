using Assets.Scripts.UI;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menu.View
{
    public class TabletView : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private RectTransform _text;

        [SerializeField] private Asymptote _asymptote;

        private void Start()
        {
            _slider.onValueChanged.AddListener(OnSliderValueChanged);
        }

        private void OnSliderValueChanged(float value)
        {
            float y = math.lerp(_asymptote.Down, _asymptote.Up, value);
            _text.anchoredPosition = Vector2.up * y;
        }
    }
}