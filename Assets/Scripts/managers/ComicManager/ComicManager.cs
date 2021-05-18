 using System.Collections;
using System;
using UnityEngine;
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
    Animator load;

    [SerializeField]
    private Comic[] comics;
    [SerializeField]
    private Image Panel;
    [SerializeField]
    private Timeline_Manager tm;
    [SerializeField]
    private TextMeshProUGUI subs;
    [SerializeField]
    private GameObject exit;
  //  [HideInInspector]
    public int comic_index, page_index = 0;

    private void Start()
    {
        Panel.sprite = comics[comic_index].images[page_index];
    }
    public void Comic_Open()
    {
        load.gameObject.SetActive(true);
        load.SetBool("Open", true);
        StopCoroutine(Load());
        StartCoroutine(Load());
        if (comics[comic_index].objective)
        {
            tm.ObjectiveList();
        }
        exit.SetActive(false);
    }
    public void Comic_Close()
    {
        load.gameObject.SetActive(false);
        load.SetBool("Open", false);
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
        else
        {
            exit.SetActive(true);
        }
    //    Debug.Log(page_index);
    }
    public void Previous()
    {
        if (page_index != 0)
        {
            page_index--;
        }
       
            exit.SetActive(false);
        
        Panel.sprite = comics[comic_index].images[page_index];
        subs.text = comics[comic_index].comic_text[page_index];
    }

    IEnumerator Load()
    {
      
        yield return new WaitForSeconds(1);
        Panel.gameObject.SetActive(true);
      
        Panel.sprite = comics[comic_index].images[page_index];
        subs.text = comics[comic_index].comic_text[page_index];
        Time.timeScale = 0f;
       
    }
}


