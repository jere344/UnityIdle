using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JulesBehaviour : MonoBehaviour
{

    [Header("List")]
    [SerializeField]
    private List<GameObject> _clickersManagers;

    [Header("Conditions")]
    private bool canUseClicker001;
    private bool canUseClicker002;
    private bool canUseClicker003;
    private bool canUseClicker004;
    private bool canUseClicker005;
    private bool canUseClicker006;

    [Header("Sprite logo")]
    [SerializeField]
    private Sprite _checkmark;
    [SerializeField]
    private Sprite _cross;
    [SerializeField]
    private Image _imageAutoclickerLogo;

    void Update()
    {
        if (GameManager.Instance.julesIsAlreadyActivated)
        {
            _imageAutoclickerLogo.sprite = _checkmark;
        }
        else
        {
            _imageAutoclickerLogo.sprite = _cross;
        }
    }
    public void AutoClicker()
    {
        GameManager.Instance.julesIsAlreadyActivated = !GameManager.Instance.julesIsAlreadyActivated;

        foreach (GameObject go in _clickersManagers)
        {
            if (go.activeSelf)
            {
                ClickableObject clicker = go.GetComponent<ClickableObject>();
                clicker.Worker(GameManager.Instance.julesIsAlreadyActivated);
            }
        }

        //if (_clickersManagers[0].activeSelf && _clickersManagers[0].GetComponent<ClickableObject>().CanUseWorker)
        //{
        //    ClickableObject clicker = _clickersManagers[0].GetComponent<ClickableObject>();
        //    clicker.Worker(GameManager.Instance.julesIsAlreadyActivated);
        //}

        //if (_clickersManagers[1].activeSelf && _clickersManagers[1].GetComponent<ClickableObject>().CanUseWorker)
        //{
        //    ClickableObject clicker = _clickersManagers[1].GetComponent<ClickableObject>();
        //    clicker.Worker(GameManager.Instance.julesIsAlreadyActivated);
        //}

        //if (_clickersManagers[2].activeSelf && _clickersManagers[2].GetComponent<ClickableObject>().CanUseWorker)
        //{
        //    ClickableObject clicker = _clickersManagers[2].GetComponent<ClickableObject>();
        //    clicker.Worker(GameManager.Instance.julesIsAlreadyActivated);
        //}

        //if (_clickersManagers[3].activeSelf && _clickersManagers[3].GetComponent<ClickableObject>().CanUseWorker)
        //{
        //    ClickableObject clicker = _clickersManagers[3].GetComponent<ClickableObject>();
        //    clicker.Worker(GameManager.Instance.julesIsAlreadyActivated);
        //}

        //if (_clickersManagers[4].activeSelf && _clickersManagers[4].GetComponent<ClickableObject>().CanUseWorker)
        //{
        //    ClickableObject clicker = _clickersManagers[4].GetComponent<ClickableObject>();
        //    clicker.Worker(GameManager.Instance.julesIsAlreadyActivated);
        //}

        //if (_clickersManagers[5].activeSelf && _clickersManagers[5].GetComponent<ClickableObject>().CanUseWorker)
        //{
        //    ClickableObject clicker = _clickersManagers[5].GetComponent<ClickableObject>();
        //    clicker.Worker(GameManager.Instance.julesIsAlreadyActivated);
        //}
    }
}
