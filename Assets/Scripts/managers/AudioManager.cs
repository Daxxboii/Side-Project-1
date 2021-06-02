using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class AudioManager : MonoBehaviour
{
    [Header("Sources")]
    public AudioSource Footsteps;
    public AudioSource ui;
    public AudioSource Interactables;
    public AudioSource Enemy;
    public AudioSource pickable;

    [Header("Clips")]
    public AudioClip Walk_on_dirt;
    public AudioClip Button;
    public AudioClip Door_open, Door_close, Door_locked, Door_locker_open, Fence_Open, Fence_Close, Note_open, Note_close, Comic_open, Comic_close, Comic_nxt, Comic_prev, Map_take, princy_growl, princy_atack, princy_int_kill, princy_Ham_drag, princy_chase, girl_growl, girl_chase, girl_hit, girl_kill;


    [Header("Snapshots")]
    public AudioMixerSnapshot paused;
    public AudioMixerSnapshot unpaused;


    bool walking = true;

    private void Awake()
    {
        Unpaused();
    }
    public void Player_walk_on_dirt()
    {
        if (walking)
        {
            Footsteps.clip = Walk_on_dirt;
            Footsteps.Play();
            walking = false;
        }
    }
    public void Player_stop()
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

    public void Gate_Close()
    {
        Interactables.clip = Door_close;
        Interactables.Play();
    }

    public void Gate_Locked()
    {
        Interactables.clip = Door_locked;
        Interactables.Play();
    }

    public void Gate_Fence_Open()
    {
        Interactables.clip = Fence_Open;
        Interactables.Play();
    }

    public void Gate_Fence_Close()
    {
        Interactables.clip = Fence_Close;
        Interactables.Play();
    }

    public void Note_Open()
    {
        pickable.clip = Note_open;
        pickable.Play();
    }

    public void Note_Close()
    {
        pickable.clip = Note_close;
        pickable.Play();
    }

    public void Comic_Open()
    {
        pickable.clip = Comic_open;
        pickable.Play();
    }

    public void Comic_Close()
    {
        pickable.clip = Comic_close;
        pickable.Play();
    }

    public void Comic_Next()
    {
        pickable.clip = Comic_nxt;
        pickable.Play();
    }

    public void Comic_Prev()
    {
        pickable.clip = Comic_prev;
        pickable.Play();
    }

    public void Map_Take()
    {
        pickable.clip = Map_take;
        pickable.Play();
    }

    public void Enemy_Princy_Growl()
    {
        Enemy.clip = princy_growl;
        Enemy.Play();
    }

    public void Enemy_Princy_Atack()
    {
        Enemy.clip = princy_atack;
        Enemy.Play();
    }

    public void Enemy_Princy_Int_Kill()
    {
        Enemy.clip = princy_int_kill;
        Enemy.Play();
    }

    public void Enemy_Princy_Hammer()
    {
        Enemy.clip = princy_Ham_drag;
        Enemy.Play();
    }

    public void Enemy_Princy_chase()
    {
        Enemy.clip = princy_chase;
        Enemy.Play();
    }

    public void Enemy_Girl_Growl()
    {
        Enemy.clip = girl_growl;
        Enemy.Play();
    }

    public void Enemy_Girl_chase()
    {
        Enemy.clip = girl_chase;
        Enemy.Play();
    }

    public void Enemy_Girl_hit()
    {
        Enemy.clip = girl_hit;
        Enemy.Play();
    }

    public void Enemy_Girl_kill()
    {
        Enemy.clip = girl_kill;
        Enemy.Play();
    }
}
