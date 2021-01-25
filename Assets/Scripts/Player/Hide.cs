using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Scripts.Player
{
    public class Hide : MonoBehaviour
    {
        GameObject hideButton;
        [SerializeField] float range = 4f;
        [SerializeField] LayerMask hidableLayer;

        private void Awake()
        {
            hideButton = GameObject.Find("hide");
            hideButton.SetActive(false);
           
        }

        private void Update()
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position, transform.forward, out hit, range, hidableLayer))
                hideButton.SetActive(true);
            else
                hideButton.SetActive(false);

        }
        public void OnButtonPressed()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, range, hidableLayer))
            {
                if(hit.collider.tag == "Hideable")
                {
                    HidingController hc = hit.transform.gameObject.GetComponent<HidingController>();
                    hc.GiveItToMe();
                    
                }
                
            }
        }
    }
}
