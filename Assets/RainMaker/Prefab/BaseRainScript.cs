//
// Rain Maker (c) 2015 Digital Ruby, LLC
// http://www.digitalruby.com
//


using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Audio;

namespace DigitalRuby.RainMaker
{
    public class BaseRainScript : MonoBehaviour
    {
        [Tooltip("Camera the rain should hover over, defaults to main camera")]
        public Camera Camera;

       

        [Tooltip("Light rain looping clip")]
        public AudioClip RainSoundLight;

        [Tooltip("Medium rain looping clip")]
        public AudioClip RainSoundMedium;

        [Tooltip("Heavy rain looping clip")]
        public AudioClip RainSoundHeavy;

        [Tooltip("AudoMixer used for the rain sound")]
        public AudioMixerGroup RainSoundAudioMixer;

        [Tooltip("Intensity of rain (0-1)")]
        [Range(0.0f, 1.0f)]
        public float RainIntensity;

        [Tooltip("Rain particle system")]
        public ParticleSystem RainFallParticleSystem;

      

      
        [Tooltip("Wind looping clip")]
        public AudioClip WindSound;

        [Tooltip("Wind sound volume modifier, use this to lower your sound if it's too loud.")]
        public float WindSoundVolumeModifier = 0.5f;

      


        protected LoopingAudioSource audioSourceRainLight;
        protected LoopingAudioSource audioSourceRainMedium;
        protected LoopingAudioSource audioSourceRainHeavy;
        protected LoopingAudioSource audioSourceRainCurrent;
        protected LoopingAudioSource audioSourceWind;
        protected Material rainMaterial;
      

        private float lastRainIntensityValue = -1.0f;

        private void UpdateWind()
        {
            audioSourceWind.Update();
        }

        private void CheckForRainChange()
        {
            if (lastRainIntensityValue != RainIntensity)
            {
                lastRainIntensityValue = RainIntensity;
                if (RainIntensity <= 0.01f)
                {
                    if (audioSourceRainCurrent != null)
                    {
                        audioSourceRainCurrent.Stop();
                        audioSourceRainCurrent = null;
                    }
                    if (RainFallParticleSystem != null)
                    {
                        ParticleSystem.EmissionModule e = RainFallParticleSystem.emission;
                        e.enabled = false;
                        RainFallParticleSystem.Stop();
                    }
                   
                }
                else
                {
                    LoopingAudioSource newSource;
                    if (RainIntensity >= 0.67f)
                    {
                        newSource = audioSourceRainHeavy;
                    }
                    else if (RainIntensity >= 0.33f)
                    {
                        newSource = audioSourceRainMedium;
                    }
                    else
                    {
                        newSource = audioSourceRainLight;
                    }
                    if (audioSourceRainCurrent != newSource)
                    {
                        if (audioSourceRainCurrent != null)
                        {
                            audioSourceRainCurrent.Stop();
                        }
                        audioSourceRainCurrent = newSource;
                        audioSourceRainCurrent.Play(1.0f);
                    }
                    if (RainFallParticleSystem != null)
                    {
                        ParticleSystem.EmissionModule e = RainFallParticleSystem.emission;
                        e.enabled = RainFallParticleSystem.GetComponent<Renderer>().enabled = true;
                        if (!RainFallParticleSystem.isPlaying)
                        {
                            RainFallParticleSystem.Play();
                        }
                        ParticleSystem.MinMaxCurve rate = e.rateOverTime;
                        rate.mode = ParticleSystemCurveMode.Constant;
                        rate.constantMin = rate.constantMax = RainFallEmissionRate();
                        e.rateOverTime = rate;
                    }
                 
                }
            }
        }

        protected virtual void Start()
        {

#if DEBUG

            if (RainFallParticleSystem == null)
            {
                Debug.LogError("Rain fall particle system must be set to a particle system");
                return;
            }

#endif

            if (Camera == null)
            {
                Camera = Camera.main;
            }

            audioSourceRainLight = new LoopingAudioSource(this, RainSoundLight, RainSoundAudioMixer);
            audioSourceRainMedium = new LoopingAudioSource(this, RainSoundMedium, RainSoundAudioMixer);
            audioSourceRainHeavy = new LoopingAudioSource(this, RainSoundHeavy, RainSoundAudioMixer);
            audioSourceWind = new LoopingAudioSource(this, WindSound, RainSoundAudioMixer);

            if (RainFallParticleSystem != null)
            {
                ParticleSystem.EmissionModule e = RainFallParticleSystem.emission;
                e.enabled = false;
                Renderer rainRenderer = RainFallParticleSystem.GetComponent<Renderer>();
                rainRenderer.enabled = false;
                rainMaterial = new Material(rainRenderer.material);
                rainMaterial.EnableKeyword("SOFTPARTICLES_OFF");
                rainRenderer.material = rainMaterial;
            }
           
        }

        protected virtual void Update()
        {

#if DEBUG

            if (RainFallParticleSystem == null)
            {
                Debug.LogError("Rain fall particle system must be set to a particle system");
                return;
            }

#endif

            CheckForRainChange();
            UpdateWind();
            audioSourceRainLight.Update();
            audioSourceRainMedium.Update();
            audioSourceRainHeavy.Update();
        }

        protected virtual float RainFallEmissionRate()
        {
            return (RainFallParticleSystem.main.maxParticles / RainFallParticleSystem.main.startLifetime.constant) * RainIntensity;
        }

       
        protected virtual bool UseRainMistSoftParticles
        {
            get
            {
                return true;
            }
        }
    }

    /// <summary>
    /// Provides an easy wrapper to looping audio sources with nice transitions for volume when starting and stopping
    /// </summary>
    public class LoopingAudioSource
    {
        public AudioSource AudioSource { get; private set; }
        public float TargetVolume { get; private set; }

        public LoopingAudioSource(MonoBehaviour script, AudioClip clip, AudioMixerGroup mixer)
        {
            AudioSource = script.gameObject.AddComponent<AudioSource>();

            if (mixer != null)
            {
                AudioSource.outputAudioMixerGroup = mixer;
            }

            AudioSource.loop = true;
            AudioSource.clip = clip;
            AudioSource.playOnAwake = false;
            AudioSource.volume = 0.0f;
            AudioSource.Stop();
            TargetVolume = 1.0f;
        }

        public void Play(float targetVolume)
        {
            if (!AudioSource.isPlaying)
            {
                AudioSource.volume = 0.0f;
                AudioSource.Play();
            }
            TargetVolume = targetVolume;
        }

        public void Stop()
        {
            TargetVolume = 0.0f;
        }

        public void Update()
        {
            if (AudioSource.isPlaying && (AudioSource.volume = Mathf.Lerp(AudioSource.volume, TargetVolume, Time.deltaTime)) == 0.0f)
            {
                AudioSource.Stop();
            }
        }
    }
}