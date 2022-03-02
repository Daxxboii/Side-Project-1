using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManagerIntroVideo : MonoBehaviour
{
	public CinemachineVirtualCamera[] cameras;
	public int Timer;
	float timer;
	int random,counter;

	private void Start()
	{
		random = Random.Range(0, cameras.Length);
		foreach(CinemachineVirtualCamera cam in cameras)
		{
          if(cam == cameras[counter])
			{
				cam.Priority++;
			}
			
			counter++;
		}
		counter = 0;
	}
	private void Update()
	{
		timer += Time.deltaTime;
		if (timer > Timer)
		{
			random = Random.Range(0, cameras.Length);
			foreach (CinemachineVirtualCamera cam in cameras)
			{
				if (cam == cameras[counter])
				{
					cam.Priority++;
				}
				
				counter++;
			}
			counter = 0;
		}
	}
}
