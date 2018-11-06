using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager _instance;
    public bool audioPreferences = true;

    public AudioClip grabGemClip;
    public AudioClip grabPointClip;
    public AudioClip hitWallClip;

    AudioSource _audioSource;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        } else if (_instance != this)
        {
            Debug.LogError("Multiple soundmanagers: deleting self");
            Destroy(this);
        }

        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayGemSound()
    {
        if (audioPreferences)
        _audioSource.PlayOneShot(grabGemClip);
    }

    public void PlayPointSound()
    {
        if (audioPreferences)
        _audioSource.PlayOneShot(grabPointClip);
    }

    public void PlayDieSound()
    {
        if (audioPreferences)
        _audioSource.PlayOneShot(hitWallClip);
    }

    public void PlayMusic(bool on)
    {
        if (_audioSource == null)
        {
            Debug.LogError("Audiosource null");
        }

        if (on && audioPreferences)
        {
            _audioSource.Play();
        }
        else
        {
            _audioSource.Stop();
        }
    }

    public void ToggleAudio()
    {
        audioPreferences = !audioPreferences;
        PlayMusic(audioPreferences);
        GM._instance.Save();
    }
}
