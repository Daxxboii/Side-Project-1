using Scripts.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Scripts.Timeline;
using UnityEngine.InputSystem;
using Scripts.Player;

namespace Scripts.Buttons
{
    public class ButtonOpen : MonoBehaviour
    {
        private PlayerControls playercontrols;

        [SerializeField]
        GameObject Error_comment;
        [SerializeField] GameObject Player;
       
        TextMeshProUGUI comment;
        [SerializeField] Camera cam;
        [SerializeField] LayerMask lm;
        [SerializeField] ObjectController oc;
        [SerializeField] Timeline_Manager tm;
        

        private string[] arrayList;
        [SerializeField] Intraction interaction;
        [SerializeField] ComicManager comicManager;
        [SerializeField] AudioManager audioManager;
        [SerializeField] SaveManager saveManager;
        [SerializeField] Timeline_Manager timeline_Manager;
        public RaycastHit hit;

        bool pointed;
        void Awake()
        {
            playercontrols = new PlayerControls();
            playercontrols.Controls.Interact.performed += ctx => onInteract();
            Error_comment.SetActive(true);
            arrayList = new string[] { "Door" , "Escape Door" , "Door_locker" , "Door_fence" , "pickup","Doll","Fingie","Comic","Note","Save","Map","Timeline","Timeline_independent","Obstacle", "Blackboard","Untagged" };
            comment = Error_comment.GetComponent<TextMeshProUGUI>();
        }

     
        void Update()
        {
            if (Player.activeInHierarchy == true && Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 6f, lm))
            {
                pointed = true;
            }
            else
            {
                close();
            }

        }

        void onInteract()
        {
            if (pointed)
            {
                if (hit.transform.CompareTag("Door") || hit.transform.CompareTag("Escape Door") || hit.transform.CompareTag("Door_locker") || hit.transform.CompareTag("Door_fence") || hit.transform.CompareTag("pickup") || hit.transform.CompareTag("Doll") || hit.transform.CompareTag("Fingie"))
                {
                    interaction.PlayerInteraction();
                }

                else if (hit.transform.CompareTag("Comic"))
                {
                    interaction.PlayerInteraction();
                    comicManager._preOpen();
                    close();
                }

                else if (hit.transform.CompareTag("Note"))
                {
                    interaction.PlayerInteraction();
                    audioManager.Paused();
                    audioManager.Note_Open();

                }
                else if (hit.transform.CompareTag("Save"))
                {
                    saveManager.Save();
                }


                else if (hit.transform.CompareTag("Map"))
                {
                    interaction.PlayerInteraction();
                    comment.text = "Should take it , would be of help later";
                }

                else if ((hit.transform.CompareTag("Timeline") && oc.had != null) || hit.transform.CompareTag("Timeline_independent"))
                {
                    timeline_Manager.TimeLine_Activator();
                    comment.text = "Interact?";
                }

                else if ((hit.transform.CompareTag("Timeline") && oc.had == null))
                {
                    comment.text = "get something ";
                }

                else if (hit.transform.CompareTag("Obstacle"))
                {
                    comment.text = "Going there wont be a good idea";
                }
                else if (hit.transform.CompareTag("Blackboard"))
                {
                    comment.text = "Find something to write with";
                }
                else if (hit.transform.CompareTag("Untagged"))
                {
                    close();
                }
            }
        }
      public   void close()
        {
            comment.text = "";
        }
      

        private void OnEnable()
        {
            playercontrols.Enable();
        }

        private void OnDisable()
        {
            playercontrols.Disable();
        }
    }
}