using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GoalDisplay DisplayGoal;
    public MoneyDisplay DisplayMoney;
    public ResourceDisplay DisplayResource;
    public ShopGestion GestionShop;
    public ResourceGestion gestionResource;
    public ClickableObject ovenClickable;
   // public ClickableObject washingClickable_001;


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
