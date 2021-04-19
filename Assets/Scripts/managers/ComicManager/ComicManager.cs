 using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Player;
using UnityEngine.UI;


public class ComicManager : MonoBehaviour
{

    [Serializable]
    private struct Comic
    {
        public Sprite[] images;
    }

    [SerializeField]
    private Comic[] comics;
    [SerializeField]
    private Image Panel;
    [HideInInspector]
    public int comic_index, page_index = 0;

    private void Start()
    {
        Panel.sprite = comics[comic_index].images[page_index];
    }
    public void Comic_Open()
    {
        Time.timeScale = 0f;
        Panel.gameObject.SetActive(true);
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
        if (page_index <= comics[comic_index].images.Length)
        {
            page_index++;
        }
        Panel.sprite = comics[comic_index].images[page_index];
    }
    public void Previous()
    {
        if (page_index != 0)
        {
            page_index--;
        }
        Panel.sprite = comics[comic_index].images[page_index];
    }

}


