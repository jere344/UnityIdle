using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private GameObject _audioManager;
    [SerializeField]
    private Animator transitionAnim;
    [SerializeField]
    private GameObject _transitionObject;

    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120;
    }

    public void NextLevel()
    {
        DontDestroyOnLoad(_audioManager);
        StartCoroutine(LoadLevel());
    }

    private IEnumerator LoadLevel()
    {
        _transitionObject.SetActive(true);
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("BubbleCoffee_GameScreen");
    }
}
