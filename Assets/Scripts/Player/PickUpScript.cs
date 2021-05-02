using Scripts.Objects;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Timeline;
namespace Scripts.Player
{
    public class PickUpScript : MonoBehaviour
    {
        [SerializeField]
        Timeline_Manager tm;
        [SerializeField]
        ObjectController oc;
        [SerializeField]
        Camera FPScam;
        [SerializeField]
        Pickups p = new Pickups();
        [Serializable]
        private struct Pickups
        {
            public GameObject PickedUpObject;
            public float range;
            public LayerMask pickableLayer;
        }
      
        public void Pickup()
        {
            RaycastHit hit;
            if (Physics.Raycast(FPScam.transform.position, FPScam.transform.forward, out hit, p.range, p.pickableLayer))
            {
                if (hit.collider.tag == "Key"  || hit.collider.tag == "pickup")
                {
                    p.PickedUpObject = hit.collider.gameObject;
                    tm.ObjectiveList();
                    if (p.PickedUpObject != null)
                    {
                        oc.bring(p.PickedUpObject);
                       Destroy(p.PickedUpObject);
                       
                    }
                }
                else if(hit.collider.tag == "Doll")
                {
                    hit.transform.gameObject.SetActive(false);
                }
                else if (hit.collider.tag == "Comic")
                {
                    hit.transform.gameObject.SetActive(false);
                }


            }
        }
    }
}