using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    //Scripts
    public GoalDisplay DisplayGoal;
    public MoneyDisplay DisplayMoney;
    public ResourceDisplay DisplayResource;
    public ShopGestion GestionShop;
    public ResourceGestion gestionResource;
    [HideInInspector]
    public ClickableObject clickableObject;

    //Var
    public int GoldAmount;

    public int PlayerLvl;
    public int PlayerCompetence;
    public int LouisLvl;
    public float LouisCompetence;
    public int JulesLvl;
    public float JulesCompetence;

    public int OvenLvl;
    public int OvenCompetence;
    public int LaundryLvl;
    public int LaundryCompetence;


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
}
