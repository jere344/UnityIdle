using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("AudioSources")]
    [SerializeField]
    private AudioSource _musicSource;
    [SerializeField]
    private AudioSource _sfxSource;

    [Header("AudioClips")]
    [SerializeField]
    private AudioClip _music001;
    [SerializeField]
    private AudioClip _music002;

    private bool IsPlayingFirstMusic = true;

    void Start()
    {
        _musicSource.clip = _music001;
        _musicSource.Play();
    }
    void Update()
    {
        if (!_musicSource.isPlaying)
        {
            if (IsPlayingFirstMusic)
            {
                _musicSource.clip = _music001;
            }
            else
            {
                _musicSource.clip = _music002;
            }

            IsPlayingFirstMusic = !IsPlayingFirstMusic;
            _musicSource.Play();
        }
    }

    public void PlaySound(AudioClip sound)
    {
        _sfxSource.clip = sound;
        _sfxSource.Play();
    }
}
