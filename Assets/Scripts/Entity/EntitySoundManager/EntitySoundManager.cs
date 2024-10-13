using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class EntitySoundManager : NetworkBehaviour
{
    [SerializeField] protected List<AudioClip> idleSounds = new List<AudioClip>();
    [SerializeField] protected List<AudioClip> attackSounds = new List<AudioClip>();
    [SerializeField] protected List<AudioClip> hitSounds = new List<AudioClip>();

    [SerializeField] private AudioSource AudioSource;

    public float IdleCooldown = 1f;
    private float curTime = 0f;
    public void Update()
    {
       if (Time.time - curTime >= (IdleCooldown + Random.Range(-0.25f, 0.25f)))
       {
            PlayIdle();
            curTime = Time.time;
       }
    }
    public void PlayIdle()
    {
        if (idleSounds.Count == 0) return;
        AudioSource.PlayOneShot(idleSounds[Random.Range(0, idleSounds.Count)]);
    }

    public void PlayAttackSound()
    {
        if (attackSounds.Count == 0) return;
        AudioSource.PlayOneShot(attackSounds[Random.Range(0, attackSounds.Count)]);
    }

    public void PlayHitSound()
    {
        if (hitSounds.Count == 0) return;
        AudioSource.PlayOneShot(hitSounds[Random.Range(0, hitSounds.Count)]);
    }
}
