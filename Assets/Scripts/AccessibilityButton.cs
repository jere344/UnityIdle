using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccessibilityButton : MonoBehaviour
{
    [Header("Sound Effect")]
    private AudioManager audioManager;
    [SerializeField]
    private AudioClip _sfxSound;

    [Header("Lists")]
    [SerializeField]
    private List<Sprite> _originalBookmarks;
    [SerializeField]
    private List<Sprite> _leftHandedBookmarks;

    [Header("Images to scale")]
    [SerializeField]
    private GameObject _background_blank;
    [SerializeField]
    private GameObject _background_001;
    [SerializeField]
    private GameObject _background_002;
    [SerializeField]
    private GameObject _bookmarksButton;
    [SerializeField]
    private GameObject _buttonQuit;

    [Header("Buttons")]
    [SerializeField]
    private GameObject _buttonWorkersDisplay;
    [SerializeField]
    private GameObject _buttonWorkers;
    [SerializeField]
    private GameObject _buttonLevelUpDisplay;
    [SerializeField]
    private GameObject _buttonLevelUp;
    [SerializeField]
    private GameObject _buttonShopDisplay;
    [SerializeField]
    private GameObject _buttonShop;
    [SerializeField]
    private GameObject _buttonOptionsDisplay;
    [SerializeField]
    private GameObject _buttonOptions;

    [Header("Image and Sprites")]
    [SerializeField] 
    private Image _buttonImage;
    [SerializeField]
    private Sprite _spriteBlank;
    [SerializeField]
    private Sprite _spriteCross;

    [Header("Conditions")]
    private bool isSpriteBlank;
    private bool isLeftHanded = true;


    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }
    public void ChangeMainHand()
    {
        _buttonImage.sprite = isSpriteBlank ? _spriteBlank : _spriteCross;
        isSpriteBlank = !isSpriteBlank;

        if (isLeftHanded)
        {
            _background_blank.transform.localScale = new Vector3(-1f, 1f, 1f);
            _background_002.transform.localScale = new Vector3(-1f, 1f, 1f);
            _background_001.transform.localScale = new Vector3(-1f, 1f, 1f);
            _bookmarksButton.transform.localScale = new Vector3(-1f, 1f, 1f);
            _buttonQuit.transform.localScale = new Vector3(-1f, 1f, 1f);
            _buttonWorkersDisplay.transform.localScale = new Vector3(-1f, 1f, 1f);
            _buttonLevelUpDisplay.transform.localScale = new Vector3(-1f, 1f, 1f);
            _buttonShopDisplay.transform.localScale = new Vector3(-1f, 1f, 1f);
            _buttonOptionsDisplay.transform.localScale = new Vector3(-1f, 1f, 1f);
            _buttonWorkers.transform.localScale = new Vector3(-1f, 1f, 1f);
            _buttonLevelUp.transform.localScale = new Vector3(-1f, 1f, 1f);
            _buttonShop.transform.localScale = new Vector3(-1f, 1f, 1f);
            _buttonOptions.transform.localScale = new Vector3(-1f, 1f, 1f);

            _buttonWorkers.GetComponent<Image>().sprite = _leftHandedBookmarks[0];
            _buttonLevelUp.GetComponent<Image>().sprite = _leftHandedBookmarks[1];
            _buttonShop.GetComponent<Image>().sprite = _leftHandedBookmarks[2];
            _buttonOptions.GetComponent<Image>().sprite = _leftHandedBookmarks[3];
        }
        else
        {
            _background_blank.transform.localScale = new Vector3(1f, 1f, 1f);
            _background_001.transform.localScale = new Vector3(1f, 1f, 1f);
            _background_002.transform.localScale = new Vector3(1f, 1f, 1f);
            _bookmarksButton.transform.localScale = new Vector3(1f, 1f, 1f);
            _buttonQuit.transform.localScale = new Vector3(1f, 1f, 1f);
            _buttonWorkersDisplay.transform.localScale = new Vector3(1f, 1f, 1f);
            _buttonLevelUpDisplay.transform.localScale = new Vector3(1f, 1f, 1f);
            _buttonShopDisplay.transform.localScale = new Vector3(1f, 1f, 1f);
            _buttonOptionsDisplay.transform.localScale = new Vector3(1f, 1f, 1f);
            _buttonWorkers.transform.localScale = new Vector3(1f, 1f, 1f);
            _buttonLevelUp.transform.localScale = new Vector3(1f, 1f, 1f);
            _buttonShop.transform.localScale = new Vector3(1f, 1f, 1f);
            _buttonOptions.transform.localScale = new Vector3(1f, 1f, 1f);


            _buttonWorkers.GetComponent<Image>().sprite = _originalBookmarks[0];
            _buttonLevelUp.GetComponent<Image>().sprite = _originalBookmarks[1];
            _buttonShop.GetComponent<Image>().sprite = _originalBookmarks[2];
            _buttonOptions.GetComponent<Image>().sprite = _originalBookmarks[3];
        }

        isLeftHanded = !isLeftHanded;
        audioManager.PlaySound(_sfxSound);
    }
}
