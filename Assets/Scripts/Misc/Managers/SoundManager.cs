using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Misc.Managers
{
    public enum Sounds 
    { 
        HoverUI,
        ClickUI,
        SwordSwing,
        MeadowDayTheme,
        ForestDayTheme,
        SwampDayTheme,
        WindAndTrees
    }

    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private List<SoundConf> _sounds;
        public static SoundManager Instance { private set; get; }

        private AudioSource _audioSource;

        public AudioClip CurrentClip => _audioSource.clip;
        public Sounds? CurrentSound { get; private set; }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            _audioSource = GetComponent<AudioSource>();
            Instance = this;
        }

        public void PlaySound(Sounds sound, bool replay = true)
        {
            if (replay)
            {
                if (CurrentSound == null )
                {
                    CurrentSound = sound;
                }
                else
                {
                    if (CurrentSound == sound)
                    {
                        Debug.Log("Already playing " + sound);
                        return;
                    }
                }
            }
            CurrentSound = sound;
            SoundConf conf = _sounds.Find(x => x.Sound == sound);
            _audioSource.Stop();
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