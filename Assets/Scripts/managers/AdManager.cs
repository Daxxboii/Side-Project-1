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
    string mySurfacingId = "4083073";
    public bool GameMode ;
    string placement = "Rewarded_Android";
    string banner_placement = "Banner_Android";
    public PlayerScript ps;
    public GameObject no_internet,retry;
   
    void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(mySurfacingId, true);
     //   Debug.Log(SceneManager.GetActiveScene().buildIndex);
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            StartCoroutine(ShowBannerWhenInitialized());
        }
        else
        {
            Advertisement.Banner.Hide();
        }
    }
   public void Show(int a)
    {
        Advertisement.Show(placement);
        Time.timeScale = 0;
    }

  

    public void OnUnityAdsReady(string placementId)
    {


    }

    public void OnUnityAdsDidError(string message)
    {
            no_internet.SetActive(true);
            retry.SetActive(false);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
      
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
            Time.timeScale = 1;
            PlayerPrefs.SetInt("loadindex", 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator ShowBannerWhenInitialized()
    {
        while (!Advertisement.isInitialized)
        {
            yield return new WaitForSeconds(10f);
        }
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Show(banner_placement);
       
    }
}
