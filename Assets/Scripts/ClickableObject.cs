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
    private TextMeshProUGUI _barText;
    [SerializeField]
    private Image _barAmountImage;

    private ResourceScriptable scriptableResource;

    public bool ResourceIsFood, ResourceIsLaundry;
    public int ResourceMoney;
    public Sprite ResourceNewImage;
    public GameObject FoodObject;
    private string resourceName;



    private bool playerIsClicking, playerResourceActivated;
    private int playerCompetence;

    private float maxFillAmount, fillAmount = 0;

    private float workerCompetence;
    private bool workerIsClicking, workerResourceActivated;
    private Coroutine workerCoroutine;

    void Start()
    {
        _barText.text = "";
        ChangeResource();
    }

    void Update()
    {
        Color pastelRed = new Color(1.0f, 0.7389936f, 0.7462438f, 1.0f);
        Color pastelGreen = new Color(0.7940765f, 1.0f, 0.4980392f, 1.0f);

        _barAmountImage.fillAmount = fillAmount / maxFillAmount;
        _barAmountImage.color = Color.Lerp(pastelRed, pastelGreen, fillAmount / maxFillAmount);
    }

    public void Clicker(int competence)
    {
        fillAmount += competence;
        _barText.text = resourceName;
        _barAmountText.text =  fillAmount + "/" + maxFillAmount;
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
        _barText.text = "Terminé !";

        if (ResourceIsFood)
        {
            GameManager.Instance.DisplayResource.DisplayResourceFood(ResourceMoney, ResourceNewImage, FoodObject);
        }
        else
        {
            GameManager.Instance.DisplayResource.DisplayResource(ResourceMoney, ResourceNewImage);
        }

        yield return new WaitForSeconds(0.5f);
        ChangeResource();
        fillAmount = 0;
        _barText.text = "";
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

                    if (ResourceIsFood)
                    {
                        GameManager.Instance.DisplayResource.DisplayResourceFood(ResourceMoney, ResourceNewImage, FoodObject);
                    }
                    else
                    {
                        GameManager.Instance.DisplayResource.DisplayResource(ResourceMoney, ResourceNewImage);
                    }

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

        resourceName = scriptableResource.ResourceName;
        ResourceNewImage = scriptableResource.ResourceImage;

        float resourceClickBase = scriptableResource.GetResourceClick() * (Mathf.Pow(1.5f, (GameManager.Instance.DisplayGoal.currentIndex)));
        float resourceMoneyBase = scriptableResource.GetResourceMoney() * (Mathf.Pow(1.5f, (GameManager.Instance.DisplayGoal.currentIndex)));
        maxFillAmount = (int) resourceClickBase;
        ResourceMoney = (int) resourceMoneyBase;
    }

}

