using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ResourceGestion : MonoBehaviour
{
    public List<ResourceScriptable> AllLaundryResources;
    public List<ResourceScriptable> AllFoodResources;
    public List<GameObject> AllFoodGameObjects;

    public List<GameObject> seasonFoodObject;
    public List<ResourceScriptable> seasonResource;

    private int index;
    private int count;

    public void RandomFoodResourcesList()
    {
        seasonResource = new List<ResourceScriptable>();
        seasonFoodObject = new List<GameObject>();

        List<int> availableIndices = new List<int>();
        for (index = 0; index < AllFoodResources.Count; index++)
        {
            availableIndices.Add(index);
        }

        for (count = 0; count < 4; count++)
        {
            int randomIndex = Random.Range(0, availableIndices.Count);
            int selectedIndex = availableIndices[randomIndex];

            seasonResource.Add(AllFoodResources[selectedIndex]);
            seasonFoodObject.Add(AllFoodGameObjects[selectedIndex]);

            availableIndices.RemoveAt(randomIndex);
        }
    }

    public void ChangeRandomResourcesList()
    {
        RandomFoodResourcesList();
    }
}
