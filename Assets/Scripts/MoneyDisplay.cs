using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _goldAmountText;

    void Update()
    {
        if (GameManager.Instance.GoldAmount >= 1000)
        {
            _goldAmountText.text = (GameManager.Instance.GoldAmount / 1000) + " k";
        }
        else
        {
            _goldAmountText.text = GameManager.Instance.GoldAmount.ToString("");
        }
    }

    public void GainGold(int GainAmount)
    {
        GameManager.Instance.DisplayGoal.PlayerGoalAmount += GainAmount;
        GameManager.Instance.GoldAmount += GainAmount;
    }
}
