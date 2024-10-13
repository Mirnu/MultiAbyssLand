using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Assets.Scripts.Misc.Managers;

public class EntitySoundManager : NetworkBehaviour
{
    [SerializeField] protected List<AudioClip> idleSounds = new List<AudioClip>();
    [SerializeField] protected List<AudioClip> attackSounds = new List<AudioClip>();
    [SerializeField] protected List<AudioClip> hitSounds = new List<AudioClip>();

    [SerializeField] protected AudioSource audioSource;

    public float IdleCooldown = 1f;
    private float curTime = 0f;
    public void Update()
    {
        audioSource.volume = SoundSettings.BackgroundVolume * SoundSettings.MasterVolume;
        if (Time.time - curTime >= (IdleCooldown + Random.Range(-0.25f, 0.25f)))
        {
            PlayIdle();
            curTime = Time.time;
        }
    }
    public virtual void PlayIdle()
    {
        if (idleSounds.Count == 0) return;
        audioSource.PlayOneShot(idleSounds[Random.Range(0, idleSounds.Count)]);
    }

    public virtual void PlayAttackSound()
    {
        if (attackSounds.Count == 0) return;
        audioSource.PlayOneShot(attackSounds[Random.Range(0, attackSounds.Count)]);
    }

    public virtual void PlayHitSound()
    {
        if (hitSounds.Count == 0) return;
        audioSource.PlayOneShot(hitSounds[Random.Range(0, hitSounds.Count)]);
    }
}
