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

    void Start()
    {
        goldAmount = GameManager.Instance.GoldAmount;
        goldAmount = 0;
    }

    void Update()
    {
        if (goldAmount >= 1000)
        {
            _goldAmountText.text = (goldAmount / 1000) + " k";
        }
        else
        {
            _goldAmountText.text = goldAmount.ToString("");
        }
    }

    public void GainGold(int GainAmount)
    {
        GameManager.Instance.DisplayGoal.PlayerGoalAmount += GainAmount;
        goldAmount += GainAmount;
    }
}
