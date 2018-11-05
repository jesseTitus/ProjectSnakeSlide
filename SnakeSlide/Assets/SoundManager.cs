using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip grabGemClip;
    public AudioClip grabPointClip;
    public AudioClip hitWallClip;

    AudioSource _audioSource;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayGemSound()
    {
        _audioSource.PlayOneShot(grabGemClip);
    }

    public void PlayPointSound()
    {
        _audioSource.PlayOneShot(grabPointClip);
    }

    public void PlayDieSound()
    {
        _audioSource.PlayOneShot(hitWallClip);
    }

    public void SwitchMusic(bool on)
    {
        if (on)
        {
            _audioSource.Play();
        }
        else
        {
            _audioSource.Stop();
        }
    }
}
