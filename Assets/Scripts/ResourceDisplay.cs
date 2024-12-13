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
    private float maxResources = 8;

    [SerializeField]
    private TextMeshProUGUI _extraMoneyText;
    [SerializeField]
    private GameObject _extraMoneyGO;
    public int ExtraMoney;

    private GameObject _newResource;

    void Start()
    {
        ExtraMoney = 0;
    }

    void Update()
    {

    }

    public void DisplayResource(int ResourceMoney)
    {
        resourcePrice = ResourceMoney;

        if (_resourceContainer.childCount >= maxResources)
        {
            _extraMoneyGO.SetActive(true);
            ExtraMoney += resourcePrice;
            _extraMoneyText.text = "" + ExtraMoney;
        }
        else
        {
            _newResource = Instantiate(_resourcePrefab, _resourceContainer);
            TextMeshProUGUI resourceText = _newResource.GetComponentInChildren<TextMeshProUGUI>();

            _newResource.GetComponent<ResourceBehaviour>().MoneyAmount = resourcePrice;
            resourceText.text = "" + resourcePrice;
        }
    }
}
