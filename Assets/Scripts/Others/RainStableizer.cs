﻿using DigitalRuby.RainMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainStableizer : MonoBehaviour
{
    //[SerializeField]
    //AudioSource thunder;
    [SerializeField]
    private float Timerstert, MaxTime;
    [SerializeField]
    RainScript RS;

    private void Start()
    {
        if (!PlayerPrefsX.GetBool("Saved"))
        {
            FogActivator.inside = false;
        }
    }
    void FixedUpdate()
    {
        if (!FogActivator.inside)
        {
            Timerstert += Time.deltaTime;
            if (Timerstert >= MaxTime)
            {
                //thunder.Play();
                RS.RainIntensity = Random.Range(0.1f, 1f);
                Timerstert = 0.0f;

            }
        }
    }
}
