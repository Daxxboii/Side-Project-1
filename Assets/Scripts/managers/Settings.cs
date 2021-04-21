using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Scripts.Player;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public float volume;
    public float senci;

    [SerializeField]
    Slider sensitivity, music;
    public AudioMixer audiom;

    public void Start()
    {
        music.value = volume;
        sensitivity.value = senci;
        audiom.SetFloat("Volume", volume);
        PlayerScript.SetSensi(senci);
        sensitivity.onValueChanged.AddListener(delegate { setaudio(); });
        music.onValueChanged.AddListener(delegate { SetSencivity(); });
       

    }

    public void setaudio()
    {
        volume = music.value;
        audiom.SetFloat("Volume", volume);
    }

    public void SetSencivity()
    {
        senci = sensitivity.value;
        PlayerScript.SetSensi(senci);
    }
    
    
}
