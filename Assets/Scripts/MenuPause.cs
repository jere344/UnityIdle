using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
    public GameObject MenuPauseUI;
    private bool gameIsPaused;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                RestartGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void RestartGame()
    {
        MenuPauseUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void PauseGame()
    {
        MenuPauseUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void TitleMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("BubbleCoffee_MenuScreen");
    }
}
