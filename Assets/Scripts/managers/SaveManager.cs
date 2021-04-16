using UnityEngine;
using System.IO;
using Scripts.Timeline;
using System.Collections.Generic;
using System.Collections;

public class SaveManager : MonoBehaviour
{
    public Serializer[] serializers;
    private int index;
    public string[] id;
    public bool[] state,collider_state;
    public Vector3[] location,rotation;
    public void Start()
    {
        assign_id();
        Save();
    }

    void assign_id()
    {
        serializers = FindObjectsOfType<Serializer>();
        id = new string[serializers.Length];
        foreach (Serializer i in serializers)
        {
            id[index] = serializers[index].name;
            index++;
        }
        index = 0;
    }

   public  void Save()
    {
        check_state();
        Save_transforms();
        Save_colliders();
        PlayerPrefsX.SetStringArray("id", id);
        PlayerPrefsX.SetBoolArray("state", state);
        PlayerPrefsX.SetBoolArray("collider_state", collider_state);
        PlayerPrefsX.SetVector3Array("location", location);
        PlayerPrefsX.SetVector3Array("rotation", rotation);
        PlayerPrefs.Save();
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
    public void Load()
    {
        
    }



}
