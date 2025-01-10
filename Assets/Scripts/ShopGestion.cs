using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopGestion : MonoBehaviour
{
    [Header("Audio")]
    private AudioManager audioManager;
    [SerializeField]
    private AudioClip _sfxSound;

    [Header("Lists")]
    public List<GameObject> Workers;
    [SerializeField]
    private List<GameObject> _machines;
    [SerializeField]
    private List<GameObject> _machinesShadows;
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
    [SerializeField]
    public List<Sprite> _spritesShadows;

    [Header("Item Coffee")]
    [SerializeField]
    private GameObject _coffeeCooldownReference;
    private bool coffeeCooldownActivated;
    private bool coffeeButtonActivated;
    private bool startTimerCoffee;
    private float endCooldownCoffee;
    private float cooldownCoffee;
    [SerializeField]
    private ItemScriptable _restartCoffeeItem;

    [Header("Item Square Cup")]
    [SerializeField]
    private GameObject _sCupCooldownReference;
    public bool sCupCooldownActivated;
    public bool sCupButtonActivated;
    [SerializeField]
    private ItemScriptable _restartSCupItem;
    private bool startTimerSCup;
    private float endCooldownSCup;
    private float cooldownSCup;

    [Header("Item Round Cup")]
    [SerializeField]
    private GameObject _rCupCooldownReference;
    private bool rCupCooldownActivated;
    private bool rCupButtonActivated;
    private bool startTimerRCup;
    private float endCooldownRCup;
    private float cooldownRCup;
    [SerializeField]
    private ItemScriptable _restartRCupItem;

    [Header("Items informations")]
    private string itemName;
    private int itemPrice;
    private int itemIndex;
    private Type itemType;
    private string originalItemName;
    private string originalItemDescription;

    [Header("Index")]
    private int indexSet;
    public int indexMachine = 1;
    public int indexMachineShadow = 1;
    private int indexMachineJules = 1;
    private int indexCoffeeItems;
    private int indexSpriteMachine;

    [Header("Conditions")]
    private bool canBuyJulesLvl;
    private bool canBuyLaundryLvl;

    [Header("Money and Price")]
    private int playerLvlPrice = 40;
    private int louisLvlPrice = 20;
    private int julesLvlPrice = 30;
    private int ovenLvlPrice = 15;
    private int laundryLvlPrice = 30;
    private int machinePrice = 50;
    private int setPrice = 40;
    private int coffeePrice = 20;
    private int sCupPrice = 15;
    private int rCupPrice = 15;

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
    [SerializeField]
    private Button _julesButton;

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
        audioManager = FindObjectOfType<AudioManager>();

        actualQuantityCoffee = quantityCoffee;
    }

    void Update()
    {
        Debug.Log(GameManager.Instance.julesIsAlreadyActivated);

        if (coffeeButtonActivated)
        {
            startTimerCoffee = true;
            _itemPrice.text = "" + (endCooldownCoffee - cooldownCoffee);
        }
        if (startTimerCoffee)
        {
            CooldownCoffee();
        }

        if (sCupButtonActivated)
        {
            startTimerSCup = true;
            _itemPrice.text = "" + (endCooldownSCup - cooldownSCup);

        }
        if (startTimerSCup)
        {
            CooldownSCup();
        }

        if (rCupButtonActivated)
        {
            startTimerRCup = true;
            _itemPrice.text = "" + (endCooldownRCup - cooldownRCup);
        }
        if (startTimerRCup)
        {
            CooldownRCup();
        }
    }

    // Cooldowns 
    private void CooldownCoffee()
    {
        endCooldownCoffee = _coffeeCooldownReference.GetComponent<ItemBehaviour>().EndTimer;
        float cooldownFloat = _coffeeCooldownReference.GetComponent<ItemBehaviour>().Timer;
        cooldownCoffee = (int)cooldownFloat;

        if (cooldownCoffee >= endCooldownCoffee)
        {
            coffeeButtonActivated = false;
            coffeeCooldownActivated = false;
            _buyButton.interactable = true;

            if (actualQuantityCoffee == 0)
            {
                actualQuantityCoffee = quantityCoffee;
            }

            coffeePrice += itemPrice;
            DisplayInformations(_restartCoffeeItem);
            startTimerCoffee = false;
        }
    }
    private void CooldownSCup()
    {
        endCooldownSCup = _sCupCooldownReference.GetComponent<ItemBehaviour>().EndTimer;
        float cooldownFloat = _sCupCooldownReference.GetComponent<ItemBehaviour>().Timer;
        cooldownSCup = (int)cooldownFloat;

        if (cooldownSCup >= endCooldownSCup)
        {
            sCupButtonActivated = false;
            sCupCooldownActivated = false;
            _buyButton.interactable = true;

            sCupPrice += itemPrice;
            DisplayInformations(_restartSCupItem);
            startTimerSCup = false;
        }
    }
    private void CooldownRCup()
    {
        endCooldownRCup = _rCupCooldownReference.GetComponent<ItemBehaviour>().EndTimer;
        float cooldownFloat = _rCupCooldownReference.GetComponent<ItemBehaviour>().Timer;
        cooldownRCup = (int)cooldownFloat;

        if (cooldownRCup >= endCooldownRCup)
        {
            rCupButtonActivated = false;
            rCupCooldownActivated = false;
            _buyButton.interactable = true;

            rCupPrice += itemPrice;
            DisplayInformations(_restartRCupItem);
            startTimerRCup = false;
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

        itemName = itemScriptable.ItemName;
        itemPrice = itemScriptable.ItemPrice;
        itemType = itemScriptable.ItemType;
        itemIndex = itemScriptable.ItemIndex;
        _itemDescription.text = itemScriptable.ItemDescription;
        originalItemDescription = _itemDescription.text;

        _itemName.text = itemName;
        originalItemName = _itemName.text;
        _itemImage.sprite = itemScriptable.ItemImage;
        _itemImage.SetNativeSize();

        Sorting();
    }

    public void Sorting()
    {
        if (itemName == "Tristan")
        {
            _itemPrice.text = "";
            _playerMoney.text = "" + GameManager.Instance.GoldAmount;
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
        _playerMoney.text = "" + GameManager.Instance.GoldAmount;

        if (indexMachine == _machines.Count)
        {
            _itemDescription.text = originalItemDescription;

            if (GameManager.Instance.GoldAmount > machinePrice)
            {
                canBuyLaundryLvl = true;
                _buttonDisplay.sprite = _buttonGreen;
            }
            else
            {
                canBuyLaundryLvl = false;
                _buttonDisplay.sprite = _buttonRed;
            }
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
        _playerMoney.text = "" + GameManager.Instance.GoldAmount;

        if (indexMachineJules < _machinesManagers.Count-1)
        {
            _itemName.text = originalItemName + " v1";
            _itemDescription.text = originalItemDescription + "\r\n<b>Clique sur toutes les Machines</b>\r\n<color=#98E5FF>Nécessite 1 machine en plus</color>";

            if (indexMachine > GameManager.Instance.JulesLvl)
            {
                {
                    _itemDescription.text = originalItemDescription + "\r\n<b>Clique sur toutes les Machines</b>";

                    if (GameManager.Instance.GoldAmount > julesLvlPrice)
                    {
                        canBuyJulesLvl = true;
                        _buttonDisplay.sprite = _buttonGreen;
                    }
                    else
                    {
                        canBuyJulesLvl = false;
                        _buttonDisplay.sprite = _buttonRed;
                    }
                }
            }
            else
            {
                canBuyJulesLvl = false;
                _buttonDisplay.sprite = _buttonBlue;
            }
        }

        if (indexMachineJules >= _machinesManagers.Count-1)
        {
            GameManager.Instance.JulesV2 = true;
            canBuyJulesLvl = true;
            _itemName.text = originalItemName + " v2";
            _itemDescription.text = originalItemDescription + "\n<b>Diminue son temps de clic de 0,2 ms</b>\r\n<i>S'applique à toutes les machines</i>";

            PriceBehaviour(julesLvlPrice);
        }
    }
    
    // Item Button Behaviours
    public void ButtonBehaviour()
    {
        if (itemType == Type.BWorker)
        {
            if (Workers[itemIndex].activeSelf)
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

        if (itemType == Type.BMachine)
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

        if (itemType == Type.BSet)
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
                PriceBehaviour(setPrice);
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
                    PriceBehaviour(sCupPrice);
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
                    PriceBehaviour(rCupPrice);
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
                PriceBehaviour(coffeePrice);
            }
        }
    }
    public void PriceBehaviour(int itemPrice)
    {
        _itemPrice.text = "" + itemPrice;
        _playerMoney.text = "" + GameManager.Instance.GoldAmount;

        if (GameManager.Instance.GoldAmount >= itemPrice)
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
        if (_buyButton.image.sprite != _buttonBlue)
        {
            if (_buyButton.image.sprite != _buttonRed)
            {
                audioManager.PlaySound(_sfxSound);
            }
        }

        if (itemType == Type.BWorker)
        {
            BuyWorkers();
        }

        if (itemType == Type.BSet || itemType == Type.BMachine || itemType == Type.BCoffee || itemType == Type.BCups)
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
        if (GameManager.Instance.GoldAmount >= itemPrice)
        {
            if (itemIndex >= 0 && itemIndex < Workers.Count)
            {

                GameManager.Instance.GoldAmount -= itemPrice;
                _itemPrice.text = "";
                _playerMoney.text = "" + GameManager.Instance.GoldAmount;

                Workers[itemIndex].SetActive(true);
                _buyButton.interactable = false;
                _buttonDisplay.sprite = _buttonGreen;
            }
        }
    }

    //002 : Buy an Object
    public void BuyObjects()
    {
        _playerMoney.text = "" + GameManager.Instance.GoldAmount;

        if (itemType == Type.BSet)
        {
            if (GameManager.Instance.GoldAmount >= setPrice)
            {
                GameManager.Instance.GoldAmount -= setPrice;
                setPrice += itemPrice;

                if (indexSet >= 0 && indexSet < _set.Count)
                {
                    _set[indexSet].SetActive(true);
                    indexSet++;
                    quantitySet--;
                    PriceBehaviour(setPrice);
                }
                if (indexSet == _set.Count)
                {
                    _buyButton.interactable = false;
                    _buttonDisplay.sprite = _buttonGreen;
                    _itemPrice.text = "";
                }

                _itemDescription.text = originalItemDescription + "\r\n<color=#98E5FF>Quantité : " + quantitySet + "</color>";
            }
        }



        else if (itemType == Type.BMachine)
        {
            if (GameManager.Instance.GoldAmount >= machinePrice)
            {
                GameManager.Instance.GoldAmount -= machinePrice;

                if (indexMachine < _machines.Count)
                {
                    _machines[indexMachine].SetActive(true);
                    _machinesManagers[indexMachine].SetActive(true);

                    if (indexMachine == 2 || indexMachine == 4)
                    {
                        _machinesShadows[indexMachineShadow].SetActive(true);

                        indexMachineShadow++;
                    }

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

        }
        else if (itemType == Type.BCoffee)
        {
            if (GameManager.Instance.GoldAmount >= coffeePrice)
            {
                GameManager.Instance.GoldAmount -= coffeePrice;

                if (indexCoffeeItems < _coffee.Count)
                {
                    _coffee[indexCoffeeItems].SetActive(true);
                    indexCoffeeItems++;
                    actualQuantityCoffee--;
                    PriceBehaviour(coffeePrice);
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
        }
        else if (itemType == Type.BCups)
        {

            _playerMoney.text = "" + GameManager.Instance.GoldAmount;

            if (itemIndex == 0)
            {
                if (GameManager.Instance.GoldAmount >= sCupPrice)
                {
                    _cups[itemIndex].SetActive(true);
                    _buyButton.interactable = false;
                    _buttonDisplay.sprite = _buttonBlue;
                    _itemPrice.text = "";

                    GameManager.Instance.GoldAmount -= sCupPrice;
                    sCupCooldownActivated = true;
                }
            }
            else
            {
                if (GameManager.Instance.GoldAmount >= rCupPrice)
                {
                    _cups[itemIndex].SetActive(true);
                    _buyButton.interactable = false;
                    _buttonDisplay.sprite = _buttonBlue;
                    _itemPrice.text = "";

                    GameManager.Instance.GoldAmount -= rCupPrice;
                    rCupCooldownActivated = true;
                }

            }

            ButtonBehaviour();
        }
    }
    
    //003 : Level up a Worker
    public void LevelUpWorker()
    {
        if (itemIndex == 0)
        {
            if (GameManager.Instance.GoldAmount >= playerLvlPrice)
            {
                GameManager.Instance.PlayerLvl += 1;
                GameManager.Instance.PlayerCompetence += 1;

                GameManager.Instance.GoldAmount -= playerLvlPrice;
                float itemOriginalPrice = itemPrice + itemPrice * GameManager.Instance.PlayerLvl;
                playerLvlPrice = (int)itemOriginalPrice;

                PriceBehaviour(playerLvlPrice);
            }
        }
        if (itemIndex == 1)
        {
            if (GameManager.Instance.GoldAmount >= louisLvlPrice)
            {
                GameManager.Instance.LouisCompetence -= 0.2f;

                GameManager.Instance.GoldAmount -= louisLvlPrice;
                float itemOriginalPrice = itemPrice + itemPrice * GameManager.Instance.LouisLvl;
                louisLvlPrice = (int)itemOriginalPrice;
                GameManager.Instance.LouisLvl++;

                PriceBehaviour(louisLvlPrice);

                if (GameManager.Instance.LouisCompetence <= 0.2f)
                {
                    GameManager.Instance.LouisCompetence = 0.2f;
                    _buyButton.interactable = false;
                    _buttonDisplay.sprite = _buttonBlue;
                }
            }
        }
        if (itemIndex == 2)
        {
            if (canBuyJulesLvl)
            {
                if (GameManager.Instance.GoldAmount >= julesLvlPrice)
                {
                    GameManager.Instance.GoldAmount -= julesLvlPrice;

                    float itemOriginalPrice = itemPrice + itemPrice * GameManager.Instance.JulesLvl;
                    julesLvlPrice = (int)itemOriginalPrice;
                    GameManager.Instance.JulesLvl++;
                    int localIndex = 1;
                    foreach (GameObject go in _machinesManagers)
                    {
                        if (localIndex++ <= GameManager.Instance.JulesLvl)
                        {
                            ClickableObject clicker = go.GetComponent<ClickableObject>();
                            clicker.CanUseWorker = true; ;
                            clicker.Worker(GameManager.Instance.julesIsAlreadyActivated);
                        }
                    }
                    
                    if (indexMachine == GameManager.Instance.JulesLvl && indexMachineJules != 6)
                    {
                        GameManager.Instance.JulesCompetence += 0.5f;
                        indexMachineJules++;

                        //if (GameManager.Instance.julesIsAlreadyActivated)
                        //{
                        //    _julesButton.GetComponent<Button>().onClick.Invoke();
                        //    //_julesButton.GetComponent<Button>().onClick.Invoke();
                        //}

                    }
                    else if (indexMachineJules <= GameManager.Instance.JulesLvl)
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
        _playerMoney.text = "" + GameManager.Instance.GoldAmount;

        if (itemIndex == 0)
        {
            if (GameManager.Instance.GoldAmount >= ovenLvlPrice)
            {
                GameManager.Instance.GoldAmount -= ovenLvlPrice;
                float itemOriginalPrice = itemPrice + itemPrice * GameManager.Instance.OvenLvl;
                ovenLvlPrice = (int)itemOriginalPrice;

                GameManager.Instance.OvenCompetence += 1;

                _itemPrice.text = "" + ovenLvlPrice;
                PriceBehaviour(ovenLvlPrice);
                GameManager.Instance.OvenLvl++;
            }

        }

        else if (itemIndex == 1)
        {
            if (!canBuyLaundryLvl)
            {
                ButtonLvlLaundry();
            }
            else
            {
                if (GameManager.Instance.GoldAmount >= laundryLvlPrice)
                {
                    GameManager.Instance.GoldAmount -= laundryLvlPrice;

                    float itemOriginalPrice = itemPrice + itemPrice * GameManager.Instance.LaundryLvl;
                    laundryLvlPrice = (int)itemOriginalPrice;
                    GameManager.Instance.LaundryCompetence += 1;

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
                        indexSpriteMachine = 0;
                        while (indexSpriteMachine != _machines.Count)
                        {
                            if (indexSpriteMachine % 2 == 0)
                            {
                                _machines[indexSpriteMachine].GetComponent<Image>().sprite = _sprites[4];

                                SpriteState spriteState = new SpriteState
                                {
                                    highlightedSprite = _sprites[4],
                                    pressedSprite = _sprites[5]
                                };

                                _machines[indexSpriteMachine].GetComponent<Button>().spriteState = spriteState;

                                indexSpriteMachine += 1;
                            }
                            else
                            {
                                _machines[indexSpriteMachine].GetComponent<Image>().sprite = _sprites[6];

                                SpriteState spriteState = new SpriteState
                                {
                                    highlightedSprite = _sprites[6],
                                    pressedSprite = _sprites[7]
                                };

                                _machines[indexSpriteMachine].GetComponent<Button>().spriteState = spriteState;

                                indexSpriteMachine += 1;
                            }
                        }
                    }

                    else if (GameManager.Instance.LaundryLvl == 20)
                    {
                        indexSpriteMachine = 0;
                        while (indexSpriteMachine != _machines.Count)
                        {
                            _itemImage.SetNativeSize();
                            if (indexSpriteMachine % 2 == 0)
                            {
                                _machines[indexSpriteMachine].GetComponent<Image>().sprite = _sprites[8];

                                if (indexMachineShadow == 2)
                                {
                                    _machinesShadows[indexMachineShadow].GetComponent<Image>().sprite = _spritesShadows[2];
                                }
                                if (indexMachineShadow == 1)
                                {
                                    _machinesShadows[indexMachineShadow].GetComponent<Image>().sprite = _spritesShadows[1];
                                    indexMachineShadow++;
                                }
                                if (indexMachineShadow == 0)
                                {
                                    _machinesShadows[indexMachineShadow].GetComponent<Image>().sprite = _spritesShadows[0];
                                    indexMachineShadow++;
                                }


                                SpriteState spriteState = new SpriteState
                                {
                                    highlightedSprite = _sprites[8],
                                    pressedSprite = _sprites[9]
                                };

                                _machines[indexSpriteMachine].GetComponent<Button>().spriteState = spriteState;

                                indexSpriteMachine += 1;
                            }
                            else
                            {
                                _machines[indexSpriteMachine].GetComponent<Image>().sprite = _sprites[10];

                                SpriteState spriteState = new SpriteState
                                {
                                    highlightedSprite = _sprites[10],
                                    pressedSprite = _sprites[11]
                                };

                                _machines[indexSpriteMachine].GetComponent<Button>().spriteState = spriteState;

                                indexSpriteMachine += 1;
                            }
                        }
                    }

                    else if (GameManager.Instance.LaundryLvl >= 30)
                    {
                        indexSpriteMachine = 0;
                        while (indexSpriteMachine != _machines.Count)
                        {
                            if (indexSpriteMachine % 2 == 0)
                            {
                                _machines[indexSpriteMachine].GetComponent<Image>().sprite = _sprites[12];

                                SpriteState spriteState = new SpriteState
                                {
                                    highlightedSprite = _sprites[12],
                                    pressedSprite = _sprites[13]
                                };

                                _machines[indexSpriteMachine].GetComponent<Button>().spriteState = spriteState;

                                indexSpriteMachine += 1;
                            }
                            else
                            {
                                _machines[indexSpriteMachine].GetComponent<Image>().sprite = _sprites[14];

                                SpriteState spriteState = new SpriteState
                                {
                                    highlightedSprite = _sprites[14],
                                    pressedSprite = _sprites[15]
                                };

                                _machines[indexSpriteMachine].GetComponent<Button>().spriteState = spriteState;

                                indexSpriteMachine += 1;
                            }
                        }
                    }
                }

            }
        }
    }
}
