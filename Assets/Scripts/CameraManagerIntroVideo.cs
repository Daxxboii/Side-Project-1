using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Cinemachine;

public class CameraManagerIntroVideo : MonoBehaviour
{
	public Animator thunder;
	[Serializable]
	public struct cams
	{
		public CinemachineVirtualCamera camera;
		[Range(0f,30f)]public float Screen_time;
	}
	
	public GameObject[] wheels;
	float timer;
	public float wheel_speed;
	int counter;
	[SerializeField]public cams[] Camera_set;

	public List<CinemachineVirtualCamera> all_cameras;

	private void Start()
	{
		all_cameras.Clear();
		
		foreach(cams c in Camera_set)
		{
			all_cameras.Add(c.camera);
			c.camera.Priority = 0;
			
		}
		
		foreach (CinemachineVirtualCamera c in all_cameras)
		{
			if (c == Camera_set[counter].camera)
			{
				c.Priority = 1;
			}
			else
			{
				c.Priority = 0;
			}
		}
	}
	private void Update()
	{
		foreach(GameObject wheel in wheels)
		{
			wheel.transform.Rotate(transform.right * wheel_speed * Time.deltaTime);
		}

		timer += Time.deltaTime;
		if (timer > Camera_set[counter].Screen_time)
		{
			counter++;
			foreach (CinemachineVirtualCamera c in all_cameras)
			{
				if (c == Camera_set[counter].camera)
				{
					c.Priority = 1;
				}
				else
				{
					c.Priority = 0;
				}
			}
			
			timer = 0;
		}

		
		
	}
}
