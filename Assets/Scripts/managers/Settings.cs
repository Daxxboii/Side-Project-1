using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Scripts.Player;

public class Settings : MonoBehaviour
{
    
    public AudioMixer audiom;
    
    public void setaudio(float volume)
    {
        audiom.SetFloat("Volume", volume);
    }

    public void SetSencivity(float senci)
    {
        PlayerScript.SetSensi(senci);
    }
    
    
}
