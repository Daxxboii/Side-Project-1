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
    public Toggle fog;
    public GameObject[] fogs;
    [SerializeField]
    Slider sensitivity, music;
    public AudioMixer audiom;
    public ParticleSystemRenderer[] fog_system;
    private void Start()
    {
      if(PlayerPrefsX.GetBool("Saved"))
        {
                fog.isOn = PlayerPrefsX.GetBool("Fogs");
        }
        else
        {
            fog.isOn = true;
        }
       
    }
    public void Awake()
    {
        if(fog!= null)
        {
            if (fog.isOn)
            {
                fogs[0].SetActive(true);
                fogs[1].SetActive(true);
            }
            else
            {
                fogs[0].SetActive(false);
                fogs[1].SetActive(false);
            }
        }
        if (sensitivity != null)
        {
            sensitivity.minValue = 0.5f;
            sensitivity.value = senci;
        }
       
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
        PlayerScript.SetSensi(sensitivity.value);
        PlayerPrefs.SetFloat("Senci", sensitivity.value);
    }
    private void FixedUpdate()
    {
        if (fog != null)
        {
            if (fog.isOn)
            {
                fogs[0].SetActive(true);
                fogs[1].SetActive(true);
            }
            else
            {
                fogs[0].SetActive(false);
                fogs[1].SetActive(false);
            }
        }
    }

    private void OnApplicationQuit()
    {
        if (fog.isOn)
        {
            PlayerPrefsX.SetBool("Fogs", true);
        }
        else
        {
            PlayerPrefsX.SetBool("Fogs", false);
        }
    }

    public void Managefog()
    {
        fog_system[0].enabled = !fog_system[0].enabled;
        fog_system[1].enabled = !fog_system[1].enabled;

    }
}
