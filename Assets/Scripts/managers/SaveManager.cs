using UnityEngine;
using System.IO;
using Scripts.Timeline;
using System.Collections.Generic;
using System.Collections;

public class SaveManager : MonoBehaviour
{
    public Serializer serializer;
    public Vector3[] playerpos, cutscenepos, principalpos, girlpos, objpos;
    [SerializeField]
    public GameObject player, principal, girl, cutscene;
    [SerializeField]
    public Timeline_Manager tm;

   
    public bool[] active,colliders;
  
    public void Save()
    {
        SaveVectors();
        AddValues();
        SaveList();
        PlayerPrefs.Save();
    }
    //save
    void SaveList()
    {
        serializer.Save();
        active = new bool[serializer.objects.Length];
        colliders = new bool[serializer.objects.Length];


        for (int i = 0; i < serializer.objects.Length; i++)
        {
            if (serializer.objects[i] != null)
            {
                if (serializer.objects[i].activeSelf)
                {
                    active[i] = true;
                }
                else
                {
                    active[i] = false;
                }
                if (serializer.objects[i].GetComponent<Collider>().enabled)
                {
                    colliders[i] = true;
                }
                else
                {
                    colliders[i] = false;
                }

            }
        }
        PlayerPrefsX.SetBoolArray("active", active);
    }

    //save
    void SaveVectors()
    {
        playerpos = new Vector3[2];
        girlpos = new Vector3[2];
        cutscenepos = new Vector3[2];
        principalpos = new Vector3[2];
        playerpos[0] = player.transform.position;
        playerpos[1] = player.transform.rotation.eulerAngles;
        principalpos[0] = principal.transform.position;
        principalpos[1] = principal.transform.rotation.eulerAngles;
        girlpos[0] = girl.transform.position;
        girlpos[1] = girl.transform.rotation.eulerAngles;
        cutscenepos[0] = cutscene.transform.position;
        cutscenepos[1] = cutscene.transform.rotation.eulerAngles;

    }
    //save
    void AddValues()
    {
        //timeline
        PlayerPrefs.SetInt("timeline_index", tm.Current_cutscene);
        //positions
        PlayerPrefsX.SetVector3Array("Playerpos", playerpos);
        PlayerPrefsX.SetVector3Array("Cutscenepos", cutscenepos);
        PlayerPrefsX.SetVector3Array("Girlpos", girlpos);
        PlayerPrefsX.SetVector3Array("Principalpos", principalpos);


    }

    //load
    public void Load()
    {
        //timeline
        tm.Current_cutscene = PlayerPrefs.GetInt("timeline_index");
        //positions
        ApplyValues();
        //active
        Fetch();
    
    }
    //load
    void ApplyValues()
    {
        player.transform.position = PlayerPrefsX.GetVector3Array("Playerpos")[0];
        player.transform.eulerAngles = PlayerPrefsX.GetVector3Array("Playerpos")[1];
        girl.transform.position = PlayerPrefsX.GetVector3Array("Girlpos")[0];
        girl.transform.eulerAngles = PlayerPrefsX.GetVector3Array("Girlpos")[1];
        principal.transform.position = PlayerPrefsX.GetVector3Array("Principalpos")[0];
        principal.transform.eulerAngles = PlayerPrefsX.GetVector3Array("Principalpos")[1];
        cutscene.transform.position = PlayerPrefsX.GetVector3Array("Cutscenepos")[0];
        cutscene.transform.eulerAngles = PlayerPrefsX.GetVector3Array("Cutscenepos")[1];
    }
    //load
    void Fetch()
    {
        for (int i = 0; i < serializer.objects.Length-1; i++)
        {
            if (serializer.objects[i] != null)
            {
                if (PlayerPrefsX.GetBoolArray("active")[i])
                {
                    serializer.objects[i].SetActive(true);
                }
                else
                {
                    serializer.objects[i].SetActive(false);
                }
            }
        }
    }
}
