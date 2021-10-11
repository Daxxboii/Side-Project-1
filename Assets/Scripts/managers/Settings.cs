using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Scripts.Player;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public float volume = 0f;
    public float senci = 5f;
    [SerializeField]
    Slider sensitivity, music;
    public AudioMixer audiom;
    public PlayerScript Player_Script;

    public void Awake()
    {

        if (sensitivity != null)
        {
            sensitivity.minValue = 0.5f;
            sensitivity.value = senci;
        }

        music.maxValue = 20f;
        music.minValue = -50f;
        if (PlayerPrefs.GetFloat("Volume") != 0)
        {
            music.value = PlayerPrefs.GetFloat("Volume");
        }
        else
        {
            music.value = volume;
        }
        audiom.SetFloat("Audio", volume);
        setaudio();
    }

    public void setaudio()
    {
        volume = music.value;
        audiom.SetFloat("Audio", volume);
        PlayerPrefs.SetFloat("Volume", volume);
    }

    public void SetSencivity()
    {
        Player_Script.SetSensi(sensitivity.value);
        PlayerPrefs.SetFloat("Senci", sensitivity.value);
    }
}
   