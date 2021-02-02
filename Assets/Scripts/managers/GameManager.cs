using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu, settingsMenu, AreYouSureMenu, GameOverMenu, FpsCanvas,UnhideCanvas;
    private void Awake()
    {
        Time.timeScale = 1;
    }
    public void Play()
    {
        SceneManager.LoadScene(1);
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

    }
    public void settings()
    {
        settingsMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }
    public void BackToPause()
    {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
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
        UnhideCanvas.SetActive(true);
        pauseMenu.SetActive(false);
    }
}
