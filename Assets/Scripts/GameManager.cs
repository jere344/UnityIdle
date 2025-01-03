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
    public ClickableObject ovenClickable;

    //Var
    public int GoldAmount = 0;
    public int TristanCompetence;
    public int LouisCompetence;
    public int JulesCompetence;
    public int CakeValue;
    public int LaundryValue;

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
