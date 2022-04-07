using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
public class Intro : MonoBehaviour
{
    public VideoPlayer player;
    public LoadLevel load;
    bool pinged = true;
    private void Awake()
    {
            if (PlayerPrefsX.GetBool("Saved") == false || !PlayerPrefs.HasKey("Saved"))
            {
                player.Play();
            }
    }
    private void LateUpdate()
    {
        if ((player.isPlaying == false)&&player.frame>0)
        {
            if (pinged)
            {
                load.loadLevel(1);
            }
            pinged = false;
           
        }
      
    }
}
