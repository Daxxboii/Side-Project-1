using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManagerIntroVideo : MonoBehaviour
{
	public Animator thunder;
	public CinemachineVirtualCamera[] cameras;
	public GameObject[] wheels;
	public int Timer;
	float timer;
	public float wheel_speed;
	int random,counter;

	private void Start()
	{
		random = Random.Range(0, cameras.Length);
		foreach (CinemachineVirtualCamera cam in cameras)
		{
			if (cam == cameras[random])
			{
				cam.Priority=1;
			}
			else
			{
				cam.Priority=0;
			}

			counter++;
		}
		counter = 0;
	}
	private void Update()
	{
		foreach(GameObject wheel in wheels)
		{
			wheel.transform.Rotate(transform.right * wheel_speed * Time.deltaTime);
		}

		timer += Time.deltaTime;
		

		if (timer > Timer)
		{
			random = Random.Range(0, cameras.Length);
			foreach (CinemachineVirtualCamera cam in cameras)
			{
				if (cam == cameras[random])
				{
					cam.Priority=1;
					thunder.SetTrigger("Open");
				//	thunder.ResetTrigger("Open");
				}
				else
				{
					cam.Priority=0;
				}
				
				counter++;
			}
			counter = 0;
			timer = 0;
		}
		
	}
}
