using UnityEngine;
using UnityEngine.Rendering;

public class PostProcessingManager : MonoBehaviour
{
    [SerializeField] private Volume DepthOfField;
	public static bool blur;
    public static void Blur()
	{
		blur = !blur;
	}
	private void Update()
	{
		if (blur && DepthOfField.weight != 1)
		{
			DepthOfField.weight = Mathf.Lerp(DepthOfField.weight, 1, Time.deltaTime*4);
			if (DepthOfField.weight == 1)
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
	}

}
