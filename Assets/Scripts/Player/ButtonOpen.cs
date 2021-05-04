using Scripts.Objects;
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
                if (hit.transform.CompareTag("Door") || hit.transform.CompareTag("Escape Door"))
                {
                    Intract.SetActive(true);

                }

               
                if (hit.transform.CompareTag("Note"))
                {
                    note.SetActive(true);

                }
                if (hit.transform.CompareTag("Save"))
                {
                    save.SetActive(true);

                }
                if (hit.transform.CompareTag("Hideable"))
                {
                    Hide.SetActive(true);
                    Unhide.SetActive(false);

                }
                if (hit.transform.CompareTag("pickup") || hit.transform.CompareTag("Doll"))
                {
                    Pickup.SetActive(true);
                    
                   
                }
                if (hit.transform.CompareTag("Comic"))
                {
                    Comic.SetActive(true);
                  
                }

                if (hit.transform.CompareTag("Map"))
                {
                    map.SetActive(true);
                   comment.text = "Should take it , would be of help later";
                }

                if ((hit.transform.CompareTag("Timeline") && oc.had != null) || hit.transform.CompareTag("Timeline_independent"))
                {
                    Timeline.SetActive(true);
                 

                }
                 
                else if ((hit.transform.CompareTag("Timeline") && oc.had == null))
                {
                    comment.text = "I need a tool";
                }

                if (hit.transform.CompareTag("Obstacle"))
                {
                    comment.text = "Going there wont be a good idea";
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
       
    }
}