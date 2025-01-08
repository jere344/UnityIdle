using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour
{
    private AudioManager audioManager;
    [SerializeField]
    private AudioClip _sfxSound;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void OnButtonClick()
    {
            audioManager.PlaySound(_sfxSound);
    }
}
