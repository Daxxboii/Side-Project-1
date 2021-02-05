using System;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

public class LightFliaker : MonoBehaviour
{
    [SerializeField]
    lightingVariables l = new lightingVariables();
    Light lumin;
    [Serializable]
    struct lightingVariables
    {
        public float luminValStart, LuminValMax, r;
        public bool isFlickering;
    }
    void Start()
    {
        lumin = GetComponent<Light>();
    }
    void Update()
    {
        if(l.isFlickering)
        {
        }

        
        else
        {

        }
    }
}
