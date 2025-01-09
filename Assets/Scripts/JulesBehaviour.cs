using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JulesBehaviour : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _clickersManagers;

    public void AutoClicker()
    {
        if (_clickersManagers[0].activeSelf && _clickersManagers[0].GetComponent<ClickableObject>().CanUseWorker)
        {
            Debug.Log("Activated!");
            ClickableObject clicker = _clickersManagers[0].GetComponent<ClickableObject>();
            clicker.Worker();
        }

        if (_clickersManagers[1].activeSelf && _clickersManagers[1].GetComponent<ClickableObject>().CanUseWorker)
        {
            ClickableObject clicker = _clickersManagers[1].GetComponent<ClickableObject>();
            clicker.Worker();
        }

        if (_clickersManagers[2].activeSelf && _clickersManagers[2].GetComponent<ClickableObject>().CanUseWorker)
        {
            ClickableObject clicker = _clickersManagers[2].GetComponent<ClickableObject>();
            clicker.Worker();
        }

        if (_clickersManagers[3].activeSelf && _clickersManagers[3].GetComponent<ClickableObject>().CanUseWorker)
        {
            ClickableObject clicker = _clickersManagers[3].GetComponent<ClickableObject>();
            clicker.Worker();
        }

        if (_clickersManagers[4].activeSelf && _clickersManagers[4].GetComponent<ClickableObject>().CanUseWorker)
        {
            ClickableObject clicker = _clickersManagers[4].GetComponent<ClickableObject>();
            clicker.Worker();
        }

        if (_clickersManagers[5].activeSelf && _clickersManagers[5].GetComponent<ClickableObject>().CanUseWorker)
        {
            ClickableObject clicker = _clickersManagers[5].GetComponent<ClickableObject>();
            clicker.Worker();
        }
    }
}
