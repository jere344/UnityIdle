using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JulesBehaviour : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _clickersManagers;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AutoClicker()
    {
        if (_clickersManagers[0].activeSelf && _clickersManagers[0].GetComponent<ClickableObject>().CanUseWorker)
        {
            ClickableObject clicker = _clickersManagers[0].GetComponent<ClickableObject>();
            clicker.Worker();
        }

        if (_clickersManagers[1].activeSelf && _clickersManagers[1].GetComponent<ClickableObject>().CanUseWorker)
        {
            ClickableObject clicker = _clickersManagers[1].GetComponent<ClickableObject>();
            clicker.Worker();
        }
    }
}
