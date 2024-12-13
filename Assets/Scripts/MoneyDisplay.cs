using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _goldAmountText;
    public float GoldAmount;

    private GoalDisplay goalDisplay;

    void Start()
    {
        goalDisplay = FindObjectOfType<GoalDisplay>();
        GoldAmount = 0;
        _goldAmountText.text = "Or : " + GoldAmount.ToString("");
    }

    void Update()
    {
        
    }

    public void GainGold(int GainAmount)
    {
        goalDisplay.PlayerGoalAmount += GainAmount;
        GoldAmount += GainAmount;
        _goldAmountText.text = "Or : " + GoldAmount.ToString("");
    }
}
