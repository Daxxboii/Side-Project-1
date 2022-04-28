using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.RainMaker;

public class Headbob : MonoBehaviour
{
    public static bool inside;
    public AudioManager AudioManager;
    public RainScript rain;
    public PlayerScript ps;

    public float walkingBobbingSpeed = 14f;
    public float bobbingAmount = 0.05f;
    public PlayerScript controller;
    public GameObject Camera_transform;
    [Range(0f, 1f)] public float Time_between_Footsteps;
    
    float defaultPosY = 0;
    float timer = 0,footstepTimer;

    // Start is called before the first frame update
    void Start()
    {
        defaultPosY = Camera_transform.transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(controller.x) > 0.1f || Mathf.Abs(controller.z) > 0.1f)
        {
            footstepTimer += Time.deltaTime;
            if (footstepTimer > Time_between_Footsteps)
            {
                MakeFootstepsSound();
                footstepTimer = 0f;
            }
            //Player is moving
            timer += Time.deltaTime * walkingBobbingSpeed;
            Camera_transform.transform.localPosition = new Vector3(Camera_transform.transform.localPosition.x, defaultPosY + Mathf.Sin(timer) * bobbingAmount, Camera_transform.transform.localPosition.z);
            
        }
        else
        {
            //Idle
            timer = 0;
            Camera_transform.transform.localPosition = new Vector3(Camera_transform.transform.localPosition.x, Mathf.Lerp(Camera_transform.transform.localPosition.y, defaultPosY, Time.deltaTime * walkingBobbingSpeed), Camera_transform.transform.localPosition.z);
        }
    }

    void MakeFootstepsSound()
    {
        AnimationEvents.FootStep();
      
    }
    private void OnCollisionEnter(Collision other)
    {
        //Ground
        if (other.gameObject.layer == 6)
        {
            if (!ps.isCrouching)
            {
                AudioManager.Player_walk(1);
                RandomRain();
                inside = false;
            }

        }
        //Tiles
        else if (other.gameObject.layer == 3)
        {
            AudioManager.Player_walk(2);
            rain.RainIntensity = 0;
            inside = true;
        }
        //Wood
        else
        {
            AudioManager.Player_walk(0);
            rain.RainIntensity = 0;
            inside = false;
        }

    }

    void RandomRain()
    {
        rain.RainIntensity = Random.Range(0.1f, 0.5f);
    }
}