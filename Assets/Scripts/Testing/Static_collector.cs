using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Static_collector : MonoBehaviour
{
    public Material[] light;
    public MeshRenderer[] objs,lights;
    int index;
    void Start()
    {
        Collect();
    }
    void Collect()
    {
        objs = UnityEngine.Object.FindObjectsOfType<MeshRenderer>();

        foreach (MeshRenderer go in objs)
        {
          
            if (go.gameObject.activeInHierarchy)
            {

               if((go.material == light[0] || go.material == light[1]))
                {
                    lights[index] = go;
                }
            }
            index++;
        }
    }
}
