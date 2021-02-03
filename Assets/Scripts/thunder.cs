using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thunder : MonoBehaviour
{
    [SerializeField] float timer, timer1, timer2;
    [SerializeField] AudioSource Thunder;
    [SerializeField] GameObject Tlight;
    private void Awake()
    {

    }
    private void Update()
    {
        timer += Time.time;
        float T = Random.Range(timer1, timer2);
        if (timer > T)
        {
            if (Time.time > 0.3)
            {
                Tlight.SetActive(true);
            }
            Debug.Log("1");
            Thunder.Play();

            timer = 0;
            Tlight.SetActive(false);
        }
        
    }
}
