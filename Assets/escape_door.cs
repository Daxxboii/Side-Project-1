using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Timeline;
public class escape_door : MonoBehaviour
{
    [SerializeField]
    Timeline_Manager tm;
    public GameObject[] objects;
    public bool active;
    public GameObject finger;
    public bool over;
    Animator anim;
    public GameObject note;
    private void Awake()
    {
       active =  PlayerPrefsX.GetBool("Active");
        over = PlayerPrefsX.GetBool("Bool");
        anim = gameObject.GetComponent<Animator>();
        Activations();
    }
    public void Activations()
    {
        //all objects collected
      if(active && !over)
        {
            note.SetActive(true);
            anim.SetInteger("State", 1);
            foreach (GameObject i in objects)
            {
                i.SetActive(true);
            }
         
        }
      //even finger collected
      else if(active && over){
            anim.SetInteger("State", 2);
        }

        else
        {
            note.SetActive(false);
            foreach (GameObject i in objects)
            {
                i.SetActive(false);
            }
        }
        PlayerPrefsX.SetBool("Active",active);
        PlayerPrefsX.SetBool("Over", over);
    }
}
