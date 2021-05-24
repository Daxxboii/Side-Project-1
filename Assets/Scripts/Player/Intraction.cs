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
        Animator map_anim;
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
        GameObject Note_panel,map,minimap,fps_canvas;
        [Serializable]
        private struct IntractionSettings
        {
            public float range, CloseTime;
            public LayerMask Intractable;
            public Animator anim;
            public bool IsOpened;
        }
        RaycastHit hit;
        [SerializeField]
        TextMeshProUGUI error_comment;
        public GameObject Ring_Box;
        private void Start()
        {
            Note_panel.SetActive(false);
        }

        public void PlayerInteraction()
        {
      
            if (Physics.Raycast(FPScam.transform.position, FPScam.transform.forward, out hit, it.range, it.Intractable))
            {
                it.anim = hit.transform.GetComponentInParent<Animator>();
                if (hit.collider.tag == "Door")
                {
                    IntractWithDoor();
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
                    minimap.SetActive(true);
                    map_anim.SetBool("Open", true);
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

      /*  public void Comic()
        {
            Debug.Log(hit.transform.gameObject.name);
            hit.transform.gameObject.SetActive(false);
        }*/
    }
}
