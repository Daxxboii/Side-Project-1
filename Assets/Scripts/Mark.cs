using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Mark : MonoBehaviour
{
    public Texture material_to_search;
    public List<Material> gameObjects;
    public void Get()
    {
        gameObjects.Clear();
         Renderer[] rend = FindObjectsOfType(typeof(Renderer)) as Renderer[];

          for (int i = 0; i < rend.Length; i++)
          {
              if (rend[i].sharedMaterial.mainTexture == material_to_search)
              {
				if (!gameObjects.Contains(rend[i].sharedMaterial))
				{
                    gameObjects.Add(rend[i].sharedMaterial);
                }
            }
          }
       
    }
}
