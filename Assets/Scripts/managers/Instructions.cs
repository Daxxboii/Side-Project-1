﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Instructions : MonoBehaviour
{
    [SerializeField]
    private GameObject Fps_Canvas;
    public Sprite[] instructions;
    public GameObject panel;
    public Image image;
    private int index;
    void Start()
    {
        if (!PlayerPrefsX.GetBool("Saved"))
        {
            Fps_Canvas.SetActive(false);
            panel.SetActive(true);
            PostProcessingManager.blur = true;
        }
    }

   public void Next()
    {
        if (index < instructions.Length-1)
        {
            index++;
            image.sprite = instructions[index];
        }
   
    }

   public void Previous()
    {
        if (index > 0)
        {
            index--;
            image.sprite = instructions[index];
        }
       
    }
    public void Close()
    {
        PostProcessingManager.blur = false;
        Fps_Canvas.SetActive(true);
        panel.SetActive(false);
    }
}
