using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Misc.Managers
{
    public enum Sounds 
    { 
        HoverUI,
        ClickUI
    }

    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private List<SoundConf> _sounds;
        public static SoundManager Instance { private set; get; }

        private AudioSource _audioSource;


        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            _audioSource = GetComponent<AudioSource>();
            Instance = this;
        }

        public void PlaySound(Sounds sound)
        {
            SoundConf conf = _sounds.Find(x => x.Sound == sound);
            _audioSource.PlayOneShot(conf.Clip);
        }
    }

    [Serializable]
    public struct SoundConf 
    {
        public Sounds Sound;
        public AudioClip Clip;
    }
}