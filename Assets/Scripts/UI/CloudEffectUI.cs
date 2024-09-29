using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.UI
{
    public class CloudEffectUI : MonoBehaviour
    {
        [SerializeField] private List<RectTransform> _cloudSprites;
        [SerializeField] private Asymptote _asymptote;
        [SerializeField] private float _timeLive = 10;
        [SerializeField] private int _maxClouds = 5;
        [SerializeField] private float _cloudSpeed = 10;

        private float _maxRandomTime => _timeLive / _maxClouds;

        private void Awake()
        {
            StartCoroutine(Init());
        }

        private IEnumerator Init()
        {
            while (true)
            {
                Spawn();

                float timeWait = Random.Range(_maxRandomTime * 0.9f, _maxRandomTime);
                yield return new WaitForSeconds(timeWait);
            }
        }

        private void Spawn()
        {
            int yPos = Random.Range(_asymptote.Down, _asymptote.Up);
            RectTransform cloud = Instantiate(_cloudSprites[Random.Range(0, _cloudSprites.Count)], transform);

            cloud.anchoredPosition = new Vector2(cloud.anchoredPosition.x, yPos);
            StartCoroutine(MoveAndDestroyCloud(cloud));
        }

        private IEnumerator MoveAndDestroyCloud(RectTransform cloud)
        {
            float start = Time.time;

            while (start + _timeLive > Time.time)
            {
                cloud.anchoredPosition -= Vector2.right * _cloudSpeed * Time.deltaTime;
                yield return null;
            }
            Destroy(cloud.gameObject);
        }
    }

    [Serializable]
    public struct Asymptote
    {
        public int Up;
        public int Down;
    }
}