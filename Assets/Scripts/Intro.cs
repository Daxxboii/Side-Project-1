using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class Intro : MonoBehaviour
{
    public VideoPlayer player;
    public GameObject canvas;

    private void Start()
    {
        if (PlayerPrefsX.GetBool("Saved") == false )
        {
            player.Play();
            Time.timeScale = 0;
        }
    }
    private void Update()
    {
        if (player.isPlaying)
        {
            canvas.SetActive(false);
        }
        else
        {
            canvas.SetActive(true);
            Time.timeScale = 1;
            gameObject.SetActive(false);
            
        }
    }
}
