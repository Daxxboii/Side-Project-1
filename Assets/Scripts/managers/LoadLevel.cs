using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;
public class LoadLevel : MonoBehaviour
{
    public GameObject LoadingScreen;
    public Slider slider;
    public int delay;
    public void loadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsyncronasly(sceneIndex));

    }
    IEnumerator LoadAsyncronasly( int sceneIndex)
    {
        if (sceneIndex == 1 || sceneIndex == 2)
        {
            yield return new WaitForSeconds(delay);
        }
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        LoadingScreen.SetActive(true);
      
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;

            yield return null;

        }
    }
}
