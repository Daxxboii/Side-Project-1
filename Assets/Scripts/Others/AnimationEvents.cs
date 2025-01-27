using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Enemy.girlHostile;

public class AnimationEvents : MonoBehaviour
{
    public delegate void Kill();
    public static event Kill kill;

    public GirlAiGhost girlAIGhost;
    public AudioManager AudioManager;
    public PlayerScript playerScript;
    public PostProcessingManager postProcessingManager;

    public delegate void MakeSound();
    public static event MakeSound Door_open;
    public static event MakeSound Door_Locked;
    public static event MakeSound Locker_Door;
    public static event MakeSound Fence_door;
    public static event MakeSound thunder;
    public static event MakeSound girl_spook;
    public static event MakeSound girl_crack;
    public static event MakeSound Footstep;

    private int random_int;
    private int index;
    public void Invoke_Death()
    {
        kill.Invoke();
    }

    public void MakeGirlCalm()
    {
        girlAIGhost.Animations(0, 0);
    }

    public static void FootStep()
	{
        Footstep.Invoke();
	}
	#region Doors
	public void Wooden_Door_Open()
	{
        Door_open.Invoke();
	}

    public void Rattle_Door()
	{
        Door_Locked.Invoke();
	}

    public void Locker_Door_open()
	{
        Locker_Door.Invoke();
	}

    public void Fence_door_open()
	{
        Fence_door.Invoke();
	}
	#endregion
	public void Thunder_Sound()
	{
        thunder.Invoke();
	}

    //PickRandom
    public void PickRandom(int chances)
	{
        random_int = Random.Range(0, 30);
		if (random_int < chances)
		{
            girl_spook.Invoke();
		}
	}

    public void TakeDamage(float damage)
	{
        playerScript.PlayerTakeDamage(damage);
      //  Debug.Log(gameObject.name);
	}
    public void Save()
	{
        index++;
		if (index > 3)
		{
            postProcessingManager.UnBlur();
            index = 0;
		}
	}
    public void Crack()
	{
        girl_crack.Invoke();
	}

    public void Spotted()
    {

    }

    public void Chasing()
    {
        PlayerScript.is_Being_Chased = true;
    }

    public void StopChasing()
    {
        PlayerScript.is_Being_Chased = false;
    }
}
