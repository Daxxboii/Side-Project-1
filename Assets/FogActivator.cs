using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogActivator : MonoBehaviour
{
    public Settings settings;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            settings.Managefog();
           
        }
       
    }
}
