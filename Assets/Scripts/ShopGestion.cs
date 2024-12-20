using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopGestion : MonoBehaviour
{
    public int PlayerCompetence = 1;
    public float WorkerCompetence = 0.5f;
    public int ClickerMoney = 1;

    [SerializeField]
    private Image _itemImage;
    [SerializeField]
    private TextMeshProUGUI _itemName;
    [SerializeField]
    private TextMeshProUGUI _itemDescription;
    [SerializeField]
    private TextMeshProUGUI _itemPrice;

    void Start()
    {

    }


    void Update()
    {
        
    }

    public void DisplayInformations(ShopScriptable itemScriptable)
    {
        _itemImage.sprite = itemScriptable.itemImage;
        _itemName.text = itemScriptable.itemName;
        _itemDescription.text = itemScriptable.itemDescription;
        _itemPrice.text = "" + itemScriptable.itemPrice;
    }


    //--------------------- Player

    public void PlayerUpgrade(int ClicksNumber)
    {
        PlayerCompetence += ClicksNumber;
    }

    //--------------------- Auto-Clicker

    public void WorkerUpgrade(float ClicksTime)
    {
        WorkerCompetence += ClicksTime;
    }


    //---------------------- Objects

    public void ClickerUpgrade(int MoneyAmount)
    {
        ClickerMoney += MoneyAmount;
    }

}
