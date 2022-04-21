﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Scripts.Player;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    public static float volume = 0f;
    public static float senci = 5f;
    [SerializeField]private Slider sensitivity_Slider, music_Slider;
    public AudioMixer audiom;
    public PlayerScript Player_Script;

    public void Awake()
    {
        music_Slider.maxValue = 20f;
        music_Slider.minValue = -50f;

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {

        }
        else
        {  
            sensitivity_Slider.minValue = 0.5f;
            if (PlayerPrefs.HasKey("Senci"))
            {
                sensitivity_Slider.value = PlayerPrefs.GetFloat("Senci");
            }
            else
            {
              sensitivity_Slider.value = senci;
            }
            SetSencivity();
        }
       

        if (PlayerPrefs.HasKey("Volume"))
        {
            music_Slider.value = PlayerPrefs.GetFloat("Volume");
        }
        else
        {
            music_Slider.value = volume;
        }
        setaudio();
    }

    public void setaudio()
    {
        PlayerPrefs.SetFloat("Volume", music_Slider.value);
        audiom.SetFloat("Audio", music_Slider.value);
    }

    public void SetSencivity()
    {
        Player_Script.SetSensi(sensitivity_Slider.value);
        PlayerPrefs.SetFloat("Senci", sensitivity_Slider.value);
    }
}
   