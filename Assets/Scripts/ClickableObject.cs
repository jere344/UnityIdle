using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickableObject : MonoBehaviour
{
    private ResourcesGestion resourceGestion;
    private ResourceDisplay resourceDisplay;
    private ResourceScriptable actualResource;

    public float ResourcePrice;
    public bool ResourceIsFood;
    public bool ResourceIsLaundry;
    public bool ResourceIsDryer;
    private string resourceName;

    [SerializeField]
    private TextMeshProUGUI _barAmountText;
    [SerializeField]
    private Image _barAmountImage;

    [SerializeField]
    private float _playerCompetence;

    [SerializeField]
    private float _maxFillAmount;
    [SerializeField]
    private float _fillAmount = 0;

    [SerializeField]
    private float _workerCompetence;
    private bool isWorkerActive;
    private bool workerResource;
    private Coroutine workerCoroutine;

    void Start()
    {
        resourceDisplay = FindObjectOfType<ResourceDisplay>();
        resourceGestion = FindObjectOfType<ResourcesGestion>();

        ChangeResource();
    }

    void Update()
    {
        _barAmountImage.fillAmount = _fillAmount / _maxFillAmount;
        _barAmountImage.color = Color.Lerp(Color.red, Color.green, _fillAmount / _maxFillAmount);
    }

    public void Clicker()
    {
        _fillAmount += _playerCompetence;
        _barAmountText.text = resourceName + "\n" + _fillAmount + "/" + _maxFillAmount;
    }

    public void PlayerClicker()
    {
        if (_fillAmount < _maxFillAmount)
        {
            Clicker();
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
        ChangeResource();
        _fillAmount = 0;
        _barAmountText.text = "";
    }


    //########################## Auto-Clicker Behaviour ##########################//

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
                Clicker();
                yield return new WaitForSeconds(_workerCompetence);

                if (_fillAmount >= _maxFillAmount)
                {
                    workerResource = true;
                    _barAmountText.text = "Terminé !";
                    resourceDisplay.DisplayResource();
                    yield return new WaitForSeconds(_workerCompetence);
                    ChangeResource();
                    _fillAmount = 0;
                    _barAmountText.text = "";
                    workerResource = false;
                }
            }
        }
    }

    private void ChangeResource()
    {
        if (ResourceIsFood)
        {
            actualResource = resourceGestion.seasonResource[Random.Range(0, resourceGestion.seasonResource.Count)];
        }

        if (ResourceIsLaundry)
        {
            actualResource = resourceGestion.AllLaundryResources[Random.Range(0, resourceGestion.AllLaundryResources.Count)];
        }

        if (ResourceIsDryer)
        {
            actualResource = resourceGestion.AllDryerResources[Random.Range(0, resourceGestion.AllDryerResources.Count)];
        }

        _maxFillAmount = actualResource.resourceClick;
        resourceName = actualResource.resourceName;
        ResourcePrice = actualResource.resourcePrice;
    }

}

