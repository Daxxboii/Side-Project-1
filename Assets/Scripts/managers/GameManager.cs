using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu, settingsMenu, AreYouSureMenu, GameOverMenu, FpsCanvas, MainMenu;
    private void Awake()
    {
        Time.timeScale = 1;
    }
    public void credits()
    {
        SceneManager.LoadScene(2);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    public void Pause()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        FpsCanvas.SetActive(false);

    }
    public void settings()
    {
        settingsMenu.SetActive(true);

        pauseMenu.SetActive(false);
    }
    public void settings2()
    {
        settingsMenu.SetActive(true);

        MainMenu.SetActive(false);
    }
    public void BackToPause()
    {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }
    public void BackToPause2()
    {
        settingsMenu.SetActive(false);
        MainMenu.SetActive(true);
    }
    public void playerDeath()
    {
        
    }
    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void resume()
    {
        Time.timeScale = 1;
        FpsCanvas.SetActive(true);
        pauseMenu.SetActive(false);
    }
    public void back()
    {
        settingsMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }
}
