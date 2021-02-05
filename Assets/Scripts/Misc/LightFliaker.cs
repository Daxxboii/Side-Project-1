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
        public float luminValStart, LuminValMax, r, ft, fit;
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
            l.ft += Time.deltaTime;
            if(l.ft > l.fit)
            {
                l.r = Random.Range(l.luminValStart, l.LuminValMax);
                l.ft = 0.0f;
            }
            lumin.intensity = Mathf.Lerp(0f, l.r, 0.1f); ;
        }
    }
}
