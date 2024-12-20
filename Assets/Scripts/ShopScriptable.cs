using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "new_resource", menuName = "Shop/Item", order = 0)]

public class ShopScriptable : ScriptableObject
{
    public Sprite itemImage;
    public string itemName;
    public string itemDescription;
    public int itemPrice;
}
