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
    private bool sCupCooldownActivated;
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
    private int itemPrice;
    private int itemIndex;
    private Type itemType;

    [Header("Index")]
    private int indexSet;
    [SerializeField]
    private int indexMachine = 1;
    private int indexCoffeeItems;

    [Header("Money and Price")]
    private int playerMoney;
    private int playerLvlPrice = 40;
    private int louisLvlPrice = 20;
    private int julesLvlPrice = 30;
    private int ovenLvlPrice = 15;
    private int laundryLvlPrice = 30;
    private int machinePrice = 50;
    private int itemPriceReference;

    [Header("Conditions")]
    private bool canBuyJulesLvl;
    private bool canBuyLaundryLvl;

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
        playerMoney = 9999;
    }

    void Update()
    {
        //playerMoney = GameManager.Instance.GoldAmount;

        if (coffeeButtonActivated)
        {
            indexCoffeeItems = 0;
            Cooldown(_coffeeCooldownReference, coffeeCooldownActivated, _restartCoffeeItem);
        }

        if (sCupButtonActivated)
        {
            Cooldown(_sCupCooldownReference, sCupCooldownActivated, _restartSCupItem);
        }

        if (rCupButtonActivated)
        {
            Cooldown(_rCupCooldownReference, rCupCooldownActivated, _restartRCupItem);
        }
    }

    private void Cooldown(GameObject cooldownReference, bool boolReference, ItemScriptable itemReference)
    {
        int endCooldown = cooldownReference.GetComponent<ItemBehaviour>().EndTimer;
        float cooldownFloat = cooldownReference.GetComponent<ItemBehaviour>().Timer;
        int cooldown = (int)cooldownFloat;
        _itemPrice.text = "" + (endCooldown - cooldown);

        if (cooldown >= endCooldown)
        {
            boolReference = false;
            _buyButton.interactable = true;
            DisplayInformations(itemReference);
        }
    }

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

        _itemName.text = itemName;
        _itemImage.sprite = itemScriptable.itemImage;
        _itemImage.SetNativeSize();

        //-- Specials --
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

    // Specials Behaviours
    public void ButtonLvlLaundry()
    {
        _itemPrice.text = "" + machinePrice;
        _playerMoney.text = "" + playerMoney;

        if (indexMachine == _machines.Count)
        {
            _itemDescription.text = "Du linge encore mieux nettoyé !\r\n<b>Augmente l'argent obtenu de 1</b>\r\n<i>S'applique à toutes les ressources</i>";
            canBuyLaundryLvl = true;
            _buttonDisplay.sprite = _buttonGreen;
        }
        else
        {
            _itemDescription.text = "Du linge encore mieux nettoyé !\r\n<b>Augmente l'argent obtenu de 1</b>\r\n<i>S'applique à toutes les ressources</i>\n<color=#98E5FF>Nécessite 6 machines</color>";
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
            _itemDescription.text = "Il devient encore plus fort pour nettoyer et plier rapidement.\r\n<b>Clique sur toutes les Machines</b>\r\n<color=#98E5FF>Nécessite 1 machine en plus</color>";

            if (indexMachine > GameManager.Instance.JulesLvl)
            {
                _itemDescription.text = "Il devient encore plus fort pour nettoyer et plier rapidement.\r\n<b>Clique sur toutes les Machines</b>";

                canBuyJulesLvl = true;
                _buttonDisplay.sprite = _buttonGreen;
            }
            else
            {
                canBuyJulesLvl = false;
                _buttonDisplay.sprite = _buttonBlue;
            }
        }

        if (indexMachine == _machines.Count)
        {
            canBuyJulesLvl = true;
            itemName += " v2";
            _itemDescription.text = "Il devient encore plus fort pour nettoyer et plier rapidement.\n<b>Diminue son temps de clic de 0,2 ms</b>\r\n<i>S'applique à toutes les machines</i>";

            PriceBehaviour(julesLvlPrice);
        }
    }
    // Behaviours
    public void ButtonBehaviour()
    {
        if (itemType == Type.BWorker)
        {
            ListType(_workers, itemPrice);
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
            ListType(_machines, machinePrice);
        }

        if (itemType == Type.BSets)
        {
            ListType(_set, itemPrice);
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
                else
                {
                    _buyButton.interactable = true;
                    PriceBehaviour(itemPrice);
                }
            }
            else
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
    public void ListType(List<GameObject> typeOfList, int itemPrice)
    {
        if (typeOfList[itemIndex].activeSelf)
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
    public void ShoppingBehaviour()
    {
        if (itemType == Type.BWorker)
        {
            BuyWorkers();
        }

        if (itemType == Type.BSets || itemType == Type.BMachines || itemType == Type.BCoffee || itemType == Type.BCups)
        {
            BuyObjectsOrGoodies();
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

    // Buy a Worker
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

    //Buy an Object
    public void BuyObjectsOrGoodies()
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
                    indexSet += 1;
                    PriceBehaviour(itemPrice);
                }
                if (indexSet >= 4)
                {
                    _buyButton.interactable = false;
                    _buttonDisplay.sprite = _buttonGreen;
                    _itemPrice.text = "";
                }
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

                    indexMachine += 1;
                    PriceBehaviour(machinePrice);
                }
                if (indexMachine >= 5)
                {
                    Debug.Log("Full");
                    _buyButton.interactable = false;
                    _buttonDisplay.sprite = _buttonGreen;
                    _itemPrice.text = "";
                }
            }
            else if (itemType == Type.BCoffee)
            {
                playerMoney -= itemPrice;

                if (indexCoffeeItems < _coffee.Count)
                {
                    _coffee[indexCoffeeItems].SetActive(true);
                    indexCoffeeItems += 1;
                    PriceBehaviour(itemPrice);
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

                if (itemIndex < _cups.Count)
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
    }

    // Level up an Item
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
                    int julesLvl = GameManager.Instance.JulesLvl;

                    float itemOriginalPrice = itemPrice + itemPrice * GameManager.Instance.JulesLvl;
                    julesLvlPrice = (int)itemOriginalPrice;

                    GameManager.Instance.JulesLvl++;

                    playerMoney -= julesLvlPrice;

                    if (indexMachine > julesLvl)
                    {
                        _machinesManagers[indexMachine].gameObject.GetComponent<ClickableObject>().CanUseWorker = true;
                        GameManager.Instance.JulesCompetence += 0.5f;
                    }
                    else if (indexMachine == _machines.Count)
                    {
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
    public void LevelUpClickers()
    {
        if (playerMoney >= itemPrice)
        {
            _itemPrice.text = "";
            _playerMoney.text = "" + playerMoney;

            if (itemIndex == 0)
            {
                GameManager.Instance.OvenCompetence += 1;

                float itemOriginalPrice = itemPrice + itemPrice * GameManager.Instance.OvenLvl;
                ovenLvlPrice = (int)itemOriginalPrice;
                playerMoney -= ovenLvlPrice;
                PriceBehaviour(itemPriceReference);
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

                }

            }
        }
    }
}
