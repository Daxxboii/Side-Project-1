using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu, settingsMenu, AreYouSureMenu, GameOverMenu, FpsCanvas, MainMenu,SaveMenu,Continue_button,About_Button,About_panel;
    [SerializeField] int delay;
    private bool over;
   
    private void Awake()
    {
        Time.timeScale = 1;
    }
    public void credits()
    {
        pauseMenu.SetActive(false);
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

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Pause()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        FpsCanvas.SetActive(false);
        settingsMenu.SetActive(false);

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
        SaveMenu.SetActive(false);
            settingsMenu.SetActive(false);
            MainMenu.SetActive(true);
        
    }
    public void playerDeath()
    {
        
    }
    public void BackToMenu()
    {
       
        Time.timeScale = 1;
        settingsMenu.SetActive(false);
        MainMenu.SetActive(true);

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
    public void Save_menu()
    {
        SaveMenu.SetActive(true);
        settingsMenu.SetActive(false);
        MainMenu.SetActive(false);
        if (PlayerPrefsX.GetBool("Saved"))
        {
            Continue_button.SetActive(true);
        }
        else
        {
            Continue_button.SetActive(false);
        }
    }
    public void _Start()
    {
        SaveMenu.SetActive(false);
    }
   public void About()
    {
        MainMenu.SetActive(false);
        About_panel.SetActive(true);
    }
    public void BackFromAbout()
    {
        About_panel.SetActive(false);
        MainMenu.SetActive(true);
    }
    public void Link()
    {
        Application.OpenURL("https://www.lonewolfstudios.info");
    }
}
