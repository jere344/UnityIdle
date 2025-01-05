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
    private int indexMachine;
    private int indexCoffeeItems;

    [Header("Money and Price")]
    private int playerMoney;
    private int playerLvlPriceBase = 20;
    private int louisLvlPriceBase = 10;
    private int julesLvlPriceBase = 20;
    private int ovenLvlPriceBase = 15;
    private int laundryLvlPriceBase = 90;

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

    }

    void Update()
    {
        playerMoney = GameManager.Instance.GoldAmount;

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
            _itemPrice.text = "" + itemPrice;
            _playerMoney.text = "" + playerMoney;

            if (indexMachine >= GameManager.Instance.JulesLvl)
            {
                _buttonDisplay.sprite = _buttonBlue;
            }
            else
            {
                _buttonDisplay.sprite = _buttonRed;
            }
            if (GameManager.Instance.JulesLvl < 6)
            {
                itemName += " v1";
                _itemDescription.text += "\n<b>Clique sur toutes les Machines</b>\r\n<i>Nécessite 1 machine en plus</i>";
            }
            else
            {
                itemName += " v2";
                _itemDescription.text += "\n<b>Diminue son temps de clic de 0,2 ms</b>\r\n<i>S'applique à toutes les machines</i>";
            }
        }
        else if (itemScriptable.HaveGameObject)
        {
            ButtonBehaviour();
        } 
        else
        {
            _buyButton.interactable = true;
            PriceBehaviour();
        }
    }

    // Behaviours
    public void ButtonBehaviour()
    {
        if (itemType == Type.BWorker)
        {
            ListType(_workers);
        }

        if (itemType == Type.BMachines)
        {
            ListType(_machines);
        }

        if (itemType == Type.BSets)
        {
            ListType(_set);
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
                    PriceBehaviour();
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
                    PriceBehaviour();
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
                PriceBehaviour();
            }
        }
    }
    public void ListType(List<GameObject> typeOfList)
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
            PriceBehaviour();
        }
    }
    public void PriceBehaviour()
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
            if (itemType == Type.BWorker)
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
    }

    //Buy an Object
    public void BuyObjectsOrGoodies()
    {
        if (playerMoney >= itemPrice)
        {
            playerMoney -= itemPrice;
            _itemPrice.text = "";
            _playerMoney.text = "" + playerMoney;

            if (itemType == Type.BSets)
            {
                if (indexSet >= 0 && indexSet < _set.Count)
                {
                    _set[indexSet].SetActive(true);
                    indexSet += 1;
                    PriceBehaviour();
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
                if (indexMachine >= 0 && indexMachine < _machines.Count)
                {
                    _machines[indexMachine].SetActive(true);
                    _machinesManagers[indexMachine].SetActive(true);
                    indexMachine += 1;
                    PriceBehaviour();
                }
                if (indexMachine >= 5)
                {
                    _buyButton.interactable = false;
                    _buttonDisplay.sprite = _buttonGreen;
                    _itemPrice.text = "";
                }
            }
            else if (itemType == Type.BCoffee)
            {
                if (indexCoffeeItems < _coffee.Count)
                {
                    _coffee[indexCoffeeItems].SetActive(true);
                    indexCoffeeItems += 1;
                    PriceBehaviour();
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
            playerMoney -= itemPrice;

            if (itemIndex == 0)
            {
                GameManager.Instance.PlayerLvl += 1;
                GameManager.Instance.PlayerCompetence += 1;

                float itemOriginalPrice = itemPrice + playerLvlPriceBase * GameManager.Instance.PlayerLvl;
                itemPrice = (int)itemOriginalPrice;
            }
            if (itemIndex == 1)
            {
                GameManager.Instance.LouisLvl += 1;
                GameManager.Instance.LouisCompetence -= 0.2f;

                float itemOriginalPrice = itemPrice + louisLvlPriceBase * GameManager.Instance.LouisLvl;
                itemPrice = (int)itemOriginalPrice;
            }
            if (itemIndex == 2)
            {
                int julesLvl = GameManager.Instance.JulesLvl;

                if (indexMachine > julesLvl)
                {
                    _machines[julesLvl].gameObject.GetComponent<ClickableObject>().CanUseWorker = true;
                    GameManager.Instance.JulesCompetence += 0.5f;
                }

                else if (indexMachine == 6)
                {
                    GameManager.Instance.JulesCompetence -= 0.2f;

                    //if (GameManager.Instance.JulesCompetence <= 0)
                    //{
                    //    _buyButton.interactable = false;
                    //    _buttonDisplay.sprite = _buttonGreen;

                    //}
                }

                julesLvl += 1;
                float itemOriginalPrice = itemPrice + julesLvlPriceBase * GameManager.Instance.JulesLvl;
                itemPrice = (int)itemOriginalPrice;


            }

            PriceBehaviour();
        }
    }
    public void LevelUpClickers()
    {
        if (playerMoney >= itemPrice)
        {
            playerMoney -= itemPrice;
            _itemPrice.text = "";
            _playerMoney.text = "" + playerMoney;

            if (itemIndex == 0)
            {
                GameManager.Instance.OvenCompetence += 1;

                float itemOriginalPrice = itemPrice + ovenLvlPriceBase * GameManager.Instance.OvenLvl;
                itemPrice = (int)itemOriginalPrice;

                GameManager.Instance.OvenLvl++;
            }

            else if (itemIndex == 1)
            {
                GameManager.Instance.LaundryCompetence += 1;

                float itemOriginalPrice = itemPrice + laundryLvlPriceBase * GameManager.Instance.LaundryLvl;
                itemPrice = (int)itemOriginalPrice;

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
                
                GameManager.Instance.LaundryLvl++;
            }
        }
    }
}
