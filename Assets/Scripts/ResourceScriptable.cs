using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scriptable object of all game resources

[CreateAssetMenu(fileName = "new_resource", menuName = "Resource", order = 0)]
public class ResourceScriptable : ScriptableObject
{
    public Sprite resourceImage;
    public string resourceName;
    public string resourceDescription;
    public float resourcePrice;
    public ResourceType type;
}
