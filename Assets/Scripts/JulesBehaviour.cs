using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JulesBehaviour : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _clickersManagers;

    private bool canUseClicker001;
    private bool canUseClicker002;
    private bool canUseClicker003;
    private bool canUseClicker004;
    private bool canUseClicker005;
    private bool canUseClicker006;

    void Update()
    {
        if (_clickersManagers[0].activeSelf && _clickersManagers[0].GetComponent<ClickableObject>().CanUseWorker)
        {
            canUseClicker001 = true;
        }

        if (_clickersManagers[1].activeSelf && _clickersManagers[1].GetComponent<ClickableObject>().CanUseWorker)
        {
            canUseClicker002 = true;
        }

        if (_clickersManagers[2].activeSelf && _clickersManagers[2].GetComponent<ClickableObject>().CanUseWorker)
        {
            canUseClicker003 = true;
        }

        if (_clickersManagers[3].activeSelf && _clickersManagers[3].GetComponent<ClickableObject>().CanUseWorker)
        {
            canUseClicker004 = true;
        }

        if (_clickersManagers[4].activeSelf && _clickersManagers[4].GetComponent<ClickableObject>().CanUseWorker)
        {
            canUseClicker005 = true;
        }

        if (_clickersManagers[5].activeSelf && _clickersManagers[5].GetComponent<ClickableObject>().CanUseWorker)
        {
            canUseClicker006 = true;
        }
    }
    public void AutoClicker()
    {
        if (canUseClicker001)
        {
            ClickableObject clicker = _clickersManagers[0].GetComponent<ClickableObject>();
            clicker.Worker();
        }

        if (canUseClicker002)
        {
            ClickableObject clicker = _clickersManagers[1].GetComponent<ClickableObject>();
            clicker.Worker();
        }

        if (canUseClicker003)
        {
            ClickableObject clicker = _clickersManagers[2].GetComponent<ClickableObject>();
            clicker.Worker();
        }

        if (canUseClicker004)
        {
            ClickableObject clicker = _clickersManagers[3].GetComponent<ClickableObject>();
            clicker.Worker();
        }

        if (canUseClicker005)
        {
            ClickableObject clicker = _clickersManagers[4].GetComponent<ClickableObject>();
            clicker.Worker();
        }

        if (canUseClicker006)
        {
            ClickableObject clicker = _clickersManagers[5].GetComponent<ClickableObject>();
            clicker.Worker();
        }
    }
}
