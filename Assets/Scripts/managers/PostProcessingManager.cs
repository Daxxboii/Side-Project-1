using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using DG.Tweening;
public class PostProcessingManager : MonoBehaviour
{
    [SerializeField] private PostProcessVolume DepthOfField;
	[SerializeField] public float duration;

	//public static bool blur;
	//public static bool pauseafterblur=true;
  
	/*private void Update()
	{
		if (blur)
		{
			DepthOfField.weight = Mathf.Lerp(DepthOfField.weight, 1, Time.deltaTime*4);
			
			if (DepthOfField.weight >= 0.9&&pauseafterblur)
			{
				Time.timeScale = 0;
			}
		}
		else if (!blur)
		{
			if (DepthOfField.weight > 0)
			{
				Time.timeScale = 1;
				DepthOfField.weight = Mathf.Lerp(DepthOfField.weight, 0, Time.deltaTime*4);
			}
		}
	}*/

	public void Blur()
	{
		DOTween.KillAll();
		DOVirtual.Float(0, 1, duration, v =>
		{
			DepthOfField.weight = v;
		}).SetEase(Ease.InSine).OnComplete(() =>{ Time.timeScale = 0f; }); 
	
	}
	public void UnBlur()
	{
		DOTween.KillAll();
		DOVirtual.Float(1, 0, duration, v =>
		{
			DepthOfField.weight = v;
		}).SetEase(Ease.OutSine).OnComplete(() => { Time.timeScale = 1f; }).SetUpdate(true);
	}

}
