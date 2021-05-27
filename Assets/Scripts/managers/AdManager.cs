﻿using System.Collections;
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
     PlayerPrefs.SetInt("loadindex", 0);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
       
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
}
