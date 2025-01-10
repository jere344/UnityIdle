using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("AudioSources")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;

    [Header("AudioClips")]
    [SerializeField] private AudioClip _music001;
    [SerializeField] private AudioClip _music002;

    [Header("Conditions")]
    private bool isPlayingFirstMusic = true;

    private void Start()
    {
        PlayMusic(_music001);
    }

    private void Update()
    {
        if (!_musicSource.isPlaying)
        {
            SwitchMusic();
        }
    }

    private void SwitchMusic()
    {
        if (isPlayingFirstMusic)
        {
            PlayMusic(_music002);
        }
        else
        {
            PlayMusic(_music001);
        }

        isPlayingFirstMusic = !isPlayingFirstMusic;
    }

    public void PlayMusic(AudioClip clip)
    {
        _musicSource.Stop();
        _musicSource.clip = clip;
        _musicSource.Play();
    }

    public void PlaySound(AudioClip sound)
    {
        _sfxSource.PlayOneShot(sound);
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (!pauseStatus && !_musicSource.isPlaying)
        {
            _musicSource.Play();
        }
    }
}

