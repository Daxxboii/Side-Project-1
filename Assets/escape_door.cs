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
    public bool over;
    Animator anim;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
      if(active && !over)
        {
            anim.SetInteger("State", 1);
            foreach (GameObject i in objects)
            {
                i.SetActive(true);
            }

        }

      else if(active && over){
            anim.SetInteger("State", 2);
        }

        else
        {
            foreach(GameObject i in objects)
            {
                i.SetActive(false);
            }
        }
    }
}
