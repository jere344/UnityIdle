using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    public float Timer;
    public int EndTimer;
    private int gainGold;

    [SerializeField]
    private int _minGold;
    [SerializeField]
    private int _maxGold;
    [SerializeField]
    private bool objectIsASet;

    void Start()
    {
        
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
            else
            {
                gainGold = Random.Range(_minGold, _maxGold);
                GameManager.Instance.DisplayMoney.GainGold(gainGold);
                Timer = 0;
            }
        }
    }
}
