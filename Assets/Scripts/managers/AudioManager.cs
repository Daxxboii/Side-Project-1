using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public  class AudioManager : MonoBehaviour
{
    [Header("Sources")]
    public  AudioSource Footsteps;
    public AudioSource ui;
    public AudioSource Interactables;


    [Header("Clips")]
    public  AudioClip Walk_on_dirt;
    public AudioClip Button;
    public AudioClip Door_open, Door_locked;


    [Header("Snapshots")]
    public AudioMixerSnapshot paused;
    public AudioMixerSnapshot unpaused;
   

    bool walking =true;

    private void Awake()
    {
        Unpaused();
    }
    public  void Player_walk_on_dirt()
    {
        if (walking)
        {
            Footsteps.clip = Walk_on_dirt;
            Footsteps.Play();
            walking = false;
        }
    }
    public  void Player_stop()
    {
        Footsteps.Stop();
        walking = true;
    }

    public void UI()
    {
        ui.clip = Button;
        ui.Play();
    }

    public void Paused()
    {
        paused.TransitionTo(0f);
    }

    public void Unpaused()
    {
        unpaused.TransitionTo(0f);
    }

    public void Gate_Open()
    {
        Interactables.clip = Door_open;
        Interactables.Play();
    }

    public void Gate_Locked()
    {
        Interactables.clip = Door_locked;
        Interactables.Play();
    }
}
