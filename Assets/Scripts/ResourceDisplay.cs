using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Shows the resources obtained

public class ResourceDisplay : MonoBehaviour
{
    [SerializeField]
    private List<ResourceScriptable> _resources = new List<ResourceScriptable>();
    [SerializeField]
    private GameObject _resourcePrefab;
    [SerializeField]
    private Transform _resourceContainer;
    private TextMeshProUGUI resourcePriceText;
    private float resourcePrice;
    private float maxResources = 8;

    [SerializeField]
    private TextMeshProUGUI _extraMoneyText;
    [SerializeField]
    private GameObject _extraMoneyGO;
    public float ExtraMoney;



    private ResourceScriptable _randomResource;
    private GameObject _newResource;

    void Start()
    {
        ExtraMoney = 0;
    }

    void Update()
    {

    }

    public void DisplayResource()
    {
        _randomResource = _resources[Random.Range(0, _resources.Count)];
        resourcePrice = _randomResource.resourcePrice;

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
