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

    public GameObject no_internet_hint,hint_button,hint_tracker;

    public int ad_index;
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
        ad_index = a;
        Advertisement.Show(placement);
        Time.timeScale = 0;
    }

    public void Show_hint()
    {

    }

    public void OnUnityAdsReady(string placementId)
    {


    }

    public void OnUnityAdsDidError(string message)
    {
        if (ad_index == 0)
        {
            no_internet.SetActive(true);
            retry.SetActive(false);
        }
        else
        {
            no_internet_hint.SetActive(true);
        }
       
    }

    public void OnUnityAdsDidStart(string placementId)
    {
      
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (ad_index == 0)
        {
            Time.timeScale = 1;
            PlayerPrefs.SetInt("loadindex", 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            Time.timeScale = 1;
            no_internet_hint.SetActive(false);
            hint_tracker.SetActive(true);
            hint_button.SetActive(false);
        }
     
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
