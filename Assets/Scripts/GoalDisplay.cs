using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GoalDisplay : MonoBehaviour
{
    [SerializeField]
    private Image _seasonDisplay;
    [SerializeField]
    private Sprite[] _seasonsSprites;
    [SerializeField]
    private TextMeshProUGUI _seasonText;
    private string[] seasonsText = { "Printemps", "Été", "Automne", "Hiver" };

    private int currentIndex = 0;
    private float seasonGoal;

    [SerializeField]
    private TextMeshProUGUI _goalText;
    private bool goalCompleted = false;

    public float PlayerGoalAmount;

    void Start()
    {
        _seasonText.text = seasonsText[currentIndex];
        _seasonDisplay.sprite = _seasonsSprites[currentIndex];

        PlayerGoalAmount = 0;
        seasonGoal = 10;
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

        currentIndex = (currentIndex + 1) % seasonsText.Length;
        _seasonText.text = seasonsText[currentIndex];
        _seasonDisplay.sprite = _seasonsSprites[currentIndex];

        goalCompleted = false;
    }
}
