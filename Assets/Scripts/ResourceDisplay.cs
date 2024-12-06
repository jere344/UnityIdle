using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceDisplay : MonoBehaviour
{
    public ClickableObject clickableObject;

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
        //if (Input.GetKeyUp(KeyCode.Space))
        //{
        //    string resourceTag = clickableObject.actualResource.ResourceTag;
        //    GameObject resourceImage = GameObject.FindWithTag(resourceTag);
        //    resourceImage.Image.SetActive(true);
        //}
    }

    public void DisplayResource()
    {
        resourcePrice = clickableObject.ResourcePrice;

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
