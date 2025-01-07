using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopGestion : MonoBehaviour
{
    [Header("Lists")]
    [SerializeField]
    private List<GameObject> _workers;
    [SerializeField]
    private List<GameObject> _machines;
    [SerializeField]
    private List<GameObject> _machinesManagers;
    [SerializeField]
    private List<GameObject> _set;
    [SerializeField]
    private List<GameObject> _coffee;
    [SerializeField]
    private List<GameObject> _cups;
    [SerializeField]
    public List<Sprite> _sprites;

    [Header("Item Coffee")]
    [SerializeField]
    private GameObject _coffeeCooldownReference;
    private bool coffeeCooldownActivated;
    private bool coffeeButtonActivated;
    [SerializeField]
    private ItemScriptable _restartCoffeeItem;

    [Header("Item Square Cup")]
    [SerializeField]
    private GameObject _sCupCooldownReference;
    [SerializeField]
    private bool sCupCooldownActivated;
    [SerializeField]
    private bool sCupButtonActivated;
    [SerializeField]
    private ItemScriptable _restartSCupItem;

    [Header("Item Round Cup")]
    [SerializeField]
    private GameObject _rCupCooldownReference;
    private bool rCupCooldownActivated;
    private bool rCupButtonActivated;
    [SerializeField]
    private ItemScriptable _restartRCupItem;

    [Header("Items informations")]
    private string itemName;
    private string originalItemDescription;
    private int itemPrice;
    private int itemIndex;
    private Type itemType;

    [Header("Index")]
    private int indexSet;
    private int indexMachine = 1;
    private int indexMachineJules = 1;
    private int indexCoffeeItems;

    [Header("Conditions")]
    private bool canBuyJulesLvl;
    private bool canBuyLaundryLvl;

    [Header("Money and Price")]
    private int playerMoney;
    private int playerLvlPrice = 40;
    private int louisLvlPrice = 20;
    private int julesLvlPrice = 30;
    private int ovenLvlPrice = 15;
    private int laundryLvlPrice = 50;
    private int machinePrice = 50;

    [Header("Level")]
    private int julesLvl;

    [Header("Quantity")]
    private int quantityMachine = 5;
    private int quantitySet = 5;
    private int quantityCoffee = 4;
    private int actualQuantityCoffee ;

    [Header("Buttons")]
    [SerializeField]
    private Sprite _buttonRed;
    [SerializeField]
    private Sprite _buttonGreen;
    [SerializeField]
    private Sprite _buttonBlue;
    [SerializeField]
    private Image _buttonDisplay;
    [SerializeField]
    private Button _buyButton;

    [Header("Item UI")]
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
        julesLvl = GameManager.Instance.JulesLvl;
        actualQuantityCoffee = quantityCoffee;

        playerMoney = 9999;
    }

    void Update()
    {
        //playerMoney = GameManager.Instance.GoldAmount;

        if (coffeeButtonActivated)
        {
            indexCoffeeItems = 0;
            CooldownCoffee();
        }

        if (sCupButtonActivated)
        {
            CooldownSCup();
        }

        if (rCupButtonActivated)
        {
            CooldownRCup();
        }
    }

    // Cooldowns 
    private void CooldownCoffee()
    {
        int endCooldown = _coffeeCooldownReference.GetComponent<ItemBehaviour>().EndTimer;
        float cooldownFloat = _coffeeCooldownReference.GetComponent<ItemBehaviour>().Timer;
        int cooldown = (int)cooldownFloat;
        _itemPrice.text = "" + (endCooldown - cooldown);

        if (cooldown >= endCooldown)
        {
            coffeeButtonActivated = false;
            coffeeCooldownActivated = false;
            _buyButton.interactable = true;

            if (actualQuantityCoffee == 0)
            {
                actualQuantityCoffee = quantityCoffee;
            }
            Debug.Log(_restartCoffeeItem);
            DisplayInformations(_restartCoffeeItem);
        }
    }
    private void CooldownSCup()
    {
        int endCooldown = _sCupCooldownReference.GetComponent<ItemBehaviour>().EndTimer;
        float cooldownFloat = _sCupCooldownReference.GetComponent<ItemBehaviour>().Timer;
        int cooldown = (int)cooldownFloat;
        _itemPrice.text = "" + (endCooldown - cooldown);

        if (cooldown >= endCooldown)
        {
            sCupButtonActivated = false;
            sCupCooldownActivated = false;
            _buyButton.interactable = true;

            if (actualQuantityCoffee == 0)
            {
                actualQuantityCoffee = quantityCoffee;
            }
            Debug.Log(_restartSCupItem);
            DisplayInformations(_restartSCupItem);
        }
    }
    private void CooldownRCup()
    {
        int endCooldown = _rCupCooldownReference.GetComponent<ItemBehaviour>().EndTimer;
        float cooldownFloat = _rCupCooldownReference.GetComponent<ItemBehaviour>().Timer;
        int cooldown = (int)cooldownFloat;
        _itemPrice.text = "" + (endCooldown - cooldown);

        if (cooldown >= endCooldown)
        {
            rCupButtonActivated = false;
            rCupCooldownActivated = false;
            _buyButton.interactable = true;

            if (actualQuantityCoffee == 0)
            {
                actualQuantityCoffee = quantityCoffee;
            }
            Debug.Log(_restartRCupItem);
            DisplayInformations(_restartRCupItem);
        }
    }

    ///---------- If Player clicks on an Item ----------
    
    // Informations 
    public void DisplayInformations(ItemScriptable itemScriptable)
    {
        _buyButton.interactable = true;

        coffeeButtonActivated = false;
        sCupButtonActivated = false;
        rCupButtonActivated = false;

        itemName = itemScriptable.itemName;
        itemPrice = itemScriptable.itemPrice;
        itemType = itemScriptable.itemType;
        itemIndex = itemScriptable.itemIndex;
        _itemDescription.text = itemScriptable.itemDescription;
        originalItemDescription = _itemDescription.text;

        _itemName.text = itemName;
        _itemImage.sprite = itemScriptable.itemImage;
        _itemImage.SetNativeSize();

        Sorting();
    }

    public void Sorting()
    {
        if (itemName == "Tristan")
        {
            _itemPrice.text = "";
            _playerMoney.text = "" + playerMoney;
            _buyButton.interactable = false;
            _buttonDisplay.sprite = _buttonGreen;
        }
        else if (itemName == "Jules (Amélioré)")
        {
            ButtonLvlJules();
        }
        else if (itemName == "Linge (Machine à laver)")
        {
            ButtonLvlLaundry();
        }
        else
        {
            ButtonBehaviour();
        }
    }

    // Specials Item Button Behaviour
    public void ButtonLvlLaundry()
    {
        _itemPrice.text = "" + laundryLvlPrice;
        _playerMoney.text = "" + playerMoney;

        if (indexMachine == _machines.Count)
        {
            _itemDescription.text = originalItemDescription;
            canBuyLaundryLvl = true;
            _buttonDisplay.sprite = _buttonGreen;
        }
        else
        {
            _itemDescription.text = originalItemDescription + "\n<color=#98E5FF>Nécessite 6 machines</color>";
            canBuyLaundryLvl = false;
            _buttonDisplay.sprite = _buttonBlue;
        }
    }
    public void ButtonLvlJules()
    {
        _itemPrice.text = "" + julesLvlPrice;
        _playerMoney.text = "" + playerMoney;

        if (indexMachine < _machines.Count)
        {
            itemName += " v1";
            _itemDescription.text = originalItemDescription + "\r\n<b>Clique sur toutes les Machines</b>\r\n<color=#98E5FF>Nécessite 1 machine en plus</color>";

            if (indexMachine > julesLvl)
            {
                _itemDescription.text = originalItemDescription + "\r\n<b>Clique sur toutes les Machines</b>";

                canBuyJulesLvl = true;
                _buttonDisplay.sprite = _buttonGreen;
            }
            else
            {
                canBuyJulesLvl = false;
                _buttonDisplay.sprite = _buttonBlue;
            }
        }

        if (indexMachineJules == _machinesManagers.Count)
        {
            GameManager.Instance.JulesV2 = true;
            canBuyJulesLvl = true;
            itemName += " v2";
            _itemDescription.text = originalItemDescription + "\n<b>Diminue son temps de clic de 0,2 ms</b>\r\n<i>S'applique à toutes les machines</i>";

            PriceBehaviour(julesLvlPrice);
        }
    }
    
    // Item Button Behaviours
    public void ButtonBehaviour()
    {
        if (itemType == Type.BWorker)
        {
            if (_workers[itemIndex].activeSelf)
            {
                _buyButton.interactable = false;
                _buttonDisplay.sprite = _buttonGreen;
                _itemPrice.text = "";
            }
            else
            {
                _buyButton.interactable = true;
                PriceBehaviour(itemPrice);
            }
        }

        if (itemType == Type.LWorker)
        {
            if (itemIndex == 0)
            {
                PriceBehaviour(playerLvlPrice);
            }
            if (itemIndex == 1)
            {
                if (GameManager.Instance.LouisCompetence <= 0.2f)
                {
                    _buyButton.interactable = false;
                    _buttonDisplay.sprite = _buttonBlue;
                }
                else
                {
                    PriceBehaviour(louisLvlPrice);
                }
            }
            if (itemIndex == 2)
            {
                if (GameManager.Instance.JulesCompetence <= 0.2f)
                {
                    _buyButton.interactable = false;
                    _buttonDisplay.sprite = _buttonBlue;
                }
                else
                {
                    PriceBehaviour(julesLvlPrice);
                }
            }

        }

        if (itemType == Type.LClicker)
        {
            if (itemIndex == 0)
            {
                PriceBehaviour(ovenLvlPrice);
            }
            if (itemIndex == 1)
            {
                PriceBehaviour(laundryLvlPrice);
            }

        }

        if (itemType == Type.BMachines)
        {
            _itemDescription.text = originalItemDescription + "\r\n<color=#98E5FF>Quantité : " + quantityMachine + "</color>";

            if (indexMachine < 6)
            {
                _buyButton.interactable = true;
                PriceBehaviour(machinePrice);
            }
            else
            {
                _buyButton.interactable = false;
                _buttonDisplay.sprite = _buttonGreen;

                _itemPrice.text = "";
            }
        }

        if (itemType == Type.BSets)
        {
            _itemDescription.text = originalItemDescription + "\r\n<color=#98E5FF>Quantité : " + quantitySet + "</color>";

            if (indexSet == _set.Count)
            {
                _buyButton.interactable = false;
                _buttonDisplay.sprite = _buttonGreen;
                _itemPrice.text = "";
            }
            else
            {
                _buyButton.interactable = true;
                PriceBehaviour(itemPrice);
            }
        }

        if (itemType == Type.BCups)
        {
            if (itemIndex == 0)
            {

                if (sCupCooldownActivated)
                {
                    sCupButtonActivated = true;
                    _buyButton.interactable = false;
                    _buttonDisplay.sprite = _buttonBlue;
                    _itemPrice.text = "";
                }
                else if (!sCupCooldownActivated)
                {
                    _buyButton.interactable = true;
                    PriceBehaviour(itemPrice);
                }
            }
            else if (itemIndex == 1)
            {
                if (rCupCooldownActivated)
                {
                    rCupButtonActivated = true;
                    _buyButton.interactable = false;
                    _buttonDisplay.sprite = _buttonBlue;
                    _itemPrice.text = "";
                }
                else
                {
                    _buyButton.interactable = true;
                    PriceBehaviour(itemPrice);
                }
            }
        }

        if (itemType == Type.BCoffee)
        {
            _itemDescription.text = originalItemDescription + "\r\n<color=#98E5FF>Quantité : " + actualQuantityCoffee + "</color>";

            if (coffeeCooldownActivated)
            {
                coffeeButtonActivated = true;
                _buyButton.interactable = false;
                _buttonDisplay.sprite = _buttonBlue;
                _itemPrice.text = "";
            }
            else
            {
                _buyButton.interactable = true;
                PriceBehaviour(itemPrice);
            }
        }
    }
    public void PriceBehaviour(int itemPrice)
    {
        _itemPrice.text = "" + itemPrice;
        _playerMoney.text = "" + playerMoney;

        if (playerMoney >= itemPrice)
        {
            _buttonDisplay.sprite = _buttonGreen;
        }
        else
        {
            _buttonDisplay.sprite = _buttonRed;
        }
    }

    ///---------- If Player clicks on button with price ----------

    // Price Button Behaviour
    public void ShoppingBehaviour()
    {
        if (itemType == Type.BWorker)
        {
            BuyWorkers();
        }

        if (itemType == Type.BSets || itemType == Type.BMachines || itemType == Type.BCoffee || itemType == Type.BCups)
        {
            BuyObjects();
        }

        if (itemType == Type.LWorker)
        {
            LevelUpWorker();
        }

        if (itemType == Type.LClicker)
        {
            LevelUpClickers();
        }
    }

    //001 : Buy a Worker
    public void BuyWorkers()
    {
        if (playerMoney >= itemPrice)
        {
                if (itemIndex >= 0 && itemIndex < _workers.Count)
                {
                    playerMoney -= itemPrice;
                    _itemPrice.text = "";
                    _playerMoney.text = "" + playerMoney;

                    _workers[itemIndex].SetActive(true);
                    _buyButton.interactable = false;
                    _buttonDisplay.sprite = _buttonGreen;
                }
        }
    }

    //002 : Buy an Object
    public void BuyObjects()
    {
        if (playerMoney >= itemPrice)
        {
            _playerMoney.text = "" + playerMoney;

            if (itemType == Type.BSets)
            {
                playerMoney -= itemPrice;

                if (indexSet >= 0 && indexSet < _set.Count)
                {
                    _set[indexSet].SetActive(true);
                    indexSet++;
                    quantitySet--;
                    PriceBehaviour(itemPrice);
                }
                if (indexSet == _set.Count)
                {
                    _buyButton.interactable = false;
                    _buttonDisplay.sprite = _buttonGreen;
                    _itemPrice.text = "";
                }

                _itemDescription.text = originalItemDescription + "\r\n<color=#98E5FF>Quantité : " + quantitySet + "</color>";
            }
            else if (itemType == Type.BMachines)
            {
                playerMoney -= machinePrice;

                if (indexMachine < _machines.Count)
                {
                    _machines[indexMachine].SetActive(true);
                    _machinesManagers[indexMachine].SetActive(true);

                    float itemOriginalPrice = itemPrice + itemPrice * indexMachine;
                    machinePrice = (int)itemOriginalPrice;

                    indexMachine++;
                    quantityMachine--;
                    PriceBehaviour(machinePrice);
                }
                if (indexMachine >= 6)
                {
                    _buyButton.interactable = false;
                    _buttonDisplay.sprite = _buttonGreen;
                    _itemPrice.text = "";
                }

                _itemDescription.text = originalItemDescription + "\r\n<color=#98E5FF>Quantité : " + quantityMachine + "</color>";
            }
            else if (itemType == Type.BCoffee)
            {
                playerMoney -= itemPrice;

                if (indexCoffeeItems < _coffee.Count)
                {
                    _coffee[indexCoffeeItems].SetActive(true);
                    indexCoffeeItems++;
                    actualQuantityCoffee--;
                    PriceBehaviour(itemPrice);
                    _itemDescription.text = originalItemDescription + "\r\n<color=#98E5FF>Quantité : " + actualQuantityCoffee + "</color>";
                }
                if (indexCoffeeItems >= _coffee.Count)
                {
                    _buyButton.interactable = false;
                    _buttonDisplay.sprite = _buttonBlue;
                    _itemPrice.text = "";
                    coffeeCooldownActivated = true;
                    ButtonBehaviour();
                }
            }
            else if (itemType == Type.BCups)
            {
                playerMoney -= itemPrice;
                _itemPrice.text = "";
                _playerMoney.text = "" + playerMoney;

                _cups[itemIndex].SetActive(true);
                _buyButton.interactable = false;
                _buttonDisplay.sprite = _buttonBlue;
                _itemPrice.text = "";

                if (itemIndex == 0)
                {
                    sCupCooldownActivated = true;
                }
                else
                {
                    rCupCooldownActivated = true;
                }

                ButtonBehaviour();
            }
        }
    }
    
    //003 : Level up a Worker
    public void LevelUpWorker()
    {
        if (playerMoney >= itemPrice)
        {
            if (itemIndex == 0)
            {
                GameManager.Instance.PlayerLvl += 1;
                GameManager.Instance.PlayerCompetence += 1;

                float itemOriginalPrice = itemPrice + itemPrice * GameManager.Instance.PlayerLvl;
                playerLvlPrice = (int)itemOriginalPrice;

                playerMoney -= playerLvlPrice;
                PriceBehaviour(playerLvlPrice);
            }
            if (itemIndex == 1)
            {
                GameManager.Instance.LouisCompetence -= 0.2f;

                float itemOriginalPrice = itemPrice + itemPrice * GameManager.Instance.LouisLvl;
                louisLvlPrice = (int)itemOriginalPrice;
                GameManager.Instance.LouisLvl++;

                playerMoney -= louisLvlPrice;
                PriceBehaviour(louisLvlPrice);


                if (GameManager.Instance.LouisCompetence <= 0.2f)
                {
                    GameManager.Instance.LouisCompetence = 0.2f;
                    _buyButton.interactable = false;
                    _buttonDisplay.sprite = _buttonBlue;
                }
            }
            if (itemIndex == 2)
            {
                if (canBuyJulesLvl)
                {
                    float itemOriginalPrice = itemPrice + itemPrice * julesLvl;
                    julesLvlPrice = (int)itemOriginalPrice;

                    julesLvl++;

                    playerMoney -= julesLvlPrice;

                    if (indexMachine > julesLvl && indexMachineJules != 6)
                    {
                        _machinesManagers[indexMachineJules].gameObject.GetComponent<ClickableObject>().CanUseWorker = true;
                        GameManager.Instance.JulesCompetence += 0.5f;
                        indexMachineJules++;


                    }
                    else if (indexMachineJules <= julesLvl)
                    {
                        GameManager.Instance.JulesV2 = true;
                        GameManager.Instance.JulesCompetence -= 0.2f;

                        if (GameManager.Instance.JulesCompetence <= 0.2f)
                        {
                            GameManager.Instance.JulesCompetence = 0.2f;
                            _buyButton.interactable = false;
                            _buttonDisplay.sprite = _buttonBlue;
                        }
                    }
                    
                    ButtonLvlJules();
                }
            }
        }
    }
    
    //004 : Level up a Clicker
    public void LevelUpClickers()
    {
        if (playerMoney >= itemPrice)
        {
            if (itemIndex == 0)
            {
                GameManager.Instance.OvenCompetence += 1;

                float itemOriginalPrice = itemPrice + itemPrice * GameManager.Instance.OvenLvl;
                ovenLvlPrice = (int)itemOriginalPrice;
                playerMoney -= ovenLvlPrice;

                _itemPrice.text = "" + ovenLvlPrice;
                PriceBehaviour(ovenLvlPrice);
                GameManager.Instance.OvenLvl++;
            }

            else if (itemIndex == 1)
            {
                if (!canBuyLaundryLvl)
                {
                    ButtonLvlLaundry();
                }
                else
                {
                    GameManager.Instance.LaundryCompetence += 1;

                    float itemOriginalPrice = itemPrice + itemPrice * GameManager.Instance.LaundryLvl;
                    laundryLvlPrice = (int)itemOriginalPrice;
                    playerMoney -= laundryLvlPrice;

                    _itemPrice.text = "" + laundryLvlPrice;
                    PriceBehaviour(laundryLvlPrice);
                    GameManager.Instance.LaundryLvl++;

                    if (GameManager.Instance.LaundryLvl == 1)
                    {
                        int index = 0;
                        while (index <= _machines.Count)
                        {
                            if (index % 2 == 0)
                            {
                                _machines[index].GetComponent<Image>().sprite = _sprites[0];

                                SpriteState spriteState = new SpriteState
                                {
                                    highlightedSprite = _sprites[0],
                                    pressedSprite = _sprites[1]
                                };

                                _machines[index].GetComponent<Button>().spriteState = spriteState;

                                index += 1;
                            }
                            else
                            {
                                _machines[index].GetComponent<Image>().sprite = _sprites[2];

                                SpriteState spriteState = new SpriteState
                                {
                                    highlightedSprite = _sprites[2],
                                    pressedSprite = _sprites[3]
                                };

                                _machines[index].GetComponent<Button>().spriteState = spriteState;

                                index += 1;
                            }
                        }
                    }

                    else if (GameManager.Instance.LaundryLvl == 10)
                    {
                        int index = 0;
                        while (index <= _machines.Count)
                        {
                            if (index % 2 == 0)
                            {
                                _machines[index].GetComponent<Image>().sprite = _sprites[4];

                                SpriteState spriteState = new SpriteState
                                {
                                    highlightedSprite = _sprites[4],
                                    pressedSprite = _sprites[5]
                                };

                                _machines[index].GetComponent<Button>().spriteState = spriteState;

                                index += 1;
                            }
                            else
                            {
                                _machines[index].GetComponent<Image>().sprite = _sprites[6];

                                SpriteState spriteState = new SpriteState
                                {
                                    highlightedSprite = _sprites[6],
                                    pressedSprite = _sprites[7]
                                };

                                _machines[index].GetComponent<Button>().spriteState = spriteState;

                                index += 1;
                            }
                        }
                    }

                    else if (GameManager.Instance.LaundryLvl == 20)
                    {
                        int index = 0;
                        while (index <= _machines.Count)
                        {
                            if (index % 2 == 0)
                            {
                                Vector3 currentPosition = _machines[index].transform.position;
                                _machines[index].transform.position = new Vector3(currentPosition.x, currentPosition.y + 24, currentPosition.z);
                                _machines[index].GetComponent<Image>().sprite = _sprites[8];
                                _itemImage.SetNativeSize();

                                SpriteState spriteState = new SpriteState
                                {
                                    highlightedSprite = _sprites[8],
                                    pressedSprite = _sprites[9]
                                };

                                _machines[index].GetComponent<Button>().spriteState = spriteState;

                                index += 1;
                            }
                            else
                            {
                                _machines[index].GetComponent<Image>().sprite = _sprites[10];

                                SpriteState spriteState = new SpriteState
                                {
                                    highlightedSprite = _sprites[10],
                                    pressedSprite = _sprites[11]
                                };

                                _machines[index].GetComponent<Button>().spriteState = spriteState;

                                index += 1;
                            }
                        }
                    }

                    else if (GameManager.Instance.LaundryLvl == 30)
                    {
                        int index = 0;
                        while (index <= _machines.Count)
                        {
                            if (index % 2 == 0)
                            {
                                _machines[index].GetComponent<Image>().sprite = _sprites[12];

                                SpriteState spriteState = new SpriteState
                                {
                                    highlightedSprite = _sprites[12],
                                    pressedSprite = _sprites[13]
                                };

                                _machines[index].GetComponent<Button>().spriteState = spriteState;

                                index += 1;
                            }
                            else
                            {
                                _machines[index].GetComponent<Image>().sprite = _sprites[14];

                                SpriteState spriteState = new SpriteState
                                {
                                    highlightedSprite = _sprites[14],
                                    pressedSprite = _sprites[15]
                                };

                                _machines[index].GetComponent<Button>().spriteState = spriteState;

                                index += 1;
                            }
                        }
                    }

                    _playerMoney.text = "" + playerMoney;
                }

            }
        }
    }
}
