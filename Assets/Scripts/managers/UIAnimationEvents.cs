using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class UIAnimationEvents : MonoBehaviour
{
    [Header("UI Animations")]
    public RectTransform Top;
	public RectTransform Bottom;
	public bool show_bars;
	private Vector2 target;
	public Camera Timeline_camera;

	private void Start()
	{
		target = new Vector2(0f, 50f);
	}
	public void Show()
	{
		show_bars = !show_bars;
		Change();
	}

	void Change()
	{
		if (show_bars)
		{
			Top.DOSizeDelta(target, 1).SetEase(Ease.InSine);
			Bottom.DOSizeDelta(target, 1).SetEase(Ease.InSine);
			//Timeline_camera.fieldOfView = Mathf.Lerp(Timeline_camera.fieldOfView, 90f, Time.deltaTime * Camera_speed);
		}
		else if (!show_bars)
		{
			Top.DOSizeDelta(Vector2.zero, 1).SetEase(Ease.OutSine);
			Bottom.DOSizeDelta(Vector2.zero, 1).SetEase(Ease.OutSine);
			//Timeline_camera.fieldOfView = Mathf.Lerp(Timeline_camera.fieldOfView, 80f, Time.deltaTime * Camera_speed);
		}
	}
	

}
