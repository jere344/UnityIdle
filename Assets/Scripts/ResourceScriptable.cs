using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new_resource", menuName = "Clickers/Resource", order = 0)]
public class ResourceScriptable : ScriptableObject
{
    public Sprite resourceImage;
    public string resourceName;
    public float resourcePrice;
    public float resourceClick;
    public ResourceTimeClick timeClick;

    public float multiplicatorValue = 1;
    public void TimeClickValue()
    {
        switch (timeClick)
        {
            case ResourceTimeClick.Short:
                resourcePrice = 5 * multiplicatorValue;
                resourceClick = 10 * multiplicatorValue;
                break;
            case ResourceTimeClick.Medium:
                resourcePrice = 10 * multiplicatorValue;
                resourceClick = 20 * multiplicatorValue;
                break;
            case ResourceTimeClick.Long:
                resourcePrice = 20 * multiplicatorValue;
                resourceClick = 30 * multiplicatorValue;
                break;
        }
    }

    private void OnEnable()
    {
        TimeClickValue();
    }
}
