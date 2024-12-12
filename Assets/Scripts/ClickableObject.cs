using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickableObject : MonoBehaviour
{
    private UpgradeManager upgradeManager;
    private ResourceGestion resourceGestion;
    private ResourceDisplay resourceDisplay;
    private GoalDisplay goalDisplay;
    public ResourceScriptable actualResource;

    private float resourceMoneyBase;
    private float resourceClickBase;
    public int ResourceMoney;
    public bool ResourceIsFood;
    public bool ResourceIsLaundry;
    public bool ResourceIsDryer;
    private string resourceName;

    private bool playerIsClicking;

    [SerializeField]
    private TextMeshProUGUI _barAmountText;
    [SerializeField]
    private Image _barAmountImage;

    public int playerCompetence;

    [SerializeField]
    private float _maxFillAmount;
    [SerializeField]
    private float _fillAmount = 0;

    private float workerCompetence;
    private bool isWorkerActive;
    private bool workerResource;
    private Coroutine workerCoroutine;

    void Start()
    {
        resourceDisplay = FindObjectOfType<ResourceDisplay>();
        resourceGestion = FindObjectOfType<ResourceGestion>();
        goalDisplay = FindObjectOfType<GoalDisplay>();
        upgradeManager = FindObjectOfType<UpgradeManager>();

        ChangeResource();
    }

    void Update()
    {
        _barAmountImage.fillAmount = _fillAmount / _maxFillAmount;
        _barAmountImage.color = Color.Lerp(Color.red, Color.green, _fillAmount / _maxFillAmount);


        if (_fillAmount >= _maxFillAmount)
        {
            _barAmountText.text = resourceName + "\n" + _fillAmount + "/" + _maxFillAmount;
        }

        if (_fillAmount >= _maxFillAmount)
        {
            StartCoroutine(restartClicker());
        }
    }

    

    public void Clicker(int competence)
    {
        _fillAmount += competence;
        _barAmountText.text = resourceName + "\n" + _fillAmount + "/" + _maxFillAmount;


    }

    public void PlayerClicker()
    {

            playerCompetence = upgradeManager.PlayerCompetence;
            _fillAmount += playerCompetence
    }

    private IEnumerator restartClicker()
    {
        _barAmountText.text = "Terminé !";
        resourceDisplay.DisplayResource();
        yield return new WaitForSeconds(0.5f);

        ResetStats();
    }


    //########################## Auto-Clicker Behaviour ##########################//

    public void Worker()
    {
        isWorkerActive = !isWorkerActive;
        if (isWorkerActive)
        {

        }
    }

    private IEnumerator workerRoutine()
    {
        while (true)
        {
            if (_fillAmount < _maxFillAmount)
            {
                Clicker(1);
                workerCompetence = upgradeManager.WorkerCompetence;
                yield return new WaitForSeconds(workerCompetence);

                if (_fillAmount >= _maxFillAmount)
                {
                    workerResource = true;
                    _barAmountText.text = "Terminé !";
                    resourceDisplay.DisplayResource();
                    yield return new WaitForSeconds(0.2f);
                    ResetStats();
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

        resourceName = actualResource.ResourceName;
        resourceClickBase = actualResource.GetResourceClick() * (Mathf.Pow(1.5f, (goalDisplay.currentIndex)));
        resourceMoneyBase = actualResource.GetResourceMoney() * (Mathf.Pow(1.5f, (goalDisplay.currentIndex)));
        _maxFillAmount = (int) resourceClickBase;
        ResourceMoney = (int) resourceMoneyBase;
    }

    public void ResetStats()
    {
        ChangeResource();
        _fillAmount = 0;
        _barAmountText.text = "";
    }

}

