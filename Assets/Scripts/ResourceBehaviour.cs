using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Collect the money earned

public class ResourceBehaviour : MonoBehaviour
{
    public float MoneyAmount;
    private float _extraMoneyAmount;
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
