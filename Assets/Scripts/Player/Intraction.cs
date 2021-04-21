using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Scripts.Player
{
    public class Intraction : MonoBehaviour
    {
       
        [SerializeField]
        private TextMeshProUGUI note_text;
        [SerializeField]
        IntractionSettings it = new IntractionSettings();
        [SerializeField]
        Camera FPScam;
        Note_manager nm = null;

        [SerializeField]
        GameObject Note_panel,map,minimap;
        [Serializable]
        private struct IntractionSettings
        {
            public float range, CloseTime;
            public LayerMask Intractable;
            public Animator anim;
            public bool IsOpened;
        }

        private void Start()
        {
            Note_panel.SetActive(false);
        }
        public void PlayerInteraction()
        {
            RaycastHit hit;
            if (Physics.Raycast(FPScam.transform.position, FPScam.transform.forward, out hit, it.range, it.Intractable))
            {
                it.anim = hit.transform.GetComponentInParent<Animator>();
                if (hit.collider.tag == "Door")
                {
                    IntractWithDoor();
                }

                else if (hit.collider.tag == "Note")
                {
                  
                    nm = hit.transform.GetComponent<Note_manager>();
                    Note_Open();
                    hit.transform.gameObject.SetActive(false);
                }
               else if(hit.collider.tag == "Map")
                {
                    map.SetActive(false);
                    minimap.SetActive(true);
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

        }

        public void Note_Close()
        {
            Note_panel.SetActive(false);
        }
    }
}
