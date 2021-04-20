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
        audiom.SetFloat("Volume", volume);
        PlayerScript.SetSensi(senci);

    }

    public void setaudio(Slider _volume)
    {
        volume = _volume.value;
        audiom.SetFloat("Volume", volume);
       
       
    }

    public void SetSencivity(Slider _senci)
    {
        senci = _senci.value;
        PlayerScript.SetSensi(senci);
       
    }
    
    
}
