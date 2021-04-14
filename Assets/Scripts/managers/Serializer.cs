using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Serializer : MonoBehaviour
{

   public GameObject[] objects;
   
    int index;
    public void Save()
    {
        objects = new GameObject[5000];
        foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
        {
            objects[index] = go;
            index++;
        }
     
    }
}
