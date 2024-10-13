using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Misc.Managers
{
    public enum SoundType
    { 
        HoverUI,
        ClickUI,
        SwordSwing,
        MeadowDayTheme,
        ForestDayTheme,
        SwampDayTheme,
        WindAndTrees
    }

    public class SoundContainer : MonoBehaviour
    {
        [SerializeField] private List<SoundConf> _sounds;
        public static SoundContainer Instance { private set; get; }
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }

        public AudioClip GetSound(SoundType type)
        {
            return _sounds.Find(x => x.Sound == type).Clip;
        }
    }

    [Serializable]
    public struct SoundConf 
    {
        public SoundType Sound;
        public AudioClip Clip;
    }
}