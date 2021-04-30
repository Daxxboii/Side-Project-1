 using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Player;
using UnityEngine.UI;
using TMPro;
using Scripts.Timeline;
public class ComicManager : MonoBehaviour
{

    [Serializable]
    private struct Comic
    {
        public Sprite[] images;
        public string[] comic_text;
        public bool objective;
    }

    [SerializeField]
    private Comic[] comics;
    [SerializeField]
    private Image Panel;
    [SerializeField]
    private Timeline_Manager tm;
    [SerializeField]
    private TextMeshProUGUI subs;

  //  [HideInInspector]
    public int comic_index, page_index = 0;

    private void Start()
    {
        Panel.sprite = comics[comic_index].images[page_index];
    }
    public void Comic_Open()
    {
        if (comics[comic_index].objective)
        {
            tm.ObjectiveList();
        }
        Debug.Log("open");
        Time.timeScale = 0f;
        Panel.gameObject.SetActive(true);
        Panel.sprite = comics[comic_index].images[page_index];
        subs.text = comics[comic_index].comic_text[page_index];
    }
    public void Comic_Close()
    {
        Time.timeScale = 1f;
        Panel.gameObject.SetActive(false);
        comic_index++;
        page_index = 0;
    }

    public void Next()
    {
       
        if (page_index < comics[comic_index].images.Length -1)
        {
            page_index++;
            Panel.sprite = comics[comic_index].images[page_index];
            subs.text = comics[comic_index].comic_text[page_index];
        }
    //    Debug.Log(page_index);
    }
    public void Previous()
    {
        if (page_index != 0)
        {
            page_index--;
        }
        Panel.sprite = comics[comic_index].images[page_index];
        subs.text = comics[comic_index].comic_text[page_index];
    }

}


