using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GoalDisplay : MonoBehaviour
{
    [Header("Sound Effect")]
    private AudioManager audioManager;
    [SerializeField]
    private AudioClip _sfxSound;

    [Header("Environment Lists")]
    [SerializeField]
    private List<Sprite> _seasonsSprites;
    [SerializeField]
    private List<Sprite> _menuSprites;
    [SerializeField]
    private List<Sprite> _windowSprites;

    [Header("Environment Images")]
    [SerializeField]
    private Image _seasonDisplay;
    [SerializeField]
    private Image _menuDisplay;
    [SerializeField]
    private Image _windowDisplay;

    [Header("Environment Text")]
    [SerializeField]
    private TextMeshProUGUI _seasonText;
    private string[] seasonsText = { "Printemps", "Été", "Automne", "Hiver" };

    [Header("Goal Text")]
    [SerializeField]
    private TextMeshProUGUI _goalText;
    private bool goalCompleted = false;

    [Header("Values Goal")]
    private int newSeasonGoal;
    private int seasonGoalBase = 50;
    private float seasonGoal;
    public int CurrentIndex;
    public int PriceBase;
    public int PlayerGoalAmount;


    void Start()
    {
        PlayerGoalAmount = 0;

        audioManager = FindObjectOfType<AudioManager>();

        GameManager.Instance.GestionResource = FindObjectOfType<ResourceGestion>();

        _seasonText.text = seasonsText[CurrentIndex];
        newSeasonGoal = seasonGoalBase;

        GameManager.Instance.GestionResource.ChangeRandomResourcesList();
    }
    void Update()
    {
        if (goalCompleted == false)
        {
            if (newSeasonGoal >= 1000 && PlayerGoalAmount >= 1000)
            {
                _goalText.text = (PlayerGoalAmount / 1000) + " k / " + (newSeasonGoal / 1000) + " k";
            }
            else if (newSeasonGoal >= 1000)
            {
                _goalText.text = PlayerGoalAmount.ToString("") + " / " + (newSeasonGoal / 1000) + " k";
            }
            else
            {
                _goalText.text = PlayerGoalAmount.ToString("") + " / " + newSeasonGoal.ToString("");
            }

            if (PlayerGoalAmount >= newSeasonGoal)
            {
                goalCompleted = true;
                DisplaySeason();
            }
        }
    }

    private void DisplaySeason()
    {
        GameManager.Instance.GestionResource.ChangeRandomResourcesList();

        PlayerGoalAmount = 0;

        CurrentIndex += 1;
        PriceBase += 1;
        seasonGoal = seasonGoalBase * (Mathf.Pow(1.5f, (PriceBase)));
        newSeasonGoal = (int)seasonGoal;


        if (CurrentIndex == 4)
        {
            CurrentIndex = 0;
        }
        _seasonText.text = seasonsText[CurrentIndex];
        _seasonDisplay.sprite = _seasonsSprites[CurrentIndex];
        _menuDisplay.sprite = _menuSprites[CurrentIndex];
        _windowDisplay.sprite = _windowSprites[CurrentIndex];

        ClickableObject[] allClickers = FindObjectsOfType<ClickableObject>();

        foreach (ClickableObject clicker in allClickers)
        {
            clicker.ResetStats();
        }

        GameManager.Instance.LouisCompetence += 0.4f;
        if (GameManager.Instance.JulesV2)
        {
            GameManager.Instance.JulesCompetence += 0.4f;
        }

        audioManager.PlaySound(_sfxSound);
        goalCompleted = false;
    }
}