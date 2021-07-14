using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Scripts.Timeline;
public class AudioManager : MonoBehaviour
{
    public Timeline_Manager tm;
    public float Environment_sounds_timer;
    [Header("Sources")]
    public AudioSource Footsteps;
    public AudioSource ui;
    public static AudioSource Interactables;

    public AudioSource pickable;
    public AudioSource Environment;
    [Header("Enemy")]
    public AudioSource principal;
    public AudioSource hammer;
    public AudioSource girl;

    [Header("Clips")]
    public AudioClip Walk_on_dirt;
    public AudioClip  Walk_on_Planks;
    [Space(30)]
    [Space(10)]
    public AudioClip Button;

    [Space(10)]
    public AudioClip Door_open, Door_locked, Door_locker_open, Fence_Open;

    [Space(10)]
    public AudioClip  Note_open, Comic_open, Comic_close, Comic_nxt, Comic_prev, Map_take;

    [Space(10)]
    public AudioClip  princy_growl, princy_atack, princy_int_kill, princy_Ham_drag, princy_chase, girl_chase,  girl_kill;

    [Space(10)]
    public AudioClip[] environment_sounds;

    [Header("Snapshots")]
    public AudioMixerSnapshot paused;
    public AudioMixerSnapshot unpaused;

    public Animator thunder;
    bool crying;
    bool walking = true;
    float timer;
    int index,_index;

    private void Awake()
    {
        Unpaused();
    }

    private void Update()
    {
        timer += Time.deltaTime;
       
        if (timer > Environment_sounds_timer)
        {
            Random_timer();
           
        }
        
    }
    //player
    public void Player_walk()
    {
        if (walking)
        {
            if (FogActivator.inside)
            {
                Footsteps.clip = Walk_on_Planks;
            }
            else
            {
                Footsteps.clip = Walk_on_dirt;
            }
            Footsteps.Play();
            walking = false;
        }
    }
    public void Player_stop()
    {
        Footsteps.Stop();
        walking = true;
    }
    //UI
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

    //Gates
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
    public void Locker_Gate_Open()
    {
        Interactables.clip = Door_locker_open;
        Interactables.Play();
    }

    public void Gate_Fence_Open()
    {
        Interactables.clip = Fence_Open;
        Interactables.Play();
    }

    //notes and comics
    public void Note_Open()
    {
        pickable.clip = Note_open;
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

    //Enemies
    public void Enemy_Princy_Growl()
    {
        principal.clip = princy_growl;
        principal.Play();
    }

    public void Enemy_Princy_Atack()
    {
        hammer.clip = princy_atack;
        hammer.Play();
    }

    public void Enemy_Princy_Int_Kill()
    {
        principal.clip = princy_int_kill;
        principal.Play();
    }

    public void Enemy_Princy_Hammer()
    {
        hammer.clip = princy_Ham_drag;
        hammer.Play();
    }

    public void Enemy_Princy_chase()
    {
        principal.clip = princy_chase;
        principal.Play();
    }

  

    public void Enemy_Girl_chase()
    {
        girl.clip = girl_chase;
        girl.Play();
    }
    public void Enemy_Girl_kill()
    {
        girl.clip = girl_kill;
        girl.Play();
        
    }

    public void Girl_Stop()
    {
        girl.Pause();
    }
    //Environment
    public void Random_Sounds()
    {
        if (FogActivator.inside && index>2)
        {
            Environment.clip = environment_sounds[index];
            Environment.Play();
        }
       else if (!FogActivator.inside && index > 2)
        {
            Random_timer();
        }

        else {
            if (index == 1)
            {
                thunder.SetTrigger("Open");
                thunder.ResetTrigger("open");
            }
            Environment.clip = environment_sounds[index];
            Environment.Play();
        }
       
      
    }
    public void Random_timer()
    {
        index = Random.Range(0, environment_sounds.Length);
        if (index != _index )
        {
            if(((tm.Current_cutscene < 3) && index > 12))
            {
                Random_Sounds();
            }
           
        }
        _index = index;
        timer = 0;
    }
}
