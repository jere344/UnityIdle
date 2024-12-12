using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ResourceGestion : MonoBehaviour
{
    public List<ResourceScriptable> AllLaundryResources;
    public List<ResourceScriptable> AllDryerResources;
    public List<ResourceScriptable> AllFoodResources;

    public List<ResourceScriptable> seasonResource;
    private List<ResourceScriptable> resources;
    private List<ResourceScriptable> copyResources;


    private int index;
    private int randomIndex;

    List<ResourceScriptable> RandomResources()
    {
        resources = new List<ResourceScriptable>();
        copyResources = new List<ResourceScriptable>(AllFoodResources);


        for (index = 0; index < 4; index++)
        {
            randomIndex = Random.Range(0, copyResources.Count);
            resources.Add(copyResources[randomIndex]);
            copyResources.RemoveAt(randomIndex);
        }

        return resources;
    }

    public void ChangeRandomResourcesList()
    {
        seasonResource = RandomResources();
    }
}
