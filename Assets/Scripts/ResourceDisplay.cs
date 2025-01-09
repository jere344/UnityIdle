using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceDisplay : MonoBehaviour
{
    [Header("Resource informations")]
    [SerializeField]
    private GameObject _resourcePrefab;
    [SerializeField]
    private Transform _resourceContainer;
    private GameObject _newResource;
    public GameObject FoodObject;
    private int resourcePrice;
    private float maxResources = 5;

    [Header("Gold informations")]
    [SerializeField]
    private TextMeshProUGUI _extraGoldText;
    [SerializeField]
    private GameObject _extraGoldGO;
    public int ExtraGold;

    void Start()
    {
        ExtraGold = 0;
    }

    public void DisplayResource(int ResourceMoney, Sprite ResourceImage)
    {
        resourcePrice = ResourceMoney;

        if (_resourceContainer.childCount >= maxResources)
        {
            _extraGoldGO.SetActive(true);
            ExtraGold += resourcePrice;
            _extraGoldText.text = "" + ExtraGold + " Pièces";
            if (ExtraGold >= 1000)
            {
                _extraGoldText.text = "" + (ExtraGold/1000) + " Pièces";
            }
        }
        else
        {
            _newResource = Instantiate(_resourcePrefab, _resourceContainer);
            ResourceBehaviour resourceNewImage = _newResource.GetComponent<ResourceBehaviour>();
            ResourceBehaviour resourceText = _newResource.GetComponentInChildren<ResourceBehaviour>();
            _newResource.GetComponent<ResourceBehaviour>().GoldAmount = resourcePrice;

            resourceNewImage.ImageReference.sprite = ResourceImage;
            resourceText.TexteReference.text = "" + resourcePrice;
        }
    }

    public void DisplayResourceFood(int ResourceMoney, Sprite ResourceImage, GameObject SelectedFoodObject)
    {
        resourcePrice = ResourceMoney;

        if (_resourceContainer.childCount >= maxResources)
        {
            _extraGoldGO.SetActive(true);
            ExtraGold += resourcePrice;
            _extraGoldText.text = "" + ExtraGold + " Pièces";
        }
        else
        {
            FoodObject = SelectedFoodObject;
            if (!FoodObject.activeSelf)
            {
                FoodObject.SetActive(!FoodObject.activeSelf);
            }
            _newResource = Instantiate(_resourcePrefab, _resourceContainer);
            ResourceBehaviour resourceScript = _newResource.GetComponent<ResourceBehaviour>();
            ResourceBehaviour resourceText = _newResource.GetComponentInChildren<ResourceBehaviour>();

            resourceScript.SetObjectToDeactivate(FoodObject);
            _newResource.GetComponent<ResourceBehaviour>().GoldAmount = resourcePrice;

            resourceScript.ImageReference.sprite = ResourceImage;
            resourceText.TexteReference.text = "" + resourcePrice;
        }
    }
}
