using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Clickable object behavior
// When clicked, increases a bar
// Gives a resource when the bar reaches its maximum

// The player can improve the value of his click 
// Worker can improve his own speed

public class ClickableObject : MonoBehaviour
{
    private ResourceDisplay resourceDisplay;

    [SerializeField]
    private TextMeshProUGUI _barAmountText;
    [SerializeField]
    private Image _barAmountImage;

    [SerializeField]
    private string _objectText;

    [SerializeField]
    private float _playerCompetence;
    [SerializeField]
    private float _maxFillAmount;
    private float minFillAmount = 0;
    private float _fillAmount;
    private float remainingAmount;

    [SerializeField]
    private float _workerCompetence;
    private bool isWorkerActive;
    private bool workerResource;
    private Coroutine workerCoroutine;


    void Start()
    {
        resourceDisplay = FindObjectOfType<ResourceDisplay>();

        _fillAmount = minFillAmount;
        remainingAmount = _fillAmount;
    }

    void Update()
    {
        _barAmountImage.fillAmount = _fillAmount / _maxFillAmount;
        _barAmountImage.color = Color.Lerp(Color.red, Color.green, _fillAmount / _maxFillAmount);
    }

    public void Clicker()
    {
        if (_fillAmount < _maxFillAmount)
        {
            _fillAmount += _playerCompetence;
            remainingAmount = _maxFillAmount - _fillAmount;

            if (remainingAmount == 1)
            {
                _barAmountText.text = _objectText + " dans : " + remainingAmount.ToString("") + " clic";
            }
            else
            {
                _barAmountText.text = _objectText + " dans : " + remainingAmount.ToString("") + " clics";
            }

            if (_fillAmount >= _maxFillAmount)
            {
                StartCoroutine(restartClicker());
            }
        }
    }

    private IEnumerator restartClicker()
    {
        _barAmountText.text = "Terminé !";

        if (!workerResource)
        {
            resourceDisplay.DisplayResource();
        }

        yield return new WaitForSeconds(0.5f);
        _fillAmount = minFillAmount;
        _barAmountText.text = "";
    }

    public void Worker()
    {
        isWorkerActive = !isWorkerActive;

        if (isWorkerActive)
        {
            if (workerCoroutine == null)
            {
                workerCoroutine = StartCoroutine(workerRoutine());
            }
        }
        else
        {
            if (workerCoroutine != null)
            {
                StopCoroutine(workerCoroutine);
                workerCoroutine = null;
            }
        }
    }

   private IEnumerator workerRoutine()
   {
        while (true)
        {
            if (_fillAmount < _maxFillAmount)
            {
                _fillAmount++;
                remainingAmount = _maxFillAmount - _fillAmount;

                if (remainingAmount == 1)
                {
                    _barAmountText.text = _objectText + " dans : " + remainingAmount.ToString("") + " clic";
                    yield return new WaitForSeconds(_workerCompetence);
                }

                if (_fillAmount >= _maxFillAmount)
                {
                    workerResource = true;
                    _barAmountText.text = "Terminé !";
                    resourceDisplay.DisplayResource();
                    yield return new WaitForSeconds(_workerCompetence);
                    _fillAmount = minFillAmount;
                    _barAmountText.text = "";
                    workerResource = false;
                }

                if (remainingAmount > 1)
                {
                    _barAmountText.text = _objectText + " dans : " + remainingAmount.ToString("") + " clics";
                    yield return new WaitForSeconds(_workerCompetence);
                }

            }
        }
   }

}
