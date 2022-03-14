using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu, settingsMenu, AreYouSureMenu, GameOverMenu, FpsCanvas, MainMenu,SaveMenu,Continue_button,About_Button,About_panel,mode_menu;
    [SerializeField] int delay;
    private bool over;
    [Header("Map")]
    public GameObject map,map_cam;
    public GameObject Map_button;
    public AudioManager AudioManager;
    private void Awake()
    {
        Resources.UnloadUnusedAssets();
        Time.timeScale = 1;
        Application.targetFrameRate = 100;
        OnDemandRendering.renderFrameInterval = 2;
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
        PostProcessingManager.blur = true;
        pauseMenu.SetActive(true);
        FpsCanvas.SetActive(false);
        settingsMenu.SetActive(false);

            AudioManager.Paused();
         
        
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
       
            PostProcessingManager.blur = false;
            FpsCanvas.SetActive(true);
            pauseMenu.SetActive(false);
            AudioManager.Unpaused();
        
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
        mode_menu.SetActive(false);
    }
    public void Link()
    {
        Application.OpenURL("https://www.lonewolfstudios.info");
    }

    public void GameMode()
    {
        pauseMenu.SetActive(false);
        mode_menu.SetActive(true);
    }

    public void Load_Credits()
    {
        SceneManager.LoadScene(3);
    }

    public void Open_map()
    {
        FpsCanvas.SetActive(false);
        map_cam.SetActive(true);
        map.SetActive(true);
        Map_button.SetActive(false);
      
        Time.timeScale = 0;
    }

    public void Close_map()
    {
        Time.timeScale = 1;
        FpsCanvas.SetActive(true);
        map.SetActive(false);
        map_cam.SetActive(false);
       
        Map_button.SetActive(true);
    }
}
