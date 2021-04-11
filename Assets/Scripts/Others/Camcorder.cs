using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Camcorder : MonoBehaviour
{

   private float time;
    public TextMeshProUGUI timeText;
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
