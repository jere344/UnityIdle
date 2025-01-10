using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    [Header("Audio")]
    private AudioManager audioManager;
    [SerializeField]
    private AudioClip _sfxSound;

    [Header("Timer")]
    public float Timer;
    public int EndTimer;

    [Header("Gold Values")]
    [SerializeField]
    private int _minGold;
    [SerializeField]
    private int _maxGold;
    private int gainGold;

    [Header("Information Object")]
    [SerializeField]
    private bool objectIsASet;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }
    void Update()
    {
        if (Timer <= EndTimer)
        {
            Timer += Time.deltaTime;
        }

        else if (Timer >= EndTimer)
        {
            if (!objectIsASet)
            {
                gainGold = Random.Range(_minGold, _maxGold);
                GameManager.Instance.DisplayMoney.GainGold(gainGold);
                Timer = 0;
                gameObject.SetActive(false);
            }
            else if (objectIsASet)
            {
                gainGold = Random.Range(_minGold, _maxGold);
                GameManager.Instance.DisplayMoney.GainGold(gainGold);
                Timer = 0;
            }
            audioManager.PlaySound(_sfxSound);
        }
    }
}
