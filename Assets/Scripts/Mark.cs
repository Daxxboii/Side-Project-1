using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Mark : MonoBehaviour
{
    public Material material_to_search;
    public List<GameObject> gameObjects;
    public void Get()
    {
        gameObjects.Clear();
         Renderer[] rend = FindObjectsOfType(typeof(Renderer)) as Renderer[];

          for (int i = 0; i < rend.Length; i++)
          {
              if (rend[i].sharedMaterial == material_to_search)
              {
                  gameObjects.Add(rend[i].gameObject);
              }
          }
       
    }
}
