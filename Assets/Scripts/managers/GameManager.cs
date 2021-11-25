using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    //Controls
    public PlayerControls controls;

    [SerializeField] GameObject pauseMenu, settingsMenu, AreYouSureMenu, GameOverMenu, FpsCanvas, MainMenu,SaveMenu,Continue_button,About_Button,About_panel,mode_menu;
    [SerializeField] int delay;

    public static bool map_taken;
    [Header("Map")]
    public GameObject map,map_cam;

    public bool pause;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Controls.Map.performed += ctx => Open_map();
        Time.timeScale = 1;
        controls.Controls.Escape.performed += ctx =>pause =!pause;
        controls.Controls.Escape.performed += ctx => Pause();


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
        if (pause)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            FpsCanvas.SetActive(false);
            settingsMenu.SetActive(false);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
            FpsCanvas.SetActive(true);
            pauseMenu.SetActive(false);
        }

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
    public void BackToMenu()
    {
       
        Time.timeScale = 1;
        settingsMenu.SetActive(false);
        MainMenu.SetActive(true);

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
        if (map_taken)
        {
            FpsCanvas.SetActive(false);
            map_cam.SetActive(true);
            map.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Close_map()
    {
        Time.timeScale = 1;
        FpsCanvas.SetActive(true);
        map.SetActive(false);
        map_cam.SetActive(false);
       
      
    }

    private void OnEnable()
    {
        controls.Controls.Enable();
    }

    private void OnDisable()
    {
        controls.Controls.Disable();
    }
   
}
