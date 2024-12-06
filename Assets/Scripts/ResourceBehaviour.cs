using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBehaviour : MonoBehaviour
{
    public int MoneyAmount;
    private int _extraMoneyAmount;
    private MoneyDisplay moneyDisplay;
    private ResourceDisplay resourceDisplay;

    void Start()
    {
        moneyDisplay = FindObjectOfType<MoneyDisplay>();
        resourceDisplay = FindObjectOfType<ResourceDisplay>();

    }

    void Update()
    {

    }

    public void GainGold()
    {
        moneyDisplay.GainGold(MoneyAmount);
        Destroy(gameObject);
    }

    public void GainExtraGold()
    {
        _extraMoneyAmount = resourceDisplay.GetComponent<ResourceDisplay>().ExtraMoney;
        moneyDisplay.GainGold(_extraMoneyAmount);
        resourceDisplay.GetComponent<ResourceDisplay>().ExtraMoney = 0;
        gameObject.SetActive(false);
    }
}
