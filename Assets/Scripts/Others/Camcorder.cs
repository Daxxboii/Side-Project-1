using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class Camcorder : MonoBehaviour
{

   public float time;
    float minutes, seconds;
    public TextMeshProUGUI timeText;
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
       // TimeSpan currentTime = TimeSpan.FromMinutes
         minutes = Mathf.FloorToInt(time / 60);
         seconds = Mathf.FloorToInt(time % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
