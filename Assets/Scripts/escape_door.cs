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
        switch (active)
        {
            //all objects collected
            case true when !over:
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
                break;
            }
            case true when over:
                anim.SetInteger("State", 2);
                break;
            default:
            {
                note.SetActive(false);
                foreach (GameObject i in objects)
                {
                    i.SetActive(false);
                }

                break;
            }
        }
        PlayerPrefsX.SetBool("Active",active);
        PlayerPrefsX.SetBool("Over", over);
        PlayerPrefs.Save();
    }
}
