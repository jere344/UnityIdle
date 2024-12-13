using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickableObject : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _barAmountText;
    [SerializeField]
    private Image _barAmountImage;

    private ResourceScriptable scriptableResource;

    public bool ResourceIsFood, ResourceIsLaundry, ResourceIsDryer;
    public int ResourceMoney;
    private string resourceName;

    private bool playerIsClicking, playerResourceActivated;
    private int playerCompetence;

    private float maxFillAmount;
    private float fillAmount = 0;

    private float workerCompetence;
    private bool workerIsClicking, workerResourceActivated;
    private Coroutine workerCoroutine;

    void Start()
    {
        ChangeResource();
    }

    void Update()
    {
        _barAmountImage.fillAmount = fillAmount / maxFillAmount;
        _barAmountImage.color = Color.Lerp(Color.red, Color.green, fillAmount / maxFillAmount);
    }

    public void Clicker(int competence)
    {
        fillAmount += competence;
        _barAmountText.text = resourceName + "\n" + fillAmount + "/" + maxFillAmount;
    }

    public void PlayerClicker()
    {
        if (fillAmount < maxFillAmount)
        {
            playerCompetence = GameManager.Instance.GestionShop.PlayerCompetence;
            Clicker(playerCompetence);

            if (fillAmount >= maxFillAmount && !workerResourceActivated)
            {
                StartCoroutine(restartClicker());
            }
        }
    }

    private IEnumerator restartClicker()
    {
        playerResourceActivated = true;
        _barAmountText.text = "Terminé !";
        GameManager.Instance.DisplayResource.DisplayResource(ResourceMoney);
        yield return new WaitForSeconds(0.5f);
        ChangeResource();
        fillAmount = 0;
        _barAmountText.text = "";
        playerResourceActivated = false;
    }

    //########################## Auto-Clicker Behaviour ##########################//

    public void Worker()
    {
        workerIsClicking = !workerIsClicking;

        if (workerIsClicking)
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
            if (fillAmount < maxFillAmount)
            {
                Clicker(1);
                workerCompetence = GameManager.Instance.GestionShop.WorkerCompetence;
                yield return new WaitForSeconds(workerCompetence);

                if (fillAmount >= maxFillAmount && !playerResourceActivated)
                {
                    playerResourceActivated = true;
                    _barAmountText.text = "Terminé !";
                    GameManager.Instance.DisplayResource.DisplayResource(ResourceMoney);
                    yield return new WaitForSeconds(0.5f);
                    ChangeResource();
                    fillAmount = 0;
                    _barAmountText.text = "";
                    playerResourceActivated = false;
                }
            }
        }
    }

    //########################## Reset Stats ##########################//

    public void ResetStats()
    {
        ChangeResource();
        fillAmount = 0;
        _barAmountText.text = "";
    }

    //########################## Change resource ##########################//

    private void ChangeResource()
    {
        if (ResourceIsFood)
        {
            scriptableResource = GameManager.Instance.gestionResource.seasonResource[Random.Range(0, GameManager.Instance.gestionResource.seasonResource.Count)];
        }

        if (ResourceIsLaundry)
        {
            scriptableResource = GameManager.Instance.gestionResource.AllLaundryResources[Random.Range(0, GameManager.Instance.gestionResource.AllLaundryResources.Count)];
        }

        if (ResourceIsDryer)
        {
            scriptableResource = GameManager.Instance.gestionResource.AllDryerResources[Random.Range(0, GameManager.Instance.gestionResource.AllDryerResources.Count)];
        }

        resourceName = scriptableResource.ResourceName;
    
        float resourceClickBase = scriptableResource.GetResourceClick() * (Mathf.Pow(1.5f, (GameManager.Instance.DisplayGoal.currentIndex)));
        float resourceMoneyBase = scriptableResource.GetResourceMoney() * (Mathf.Pow(1.5f, (GameManager.Instance.DisplayGoal.currentIndex)));
        maxFillAmount = (int) resourceClickBase;
        ResourceMoney = (int) resourceMoneyBase;
    }

}

