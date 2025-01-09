using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "new_item", menuName = "Shop/Item", order = 0)]

public class ItemScriptable : ScriptableObject
{
    public int ItemIndex;
    public Sprite ItemImage;
    public string ItemName;
    [TextArea]
    public string ItemDescription;
    public int ItemPrice;
    public Type ItemType;
}
