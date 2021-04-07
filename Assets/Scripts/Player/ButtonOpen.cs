using Scripts.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

namespace Scripts.Buttons
{
    public class ButtonOpen : MonoBehaviour
    {
        [SerializeField]
        GameObject Player, Hide, Unhide, Intract, Pickup, Drop, Timeline,Error_comment;

        TextMeshProUGUI comment;
        [SerializeField]
        Camera cam;
        [SerializeField]
        LayerMask lm;
        [SerializeField]
        ObjectController oc;

      

        public RaycastHit hit;
        void Awake()
        {
            // Player = GameObject.FindWithTag("Player");
            Pickup.SetActive(false);
            Intract.SetActive(false);
            Hide.SetActive(false);
            Unhide.SetActive(false);
            Drop.SetActive(false);
            Pickup.SetActive(false);
            Timeline.SetActive(false);
            Error_comment.SetActive(true);
            comment = Error_comment.GetComponent<TextMeshProUGUI>();
        }
        void Update()
        {

            if (Player.activeInHierarchy == true && Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 6f, lm))
            {
                if (hit.transform.CompareTag("Door"))
                {
                    Intract.SetActive(true);
                 
                }
                if (hit.transform.CompareTag("Hideable"))
                {
                    Hide.SetActive(true);
                    Unhide.SetActive(false);
                 
                }
                if (hit.transform.CompareTag("pickup"))
                {
                    Pickup.SetActive(true);
                   
                }
                if ((hit.transform.CompareTag("Timeline") && oc.had != null) || hit.transform.CompareTag("Timeline_independent"))
                {
                    Timeline.SetActive(true);
                 

                }
                 
                else if ((hit.transform.CompareTag("Timeline") && oc.had == null))
                {
                  
                    comment.text = "damn it's locked";
                }
               
                
              



            }
            else
            {
                Intract.SetActive(false);
                Pickup.SetActive(false);
                Drop.SetActive(false);
                Unhide.SetActive(false);
                Hide.SetActive(false);
                Timeline.SetActive(false);
                comment.text = "";

                if (oc.had != null)
                {
                    Drop.SetActive(true);
                }
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