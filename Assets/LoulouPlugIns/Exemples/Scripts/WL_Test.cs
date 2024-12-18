using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WL_Test : MonoBehaviour
{
    //You can use WeightedList<T> as the List<T> Class but with extra features !
    
    public WeightedList<string> weightedStrings;


    // Start is called before the first frame update
    void Start()
    {
        //Use Add(item,weight) to add a new element to the list with its weight
        weightedStrings.Add("Bannana", 2);
        weightedStrings.Add("Strawberry", 8);

        //You can use SetWeightOfObject(item,weight) to change the weight of an item in the list
        weightedStrings.SetWeightOfObject("Banana", 1);

        Debug.Log(weightedStrings.TotalWeight);//TotalWeight gives you the sum of all items' weight

        //Use GetRandomElement() to access a random element in the list. The more weight the element has, the more likely it is to appear.
        Debug.Log(weightedStrings.GetRandomElement());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
