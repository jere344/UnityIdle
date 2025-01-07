using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject _audioManager;

    public void LoadScene()
    {
        DontDestroyOnLoad(_audioManager);
        SceneManager.LoadScene("BubbleCoffee_GameScreen");
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
