using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GoalDisplay : MonoBehaviour
{
    private ResourceGestion randomResource;
    private ClickableObject clickableObjects;
    private ResourceScriptable resourceScriptable;

    [SerializeField]
    private Image _seasonDisplay;
    [SerializeField]
    private Image _menuDisplay;
    [SerializeField]
    private Sprite[] _seasonsSprites;
    [SerializeField]
    private Sprite[] _menuSprites;
    [SerializeField]
    private TextMeshProUGUI _seasonText;
    private string[] seasonsText = { "Printemps", "Été", "Automne", "Hiver" };

    public int currentIndex = 0;
    private int newSeasonGoal;
    private int seasonGoalBase = 50;
    private float seasonGoal;

    public int PlayerGoalAmount;


    [SerializeField]
    private TextMeshProUGUI _goalText;
    private bool goalCompleted = false;


    void Start()
    {
        randomResource = FindObjectOfType<ResourceGestion>();
        clickableObjects = FindObjectOfType<ClickableObject>();



        _seasonText.text = seasonsText[currentIndex];
        _seasonDisplay.sprite = _seasonsSprites[currentIndex];

        PlayerGoalAmount = 0;
        newSeasonGoal = seasonGoalBase;


        randomResource.ChangeRandomResourcesList();
    }
    void Update()
    {
        if (goalCompleted == false)
        {
            _goalText.text = "Objectif : " + PlayerGoalAmount.ToString("") + "/" + newSeasonGoal.ToString("") + " Or";

            if (PlayerGoalAmount >= newSeasonGoal)
            {
                goalCompleted = true;
                DisplaySeason();
            }
        }
    }

    private void DisplaySeason()
    {
        randomResource.ChangeRandomResourcesList();

        PlayerGoalAmount = 0;

        currentIndex += 1;
        seasonGoal = seasonGoalBase * (Mathf.Pow(1.5f, (currentIndex)));
        newSeasonGoal = (int)seasonGoal;
        _seasonText.text = seasonsText[currentIndex];
        _seasonDisplay.sprite = _seasonsSprites[currentIndex];
        _menuDisplay.sprite = _menuSprites[currentIndex];

        clickableObjects.ResetStats();

        goalCompleted = false;

    }
}