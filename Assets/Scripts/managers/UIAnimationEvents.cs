using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimationEvents : MonoBehaviour
{
    [Header("UI Animations")]
    public RectTransform Top;
	public RectTransform Bottom;
	public bool show_bars;
	private Vector2 target;
	public float speed,Camera_speed;
	public Camera Timeline_camera;

	private void Start()
	{
		target = new Vector2(0f, 50f);
	}
	public void Show()
	{
		show_bars = !show_bars;
	}
	private void Update()
	{
		if (show_bars )
		{
			Top.sizeDelta =  Vector2.Lerp(Top.sizeDelta,target,Time.deltaTime*speed);
			Bottom.sizeDelta = Vector2.Lerp(Bottom.sizeDelta, target, Time.deltaTime*speed);
			//Timeline_camera.fieldOfView = Mathf.Lerp(Timeline_camera.fieldOfView, 90f, Time.deltaTime * Camera_speed);
		}
		else if (!show_bars && Top.sizeDelta != Vector2.zero)
		{
			Top.sizeDelta = Vector2.Lerp(Top.sizeDelta, Vector2.zero, Time.deltaTime*speed);
			Bottom.sizeDelta = Vector2.Lerp(Bottom.sizeDelta, Vector2.zero, Time.deltaTime*speed);
			//Timeline_camera.fieldOfView = Mathf.Lerp(Timeline_camera.fieldOfView, 80f, Time.deltaTime * Camera_speed);
		}
	}

}
