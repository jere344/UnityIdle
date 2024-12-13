using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new_resource", menuName = "Clickers/Resource", order = 0)]
public class ResourceScriptable : ScriptableObject
{
    public Sprite ResourceImage;
    public string ResourceName;
    
    public ResourceTimeClick timeClick;

    public int GetResourceMoney()
    {
        var ret = 0;
        switch (timeClick)
        {
            case ResourceTimeClick.Short:
                ret = 5;
                break;
            case ResourceTimeClick.Medium:
                ret = 10;
                break;
            case ResourceTimeClick.Long:
                ret = 15;
                break;
            default:
                break;
        }

        return ret;
    }
    public int GetResourceClick()
    {

        var ret = 0;
        switch (timeClick)
        {
            case ResourceTimeClick.Short:
                ret = 10;
                break;
            case ResourceTimeClick.Medium:
                ret = 20;
                break;
            case ResourceTimeClick.Long:
                ret = 30;
                break;
            default:
                break;
        }

        return ret;
    }

}
