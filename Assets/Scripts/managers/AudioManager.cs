using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class AudioManager : MonoBehaviour
{
    public Timeline_Manager tm;
    [Range(0f,60f)]public float Environment_sounds_timer;
    public GameObject player;

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

    [Header("Footsteps")]
    public AudioClip Walk_on_dirt;
    public AudioClip  Walk_on_Planks;
    public AudioClip Walk_on_tiles;

   
    public AudioClip Button;

    [Header("Doors")]
    public AudioClip Door_open;
    public AudioClip Door_locked;
    public AudioClip Door_locker_open;
    public AudioClip Fence_Open;


    [Header("Notes")]
    public AudioClip  Note_open;
    public AudioClip Comic_open;
    public AudioClip Comic_close;
    public AudioClip Comic_nxt;
    public AudioClip Comic_prev;
    public AudioClip Map_take;

    [Header("Ghosts")]
    public AudioClip princy_growl;
    public AudioClip princy_atack;
    public AudioClip princy_int_kill;
    public AudioClip princy_Ham_drag;
    public AudioClip princy_chase;
    public AudioClip girl_chase;
    public AudioClip girl_chase2;
    public AudioClip girl_kill;
    public AudioClip girl_bone_crack;
    public AudioClip spotted;

    public AudioClip[] environment_sounds;

    [Header("Snapshots")]
    public AudioMixerSnapshot paused;
    public AudioMixerSnapshot unpaused;

    public Animator thunder;
    bool crying;
  
    float timer;
    int index;

    private void Awake()
    {
        Unpaused();
        AnimationEvents.Door_open += Gate_Open;
        AnimationEvents.Door_Locked += Gate_Locked;
        AnimationEvents.Locker_Door += Locker_Gate_Open;
        AnimationEvents.Fence_door += Gate_Fence_Open;
        AnimationEvents.thunder += Play_Thunder;
        AnimationEvents.girl_spook += Girl_Spook;
        AnimationEvents.girl_crack += Enemy_Girl_Bones;
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
    public void Player_walk(int layer)
    {
            if (layer == 0)
            {
            Footsteps.clip = Walk_on_Planks;
            }
            else if (layer==1)
            {
                Footsteps.clip = Walk_on_dirt;
           
            }
	    	else
		    {
            Footsteps.clip = Walk_on_tiles;
           
            }
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
    public void Enemy_Girl_Bones()
	{
        girl.clip = girl_bone_crack;
        girl.Play();
	}

    public void Girl_Stop()
    {
        girl.Pause();
    }

    public void Girl_Spook()
	{
        girl.clip = girl_chase2;
        girl.Play();
    }

    public void Girl_Spotted()
    {
        girl.clip = spotted;
        girl.Play();
    }
    //Environment
    private void Random_Sounds()
    {
        if (index == 1)
        {
            thunder.SetTrigger("Open");
        }
        Environment.clip = environment_sounds[index];
		if (player.activeInHierarchy)
		{
            Environment.Play();
        }

    }

   
    public void Random_timer()
    {
        if (tm.Current_cutscene > 3 && PlayerScript.is_Being_Chased==false)
        {
            index = Random.Range(0, environment_sounds.Length);
            if (((tm.Current_cutscene < 3) && index == 13))
            {
                Random_timer();
            }
            else
            {
                Random_Sounds();
            }
        }
        timer = 0;
    }
    private void Play_Thunder()
	{
        Environment.clip = environment_sounds[1];
        Environment.Play();
    }
}
