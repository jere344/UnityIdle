using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "new_item", menuName = "Shop/Item", order = 0)]

public class ItemScriptable : ScriptableObject
{
    public int itemIndex;
    public Sprite itemImage;
    public string itemName;
    [TextArea]
    public string itemDescription;
    public int itemPrice;
    public Type itemType;
    public bool HaveGameObject;
}
