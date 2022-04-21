using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.RainMaker;


public class FootSteps : MonoBehaviour
{
	public static bool inside;
	public AudioManager AudioManager;
	public RainScript rain;
	public PlayerScript ps;
	private void OnCollisionEnter(Collision other)
	{
		//Ground
		if (other.gameObject.layer == 6)
		{
            if (!ps.isCrouching)
            {
             AudioManager.Player_walk(1);
			 RandomRain();
			 inside = false;
            }
			
		}
		//Tiles
		else if (other.gameObject.layer == 3)
		{
			AudioManager.Player_walk(2);
			rain.RainIntensity = 0;
			inside = true;
		}
		//Wood
		else
		{
			AudioManager.Player_walk(0);
			rain.RainIntensity = 0;
			inside = false;
		}

	}

	void RandomRain()
	{
		rain.RainIntensity = Random.Range(0.1f, 0.5f);
	}

}