using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JulesBehaviour : MonoBehaviour
{

    [Header("List")]
    [SerializeField]
    private List<GameObject> _clickersManagers;

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
    }
}
