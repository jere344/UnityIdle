using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public int PlayerCompetence = 1;
    public float WorkerCompetence = 0.5f;
    public int ClickerMoney = 1;


    void Start()
    {
        
    }


    void Update()
    {
        
    }


    //--------------------- Player

    public void PlayerUpgrade(int ClicksNumber)
    {
        PlayerCompetence += ClicksNumber;
    }

    //--------------------- Auto-Clicker

    public void WorkerUpgrade(float ClicksTime)
    {
        WorkerCompetence += ClicksTime;
    }


    //---------------------- Objects

    public void ClickerUpgrade(int MoneyAmount)
    {
        ClickerMoney += MoneyAmount;
    }

}
