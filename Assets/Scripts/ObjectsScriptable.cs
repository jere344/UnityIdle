using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "new_object", menuName = "Shop/Object", order = 0)]
public class ObjectsScriptable : ScriptableObject
{
    public Image objectImageInShop;
    public string objectName;
    public string objectDescription;
    public float objectPrice;

}
