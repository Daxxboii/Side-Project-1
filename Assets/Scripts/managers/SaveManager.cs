using UnityEngine;
using Scripts.Timeline;
using Scripts.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;
using PlayerPrefs = PreviewLabs.PlayerPrefs;


public class SaveManager : MonoBehaviour
{

    public CharacterController col;
    public Serializer[] serializers;
    public PlayerScript ps;
    private int index;
    public string[] id;
    public bool[] state,collider_state,rigidbodies;
    public Vector3[] location,rotation;
    public ComicManager comic = null;
    public Timeline_Manager tm = null;
    public Camcorder cam = null;
   
    public void Start()
    {
      
        if ( SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (PlayerPrefsX.GetBool("Saved"))
            {
                Load();
            }
         
        }
    }

   
    public void Loader(int load_index)
    {
        PlayerPrefs.SetInt("loadindex", load_index);
        PlayerPrefs.Flush();
    }

   public  void Save()
    {
        
        check_state();
        Save_transforms();
        Save_colliders();
        Save_rigidbodies();
      //  PlayerPrefsX.SetStringArray("id", id);
        PlayerPrefsX.SetBoolArray("state", state);
        PlayerPrefsX.SetBoolArray("collider_state", collider_state);
        PlayerPrefsX.SetVector3Array("location", location);
        PlayerPrefsX.SetVector3Array("rotation", rotation);
        PlayerPrefsX.SetBoolArray("rigidbodies", rigidbodies);
        PlayerPrefs.SetInt("Current_cutscene", tm.Current_cutscene);
        PlayerPrefs.SetInt("Subtitle_index", tm.index);
        PlayerPrefs.SetInt("Objective_index", tm.objective_index);
        PlayerPrefs.SetInt("Comic_index", comic.comic_index);
        PlayerPrefs.SetFloat("Health", ps.Health);
       
        PlayerPrefs.SetFloat("Time",cam.time);

        PlayerPrefsX.SetBool("Saved", true);
        PlayerPrefs.Flush();
       
    }

    void check_state()
    {
        state = new bool[serializers.Length];
        foreach (Serializer i in serializers)
        {
            if (serializers[index].gameObject.activeSelf)
            {
                state[index] = true;
            }
            else
            {
                state[index] = false;
            }
          index++;
        }
        index = 0;
    }
    void Save_transforms()
    {
        location = new Vector3[serializers.Length];
        rotation = new Vector3[serializers.Length];
        foreach (Serializer i in serializers)
        {
            serializers[index].Assign_transforms();
            location[index] = serializers[index].loc;
            rotation[index] = serializers[index].rot;
            index++;
        }
        index = 0;
    }

    void Save_colliders()
    {
        collider_state = new bool[serializers.Length];
        foreach (Serializer i in serializers)
        {
            serializers[index].collider();
            collider_state[index] = serializers[index].collider_state;
            index++;
        }
        index = 0;
    }

    void Save_rigidbodies()
    {
        rigidbodies = new bool[serializers.Length];
        foreach (Serializer i in serializers)
        {
            rigidbodies[index] = serializers[index].Rigidbody_Exists();
            index++;
        }
        index = 0;
    }
    public void Load()
    {

        if (PlayerPrefs.GetInt("loadindex") == 1)
        {
             ps.enabled = false;
            col.enabled = false;
            load_arrays();
            assign();
           
        }
    }

    void load_arrays()
    {
    //  id = PlayerPrefsX.GetStringArray("id");
      state =  PlayerPrefsX.GetBoolArray("state");
      collider_state = PlayerPrefsX.GetBoolArray("collider_state");
      location = PlayerPrefsX.GetVector3Array("location");
      rotation =  PlayerPrefsX.GetVector3Array("rotation");
      rigidbodies =  PlayerPrefsX.GetBoolArray("rigidbodies");
      tm.Current_cutscene =  PlayerPrefs.GetInt("Current_cutscene");
        comic.comic_index = PlayerPrefs.GetInt("Comic_index");
        ps.Health = PlayerPrefs.GetFloat("Health");
     
      
        cam.time = PlayerPrefs.GetFloat("Time");
       tm.index =  PlayerPrefs.GetInt("Subtitle_index");
        tm.objective_index = PlayerPrefs.GetInt("Objective_index");
    }

    void assign()
    {
        foreach(Serializer i in serializers)
        {
            //Active
            serializers[index].gameObject.SetActive(state[index]);
           
            //Location & Rotation
            serializers[index].gameObject.transform.position = location[index];
            serializers[index].gameObject.transform.eulerAngles = rotation[index];
            //Collider
            if (serializers[index].gameObject.GetComponent<Collider>() != null)
            {
                serializers[index].gameObject.GetComponent<Collider>().enabled = collider_state[index];
            }
            //Rigidbody
            if (serializers[index].gameObject.GetComponent<Rigidbody>() != null)
            {
                if (rigidbodies[index]== false)
                {
                    serializers[index].gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                }
                else
                {
                    serializers[index].gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
            }
            if (serializers[index].gameObject.tag == "Player")
            {
               // col.gameObject.transform.position = new Vector3(location[index].x, location[index].y, location[index].z);
                 ps.enabled = true;
                col.enabled = true;
            }

            index++;
        }
      
        index = 0;
      

    }
   public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }


}
