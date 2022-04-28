using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class escape_door : MonoBehaviour
{
    [SerializeField]
    Timeline_Manager tm;
    public GameObject[] objects;
    public  bool active,over;
    public GameObject note;
    Animator anim;
    public static bool read =false;
  
    private void Start()
    {
        //false if unsaved
       active =  PlayerPrefsX.GetBool("Active");
        over = PlayerPrefsX.GetBool("Bool");
        anim = gameObject.GetComponent<Animator>();
        Activations();
    }

   
    public void Activations()
    {
        //all objects collected
        if (active && !over)
        { 
            foreach (GameObject i in objects)
            {
                i.SetActive(true);
            }
            if (!read)
            {
                note.SetActive(true);
                read = true;
            }
            anim.SetInteger("State", 1);
           
             
        }
    
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
        PlayerPrefs.Save();
    }
}
