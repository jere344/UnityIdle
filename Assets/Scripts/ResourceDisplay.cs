using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceDisplay : MonoBehaviour
{
    [SerializeField]
    private GameObject _resourcePrefab;
    [SerializeField]
    private Transform _resourceContainer;
    private int resourcePrice;
    private float maxResources = 5;

    [SerializeField]
    private TextMeshProUGUI _extraMoneyText;
    [SerializeField]
    private GameObject _extraMoneyGO;
    public int ExtraMoney;

    private GameObject _newResource;

    public GameObject FoodObject;

    void Start()
    {
        ExtraMoney = 0;
    }

    void Update()
    {

    }

    public void DisplayResource(int ResourceMoney, Sprite ResourceImage)
    {
        resourcePrice = ResourceMoney;

        if (_resourceContainer.childCount >= maxResources)
        {
            _extraMoneyGO.SetActive(true);
            ExtraMoney += resourcePrice;
            _extraMoneyText.text = "" + ExtraMoney + " Or";
        }
        else
        {
            _newResource = Instantiate(_resourcePrefab, _resourceContainer);
            ResourceBehaviour resourceNewImage = _newResource.GetComponent<ResourceBehaviour>();
            ResourceBehaviour resourceText = _newResource.GetComponentInChildren<ResourceBehaviour>();
            _newResource.GetComponent<ResourceBehaviour>().MoneyAmount = resourcePrice;

            resourceNewImage.ImageReference.sprite = ResourceImage;
            resourceText.TexteReference.text = "" + resourcePrice;
        }
    }

    public void DisplayResourceFood(int ResourceMoney, Sprite ResourceImage, GameObject SelectedFoodObject)
    {
        resourcePrice = ResourceMoney;

        if (_resourceContainer.childCount >= maxResources)
        {
            _extraMoneyGO.SetActive(true);
            ExtraMoney += resourcePrice;
            _extraMoneyText.text = "" + ExtraMoney + " Or";
        }
        else
        {
            FoodObject = SelectedFoodObject;
            FoodObject.SetActive(!FoodObject.activeSelf);
            _newResource = Instantiate(_resourcePrefab, _resourceContainer);
            ResourceBehaviour resourceNewImage = _newResource.GetComponent<ResourceBehaviour>();
            ResourceBehaviour resourceText = _newResource.GetComponentInChildren<ResourceBehaviour>();
            _newResource.GetComponent<ResourceBehaviour>().MoneyAmount = resourcePrice;

            resourceNewImage.ImageReference.sprite = ResourceImage;
            resourceText.TexteReference.text = "" + resourcePrice;
        }
    }
}
