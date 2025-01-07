using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource _musicSource;

    public AudioClip music001;
    public AudioClip music002;

    private bool IsPlayingFirstMusic = true;

    void Start()
    {
        _musicSource.clip = music001;
        _musicSource.Play();
    }
    void Update()
    {
        if (!_musicSource.isPlaying)
        {
            if (IsPlayingFirstMusic)
            {
                _musicSource.clip = music001;
            }
            else
            {
                _musicSource.clip = music002;
            }

            IsPlayingFirstMusic = !IsPlayingFirstMusic;
            _musicSource.Play();
        }
    }
}
