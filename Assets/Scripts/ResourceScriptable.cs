using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new_resource", menuName = "Resource", order = 0)]
public class ResourceScriptable : ScriptableObject
{
    public Sprite resourceImage;
    public string resourceName;
    public float resourcePrice;
    public float resourceClick;
    public ResourceType type;
}
