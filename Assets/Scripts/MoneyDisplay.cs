using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _goldAmountText;
    private int goldAmount;
    private GoalDisplay goalDisplay;

    void Start()
    {
        goldAmount = GameManager.Instance.GoldAmount;
        goldAmount = 0;
        _goldAmountText.text = "Or : " + goldAmount.ToString("");
    }

    void Update()
    {
        
    }

    public void GainGold(int GainAmount)
    {
        goalDisplay.PlayerGoalAmount += GainAmount;
        goldAmount += GainAmount;
        _goldAmountText.text = "Or : " + goldAmount.ToString("");
    }
}
