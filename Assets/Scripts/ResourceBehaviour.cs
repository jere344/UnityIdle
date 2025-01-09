using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceBehaviour : MonoBehaviour
{
    [Header("References")]
    public Image ImageReference;
    public TextMeshProUGUI TexteReference;

    [Header("Object Informations")]
    private GameObject objectToDeactivate;
    private bool haveAnObject;

    [Header("Gold")]
    public int GoldAmount;
    private int _extraGoldAmount;

    public void GainGold()
    {
        GameManager.Instance.DisplayMoney.GainGold(GoldAmount);
        if (haveAnObject)
        {
            objectToDeactivate.SetActive(false);
        }
        Destroy(gameObject);
    }

    public void GainExtraGold()
    {
        _extraGoldAmount = GameManager.Instance.DisplayResource.ExtraGold;
        GameManager.Instance.DisplayMoney.GainGold(_extraGoldAmount);
        GameManager.Instance.DisplayResource.ExtraGold = 0;
        objectToDeactivate.SetActive(false);
        gameObject.SetActive(false);
    }

    public void SetObjectToDeactivate(GameObject obj)
    {
        haveAnObject = true;
        objectToDeactivate = obj;
    }

}
