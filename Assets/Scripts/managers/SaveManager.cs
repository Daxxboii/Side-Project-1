using UnityEngine;
using System.IO;
using Scripts.Timeline;
using System.Collections.Generic;
using System.Collections;

public class SaveManager : MonoBehaviour
{
    public Serializer[] objects;
    private int index;
    public string[] id;

    public void Start()
    {
        assign_id();
    }

    void assign_id()
    {
        objects = FindObjectsOfType<Serializer>();
        id = new string[objects.Length];
        foreach (Serializer i in objects)
        {
            objects[index].Save();
            id[index] = objects[index].name;
            index++;
        }
        index = 0;
    }


}
