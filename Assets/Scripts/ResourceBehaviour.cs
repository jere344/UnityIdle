using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBehaviour : MonoBehaviour
{
    public int MoneyAmount;
    private int _extraMoneyAmount;

    void Start()
    {

    }

    void Update()
    {

    }

    public void GainGold()
    {
        GameManager.Instance.DisplayMoney.GainGold(MoneyAmount);
        Destroy(gameObject);
    }

    public void GainExtraGold()
    {
        _extraMoneyAmount = GameManager.Instance.DisplayResource.ExtraMoney;
        GameManager.Instance.DisplayMoney.GainGold(_extraMoneyAmount);
        GameManager.Instance.DisplayResource.ExtraMoney = 0;
        gameObject.SetActive(false);
    }
}
