using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickableObject : MonoBehaviour
{
    [Header("Audio")]
    private AudioManager audioManager;
    [SerializeField]
    private AudioClip _sfxSound;

    [Header("Bar UI")]
    [SerializeField]
    private TextMeshProUGUI _barAmountText;
    [SerializeField]
    private TextMeshProUGUI _barText;
    [SerializeField]
    private Image _barAmountImage;    
    [SerializeField]
    private Image _barBackground;
    private float maxFillAmount, fillAmount = 0;

    [Header("Activated GameObjects")]
    [SerializeField]
    private GameObject _bar;

    [Header("Resources")]
    public bool ResourceIsFood, ResourceIsLaundry;
    public int ResourceMoney;
    private ResourceScriptable scriptableResource;
    private Sprite resourceNewImage;
    private GameObject foodObject;
    private string resourceName;

    [Header("Conditions & Routine")]
    public bool CanUseWorker;
    private bool playerResourceActivated;
    private bool workerIsClicking, workerResourceActivated;
    private float workerCompetence;
    private Coroutine workerCoroutine;

    [Header ("Sprite logo")]
    [SerializeField]
    private Sprite _checkmark;
    [SerializeField]
    private Sprite _cross;
    [SerializeField]
    private Image _imageAutoclickerLogo;
    private bool isCheckmark = true;

    [Header("Timer Bar")]
    public float barTimer;
    public bool timerEnd;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();

        _bar.SetActive(true);

        _barText.text = "";
        SetBackgroundImage(0);
        ChangeResource();
    }

    void Update()
    {
        if (timerEnd == false)
        {
            _bar.SetActive(true);
            barTimer += Time.deltaTime;
        }
        if (barTimer > 3 && !workerIsClicking)
        {
            timerEnd = true;
            _bar.SetActive(false);
        }
        if (workerIsClicking)
        {
            timerEnd = true;
            _bar.SetActive(true);
        }


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
        SetBackgroundImage(1f);
    }

    public void PlayerClicker()
    {
        barTimer = 0;
        timerEnd = false;

        if (fillAmount < maxFillAmount)
        {
            int playerCompetence = GameManager.Instance.PlayerCompetence;
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
            GameManager.Instance.DisplayResource.DisplayResourceFood(ResourceMoney, resourceNewImage, foodObject);
        }
        else
        {
            GameManager.Instance.DisplayResource.DisplayResource(ResourceMoney, resourceNewImage);
        }

        audioManager.PlaySound(_sfxSound);
        yield return new WaitForSeconds(0.5f);
        ChangeResource();
        fillAmount = 0;
        _barText.text = "";
        _barAmountText.text = "";
        SetBackgroundImage(0);
        playerResourceActivated = false;
    }

    // Worker (AutoClicker)

    public void Worker()
    {
        if (CanUseWorker)
        {
            workerIsClicking = !workerIsClicking;
            _imageAutoclickerLogo.sprite = isCheckmark ? _checkmark : _cross;
            isCheckmark = !isCheckmark;

            if (ResourceIsFood)
            {
                workerCompetence = GameManager.Instance.LouisCompetence;
            }
            else if (ResourceIsLaundry)
            {
                workerCompetence = GameManager.Instance.JulesCompetence;
            }

            if (workerIsClicking)
            {
                if (workerCoroutine == null)
                {
                    workerCoroutine = StartCoroutine(workerRoutine(workerCompetence));
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
    }

    private IEnumerator workerRoutine(float workerCompetence)
    {
        while (true)
        {

            if (fillAmount < maxFillAmount)
            {
                Clicker(1);

                yield return new WaitForSeconds(workerCompetence);

                if (fillAmount >= maxFillAmount && !playerResourceActivated)
                {
                    workerResourceActivated = true;
                    _barAmountText.text = "Terminé !";

                    if (ResourceIsFood)
                    {
                        GameManager.Instance.DisplayResource.DisplayResourceFood(ResourceMoney, resourceNewImage, foodObject);
                    }
                    else
                    {
                        GameManager.Instance.DisplayResource.DisplayResource(ResourceMoney, resourceNewImage);
                    }

                    audioManager.PlaySound(_sfxSound);
                    yield return new WaitForSeconds(0.5f);
                    ChangeResource();
                    fillAmount = 0;
                    _barAmountText.text = "";
                    SetBackgroundImage(0);
                    workerResourceActivated = false;
                }
            }
        }
    }

    // Reset all

    public void ResetStats()
    {
        ChangeResource();
        fillAmount = 0;
        _barAmountText.text = "";
    }

    // Change Resource

    private void ChangeResource()
    {
        int index = Random.Range(0, GameManager.Instance.GestionResource.seasonResource.Count);

        if (ResourceIsFood)
        {
            scriptableResource = GameManager.Instance.GestionResource.seasonResource[index];
            foodObject = GameManager.Instance.GestionResource.seasonFoodObject[index];
        }

        if (ResourceIsLaundry)
        {
            scriptableResource = GameManager.Instance.GestionResource.AllLaundryResources[Random.Range(0, GameManager.Instance.GestionResource.AllLaundryResources.Count)];
        }

        resourceName = scriptableResource.ResourceName;
        resourceNewImage = scriptableResource.ResourceImage;

        float resourceClickBase = scriptableResource.GetResourceClick() * (Mathf.Pow(1.5f, (GameManager.Instance.DisplayGoal.CurrentIndex)));
        float resourceMoneyBase = (scriptableResource.GetResourceMoney() * (Mathf.Pow(1.5f, (GameManager.Instance.DisplayGoal.CurrentIndex)))) + GameManager.Instance.OvenCompetence;
        maxFillAmount = (int) resourceClickBase;
        ResourceMoney = (int) resourceMoneyBase;
    }

    // UI Background Bar
    void SetBackgroundImage(float alpha)
    {
        Color currentColor = _barBackground.color;
        currentColor.a = alpha;
        _barBackground.color = currentColor;
    }

}

