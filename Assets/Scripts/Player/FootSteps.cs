using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
	public AudioManager AudioManager;
	private void OnCollisionEnter(Collision other)
	{
		//Ground
		if (other.gameObject.layer == 6)
		{
			AudioManager.Player_walk(1);
		}
		//Tiles
		else if (other.gameObject.layer == 3)
		{
			AudioManager.Player_walk(2);
		}
		//Wood
		else
		{
			AudioManager.Player_walk(0);
		}

	}
}
