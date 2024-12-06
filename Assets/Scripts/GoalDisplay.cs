using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GoalDisplay : MonoBehaviour
{
    private ResourcesGestion randomResource;
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

    private int currentIndex = 0;
    private int seasonGoal;

    public int PlayerGoalAmount;
    public int Multiplicator;


    [SerializeField]
    private TextMeshProUGUI _goalText;
    private bool goalCompleted = false;


    void Start()
    {
        randomResource = FindObjectOfType<ResourcesGestion>();

        _seasonText.text = seasonsText[currentIndex];
        _seasonDisplay.sprite = _seasonsSprites[currentIndex];

        PlayerGoalAmount = 0;
        seasonGoal = 50;

        Multiplicator = 1;


        randomResource.ChangeRandomResourcesList();
    }
    void Update()
    {
        if (goalCompleted == false)
        {
            _goalText.text = "Objectif : " + PlayerGoalAmount.ToString("") + "/" + seasonGoal.ToString("") + " Or";

            if (PlayerGoalAmount >= seasonGoal)
            {
                goalCompleted = true;
                DisplaySeason();
            }
        }
    }

    private void DisplaySeason()
    {
        PlayerGoalAmount = 0;

        if (Multiplicator == 1)
        {
            Multiplicator = 0;
        }

        Multiplicator += 2;
        seasonGoal *= Multiplicator;

        currentIndex = (currentIndex + 1) % seasonsText.Length;
        _seasonText.text = seasonsText[currentIndex];
        _seasonDisplay.sprite = _seasonsSprites[currentIndex];
        _menuDisplay.sprite = _menuSprites[currentIndex];


        randomResource.ChangeRandomResourcesList();

        goalCompleted = false;

    }
}