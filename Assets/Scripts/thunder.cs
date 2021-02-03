using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thunder : MonoBehaviour
{
    [SerializeField] float timer, timer1, timer2;
    [SerializeField] AudioSource Thunder;
    private void Awake()
    {

    }
    private void Update()
    {
        timer += Time.time;
        float T = Random.Range(timer1, timer2);
        if(timer > T)
        {
            Debug.Log("1");
            Thunder.Play();
            timer = 0;
        }
    }
}
