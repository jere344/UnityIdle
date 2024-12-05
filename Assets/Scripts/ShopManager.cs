using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public List<Image> goodiesImages;
    public List<Image> setImages;
    public List<ObjectsScriptable> allObjects;


    private MoneyDisplay moneyDisplay;

    private float timeDelay;
    private float bonusValue;

    public SpriteRenderer objectRenderer;

    void Start()
    {
        moneyDisplay = FindObjectOfType<MoneyDisplay>();
    }


    void Update()
    {

    }

    //public void BuyObject(int index)
    //{
    //    if (moneyDisplay.GoldAmount >= objects[index].objectPrice)
    //    {
    //        moneyDisplay.GoldAmount -= objects[index].objectPrice;
    //        setImages[index].gameObject.SetActive(true);
    //        setImages[index].gameObject.SetActive(true);
    //    }
    //}

    public void BuyGoodies(int index)
    {
        if (moneyDisplay.GoldAmount >= allObjects[index].objectPrice)
        {
            moneyDisplay.GoldAmount -= allObjects[index].objectPrice;
            goodiesImages[index].gameObject.SetActive(true);

            StartCoroutine(ActivateGoodies(goodiesImages[index].gameObject));

            allObjects[index].objectPrice *= 1.5f;
        }
    }

    private IEnumerator ActivateGoodies(GameObject objectActivate)
    {
        timeDelay = Random.Range(1f, 5f);

        for (float i = 0; i > timeDelay; i++)
        {
            moneyDisplay.GoldAmount += bonusValue;
            yield return new WaitForSeconds(timeDelay);
        }

        objectActivate.SetActive(false);
    }
}
