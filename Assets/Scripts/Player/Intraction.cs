using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Scripts.Timeline;
using Scripts.Objects;

namespace Scripts.Player
{
    public class Intraction : MonoBehaviour
    {
      
        [SerializeField]
        GameObject minimap_button;
        [SerializeField]
        ObjectController oc;
        [SerializeField]
        private TextMeshProUGUI note_text;
        [SerializeField]
        IntractionSettings it = new IntractionSettings();
        [SerializeField]
        Camera FPScam;
        Note_manager nm = null;
        escape_door ed = null;
        [SerializeField]
        Timeline_Manager tm;
        [SerializeField]
        GameObject Note_panel,map,fps_canvas;
        [Serializable]
        private struct IntractionSettings
        {
            public float range, CloseTime;
            public LayerMask Intractable;
            public Animator anim;
            public bool IsOpened;
          
        }
        private bool Open;
        RaycastHit hit;
        [SerializeField]
        TextMeshProUGUI error_comment;
        public GameObject Ring_Box;
        public AudioManager audio;
        private void Start()
        {
            Note_panel.SetActive(false);
        }

        public void PlayerInteraction()
        {
      
            if (Physics.Raycast(FPScam.transform.position, FPScam.transform.forward, out hit, it.range, it.Intractable))
            {
                it.anim = hit.transform.GetComponentInParent<Animator>();
                if (hit.collider.tag == "Door" || hit.transform.CompareTag("Door_locker") || hit.transform.CompareTag("Door_fence"))
                {
                    AudioManager.Interactables = hit.transform.GetComponentInParent<AudioSource>();
                    IntractWithDoor();
                    if(hit.collider.tag == "Door")
                    {
                        Door_Wooden();
                     
                    }
                    else if(hit.collider.tag == "Door_locker")
                    {
                        Door_Locker();
                    }
                    else if(hit.collider.tag == "Door_fence")
                    {
                        Door_Fence();
                    }
                }
                if (hit.transform.CompareTag("Escape Door"))
                {
                    ed = hit.transform.GetComponent<escape_door>();
                    error_comment.text = "";
                    if (tm.Current_cutscene == 12)
                    {
                        ed.active = true;
                        if (oc.had != null)
                        {
                            ed.over = true;
                        }
                    }
                    else
                    {
                        error_comment.text = "Get all the 5 objects listed to open the door";
                    }
                    ed.Activations();
                }
                else if (hit.collider.tag == "Note")
                {
                  
                    nm = hit.transform.GetComponent<Note_manager>();
                
                    Note_Open();
                    hit.transform.gameObject.SetActive(false);
                  foreach(GameObject i in nm.objects)
                    {
                        i.SetActive(true);
                    }
                    if (nm.objective)
                    {
                        tm.ObjectiveList();
                    }
                }
                else if(hit.collider.tag == "Map")
                {
                    map.SetActive(false);
                    minimap_button.SetActive(true);
                  
                    tm.ObjectiveList();
                    Ring_Box.SetActive(true);
                }
            }
        }
      void IntractWithDoor()
        {
          
            if (it.anim.GetBool("IsOpen") == false)
            {
               
                it.anim.SetBool("IsOpen", true);
                StartCoroutine(closeDoor());
            }

        }

        IEnumerator closeDoor()
        {
            yield return new WaitForSeconds(it.CloseTime);
            it.anim.SetBool("IsOpen", false);
            Open = false;
        }

       void Note_Open()
        {
            note_text.text = nm.text;
            Note_panel.SetActive(true);
            fps_canvas.SetActive(false);
            Time.timeScale = 0f;
        }

        public void Note_Close()
        {
            Note_panel.SetActive(false);
            fps_canvas.SetActive(true);
            Time.timeScale = 1f;
        }
     
      void Door_Wooden()
      {
            if (it.anim.GetBool("IsOpen") == true||!Open)
            {
                if (it.anim.runtimeAnimatorController.name == "Door_unwrapped")
                {
                    audio.Gate_Open();
                }
                else
                {
                    audio.Gate_Locked();
                }
                Open = true;
            }
      }
       void Door_Fence()
        {
            audio.Gate_Fence_Open();
        }
        void Door_Locker()
        {
            audio.Locker_Gate_Open();
        }
    }
}
