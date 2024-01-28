using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

using UnityEngine.UI;
public class LoadLevel : MonoBehaviour
{
    public GameObject LoadingScreen;
    public Slider slider;
    public int delay;
    public VideoPlayer player;

	private void Awake()
	{
        player.Prepare();
	}
	public void loadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsync(sceneIndex));
    }
    private IEnumerator LoadAsync( int sceneIndex)
    {
        yield return new WaitForSeconds(delay);

        var operation = SceneManager.LoadSceneAsync(sceneIndex);
        LoadingScreen.SetActive(true);
     
        while (!operation.isDone)
        {
            var progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            yield return null;
        }
    }
   public void Skip()
    {
        player.Pause();
    }
}
