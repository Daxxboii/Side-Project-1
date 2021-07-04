﻿using Scripts.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Scripts.Timeline;

namespace Scripts.Buttons
{
    public class ButtonOpen : MonoBehaviour
    {
        [SerializeField]
        GameObject Player, Hide, Unhide, Intract, Pickup, Drop, Timeline,Error_comment,note,save,Comic,map;
        escape_door ed = null;
        TextMeshProUGUI comment;
        [SerializeField]
        Camera cam;
        [SerializeField]
        LayerMask lm;
        [SerializeField]
        ObjectController oc;
        [SerializeField]
        Timeline_Manager tm;
        GameObject pointer;

        public LayerMask interactables;

        public RaycastHit hit;
        void Awake()
        {
            // Player = GameObject.FindWithTag("Player");
            Pickup.SetActive(false);
            map.SetActive(false);
            Intract.SetActive(false);
            Hide.SetActive(false);
            Unhide.SetActive(false);
            Drop.SetActive(false);
            Comic.SetActive(false);
            note.SetActive(false);
            Pickup.SetActive(false);
            Timeline.SetActive(false);
            save.SetActive(false);
            Error_comment.SetActive(true);
            comment = Error_comment.GetComponent<TextMeshProUGUI>();
        }

     
        void Update()
        {

            if (Player.activeInHierarchy == true && Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 6f, lm))
            {
              
                    if (hit.transform.CompareTag("Door") || hit.transform.CompareTag("Escape Door") || hit.transform.CompareTag("Door_locker") || hit.transform.CompareTag("Door_fence"))
                    {
                        Intract.SetActive(true);
                    Track(Intract);
                    }


                    if (hit.transform.CompareTag("Note"))
                    {
                        note.SetActive(true);
                    Track(note);

                    }
                    if (hit.transform.CompareTag("Save"))
                    {
                        save.SetActive(true);
                    Track(note);

                    }
                    if (hit.transform.CompareTag("Hideable"))
                    {
                        Hide.SetActive(true);
                        Unhide.SetActive(false);
                     

                    }
                    if (hit.transform.CompareTag("pickup") || hit.transform.CompareTag("Doll") || hit.transform.CompareTag("Fingie"))
                    {
                        Pickup.SetActive(true);
                    Track(Pickup);


                    }
                    if (hit.transform.CompareTag("Comic"))
                    {
                        Comic.SetActive(true);
                    Track(Comic);

                    }

                    if (hit.transform.CompareTag("Map"))
                    {
                        map.SetActive(true);
                        comment.text = "Should take it , would be of help later";
                    Track(map);
                    }

                    if ((hit.transform.CompareTag("Timeline") && oc.had != null) || hit.transform.CompareTag("Timeline_independent"))
                    {
                        Timeline.SetActive(true);
                        comment.text = "Interact?";
                    Track(Timeline);
                    }

                    if ((hit.transform.CompareTag("Timeline") && oc.had == null))
                    {
                        comment.text = "get something ";
                    }

                    if (hit.transform.CompareTag("Obstacle"))
                    {
                        comment.text = "Going there wont be a good idea";
                    }
                    if (hit.transform.CompareTag("Blackboard"))
                    {
                        comment.text = "Find something to write with";
                    }
                    if (hit.transform.CompareTag("Untagged"))
                    {
                        Intract.SetActive(false);
                        Pickup.SetActive(false);
                        Drop.SetActive(false);
                        Unhide.SetActive(false);
                        save.SetActive(false);
                        Hide.SetActive(false);
                        Timeline.SetActive(false);
                        note.SetActive(false);
                        Comic.SetActive(false);
                        map.SetActive(false);
                        comment.text = "";
                    }

                
            }
            else
            {
                Intract.SetActive(false);
                Pickup.SetActive(false);
                Drop.SetActive(false);
                Unhide.SetActive(false);
                save.SetActive(false);
                Hide.SetActive(false);
                Timeline.SetActive(false);
                note.SetActive(false);
                Comic.SetActive(false);
                map.SetActive(false);
                comment.text = "";

            /*    if (oc.had != null)
                {
                    Drop.SetActive(true);
                }*/
              /*  if (Player.activeInHierarchy == true)
                {
                    Unhide.SetActive(false);
                }
                else
                {
                    Unhide.SetActive(true);
                } */
            }

        }
      public   void close()
        {
            Intract.SetActive(false);
            Pickup.SetActive(false);
            Drop.SetActive(false);
            Unhide.SetActive(false);
            save.SetActive(false);
            Hide.SetActive(false);
            Timeline.SetActive(false);
            note.SetActive(false);
            Comic.SetActive(false);
            map.SetActive(false);
            comment.text = "";
        }
        void Track(GameObject Button)
        {
            foreach (Transform child in hit.transform)
            {
                if (child.tag == "Tracker")
                {
                     pointer = child.gameObject;
                }
                   
            }
            Vector3 pos = Camera.main.WorldToScreenPoint(pointer.transform.position);
            Button.transform.position = pos;
          
            
        }
       
    }
}