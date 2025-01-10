using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Scripts")]
    public GoalDisplay DisplayGoal;
    public MoneyDisplay DisplayMoney;
    public ResourceDisplay DisplayResource;
    public ShopGestion GestionShop;
    public ResourceGestion GestionResource;

    [Header("Variables")]
    public int GoldAmount;
    public int PlayerLvl;
    public int PlayerCompetence;
    public int LouisLvl;
    public float LouisCompetence;
    public int JulesLvl;
    public float JulesCompetence;
    public bool JulesV2;
    public int OvenLvl;
    public int OvenCompetence;
    public int LaundryLvl;
    public int LaundryCompetence;
    public bool julesIsAlreadyActivated;

    public TextMeshProUGUI text;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        GoldAmount = 9999999;
    }
}
