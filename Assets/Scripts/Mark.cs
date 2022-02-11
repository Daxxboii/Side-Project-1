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
        Material[] originalMats = Resources.FindObjectsOfTypeAll<Material>();
        for (int i = 0; i < rend.Length; i++)
        {
          
            Material[] mats = rend[i].sharedMaterials;
            Material[] tempMats = new Material[mats.Length];
          
            for (int j = 0; j < originalMats.Length; j++)
            {

                for (int k = 0; k < mats.Length; k++)
                {
                    if (mats[k] != null)
                    {


                        if ((originalMats[j].name + " (Instance) (Instance)" == mats[k].name) && mats[k] != null)
                        {

                            tempMats[k] = originalMats[j];


                        }

                        else if ((originalMats[j].name + " (Instance)" == mats[k].name) && mats[k] != null)
                        {
                            tempMats[k] = originalMats[j];

                        }


                        else if ((originalMats[j].name == mats[k].name) && mats[k] != null)
                        {

                            tempMats[k] = originalMats[j];


                        }
                    }
                }

            }

            rend[i].sharedMaterials = tempMats;


        }
    }

}

