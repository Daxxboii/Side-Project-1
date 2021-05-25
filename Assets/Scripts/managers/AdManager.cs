using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using Scripts.Player;
using UnityEngine.SceneManagement;

public class AdManager : MonoBehaviour ,IUnityAdsListener
{
    [SerializeField]
    private GameObject menu;
    public SaveManager save;
    string mySurfacingId = "4083073";
    public bool GameMode = true;
    string placement = "Rewarded_Android";
    public PlayerScript ps;

    void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(mySurfacingId, true);
    }
   public void Show()
    {
        Advertisement.Show(placement);
      
    }

    public void OnUnityAdsReady(string placementId)
    {


    }

    public void OnUnityAdsDidError(string message)
    {
     /*   PlayerPrefs.SetInt("loadindex", 0);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);*/
       
    }

    public void OnUnityAdsDidStart(string placementId)
    {
      
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        
        if(showResult == ShowResult.Finished)
        {
            Time.timeScale = 1;
            if(menu != null)
            {
                menu.SetActive(false);
            }
            save = GameObject.FindWithTag("SaveManager").GetComponent<SaveManager>(); 
            ps.Health = 75;
            save.Save();
        }
        Time.timeScale = 1;
        Debug.Log("AD");
        PlayerPrefs.SetInt("loadindex", 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
      
      
      
    }
}
