using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.RainMaker;
public class FogActivator : MonoBehaviour
{
    public RainScript rain;
    public static bool inside;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            inside = !inside;
            if (inside)
            {
                rain.RainIntensity = 0.0f;
            }
          
        }
       
    }

    private void FixedUpdate()
    {
        if (inside)
        {
            rain.RainIntensity = 0.0f;
        }
    }
}
