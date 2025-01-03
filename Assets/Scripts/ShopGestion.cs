using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopGestion : MonoBehaviour
{
    private Dictionary<string, int> competences = new Dictionary<string, int>();


    [SerializeField]
    public List<GameObject> _gameObjectToActivate;

    public int PlayerCompetence = 1;
    public float WorkerCompetence = 0.5f;
    public int ClickerMoney = 1;

    private int playerMoney;
    private int itemValue;
    private int itemIndex;

    [SerializeField]
    private Sprite buttonRed;
    [SerializeField]
    private Sprite buttonGreen;
    [SerializeField]
    private Image buttonDisplay;

    [SerializeField]
    private Button buyButton;

    [SerializeField]
    private Image _itemImage;
    [SerializeField]
    private TextMeshProUGUI _itemName;
    [SerializeField]
    private TextMeshProUGUI _itemDescription;
    [SerializeField]
    private TextMeshProUGUI _itemPrice;
    [SerializeField]
    private TextMeshProUGUI _playerMoney;

    void Start()
    {
        competences.Add("PlayerCompetence", 1);
        competences.Add("LouisCompetence", 1);
        competences.Add("JulesComptence", 1);
    }


    void Update()
    {
        playerMoney = GameManager.Instance.GoldAmount;
    }

    // Informations 
    public void DisplayInformations(ItemScriptable itemScriptable)
    {

        _itemImage.sprite = itemScriptable.itemImage;
        _itemImage.SetNativeSize();
        _itemName.text = itemScriptable.itemName;
        _itemDescription.text = itemScriptable.itemDescription;
        itemValue = itemScriptable.itemPrice;
        _playerMoney.text = "" + playerMoney;
        itemIndex = itemScriptable.itemIndex;

        if (itemIndex == 10)
        {
            buyButton.interactable = false;
            buttonDisplay.sprite = buttonGreen;
            _itemPrice.text = "";
        }
        else
        {
            ButtonBehaviour();
        }
    }

    // Buy an Item

    public void BuyItem()
    {
        if (playerMoney >= itemValue) 
        {
            playerMoney -= itemValue;

            if (itemIndex >= 0 && itemIndex < _gameObjectToActivate.Count)
            {
                _gameObjectToActivate[itemIndex].SetActive(true);
                buyButton.interactable = false;
                buttonDisplay.sprite = buttonGreen;
                _itemPrice.text = "";
                _playerMoney.text = "" + playerMoney;
            } 
        }
    }

    public void ButtonBehaviour()
    {
        if (_gameObjectToActivate[itemIndex].activeSelf)
        {
            buyButton.interactable = false;
            buttonDisplay.sprite = buttonGreen;
            _itemPrice.text = "";
        }
        else
        {
            buyButton.interactable = true;
            _itemPrice.text = "" + itemValue;

            if (playerMoney >= itemValue)
            {
                buttonDisplay.sprite = buttonGreen;
            }
            else
            {
                buttonDisplay.sprite = buttonRed;
            }
        }
    }

    // Level up an Item

    public void LevelUpItem()
    {
        if (playerMoney >= itemValue)
        {
            playerMoney -= itemValue;

            
        }
            
    }


}
