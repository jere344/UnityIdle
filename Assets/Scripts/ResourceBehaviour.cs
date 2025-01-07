using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceBehaviour : MonoBehaviour
{
    public Image ImageReference;
    public TextMeshProUGUI TexteReference;

    private GameObject objectToDeactivate;

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
        objectToDeactivate.SetActive(false);
        Destroy(gameObject);
    }

    public void GainExtraGold()
    {
        _extraMoneyAmount = GameManager.Instance.DisplayResource.ExtraMoney;
        GameManager.Instance.DisplayMoney.GainGold(_extraMoneyAmount);
        GameManager.Instance.DisplayResource.ExtraMoney = 0;
        objectToDeactivate.SetActive(false);
        gameObject.SetActive(false);
    }

    public void SetObjectToDeactivate(GameObject obj)
    {
        objectToDeactivate = obj;
    }

}
