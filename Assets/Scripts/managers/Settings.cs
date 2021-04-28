using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Scripts.Player;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public float volume = 0f;
    public float senci = 0.0f;

    [SerializeField]
    Slider sensitivity, music;
    public AudioMixer audiom;

    public void Start()
    {
        sensitivity.minValue = 0.5f;
        music.maxValue = 0f;
        music.minValue = -50f;
       if (PlayerPrefs.GetFloat("Volume") != 0)
        {
            music.value = PlayerPrefs.GetFloat("Volume");
        }
        else
        {
            music.value = volume;
        }
        if (PlayerPrefs.GetFloat("Senci") != 0)
        {
            sensitivity.value = PlayerPrefs.GetFloat("Senci");
        }
        else
        {
            sensitivity.value = senci;
        }


        sensitivity.value = senci;
        audiom.SetFloat("Audio", volume);
        PlayerScript.SetSensi(senci);
    }

    public void setaudio()
    {
        volume = music.value;
        audiom.SetFloat("Audio", volume);
        PlayerPrefs.SetFloat("Volume", volume);
    }

    public void SetSencivity()
    {
      
        senci = sensitivity.value;
        PlayerScript.SetSensi(senci);
        PlayerPrefs.SetFloat("Senci", senci);
    }
    
    
}
