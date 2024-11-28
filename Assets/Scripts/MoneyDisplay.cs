using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MoneyDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _goldAmountText;

    private float goldAmount;

    void Start()
    {
        goldAmount = 0;
        _goldAmountText.text = "Or : " + goldAmount.ToString("");
    }

    void Update()
    {
        
    }

    public void GainGold(float GainAmount)
    {
        goldAmount += GainAmount;
        _goldAmountText.text = "Or : " + goldAmount.ToString("");
    }
}
