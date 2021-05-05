using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
public class AdManager : MonoBehaviour ,IUnityAdsListener
{
    public GameObject menu;
    public SaveManager save;
    string mySurfacingId = "4083073";
    public bool GameMode = true;
    string placement = "Rewarded_Android";


    void Start()
    {
      
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
       
    }

    public void OnUnityAdsDidStart(string placementId)
    {
      
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if(showResult == ShowResult.Finished)
        {
            menu.SetActive(false);
            save.Save();
        }
        PlayerPrefs.SetInt("loadindex", 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
      
    }
}
