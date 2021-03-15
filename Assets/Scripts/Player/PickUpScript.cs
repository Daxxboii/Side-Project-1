using Scripts.Objects;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Player
{
    public class PickUpScript : MonoBehaviour
    {
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
        void Update()
        {

        }
        public void Pickup()
        {
            RaycastHit hit;
            if (Physics.Raycast(FPScam.transform.position, FPScam.transform.forward, out hit, p.range, p.pickableLayer))
            {
                if (hit.collider.tag == "Key" || hit.collider.tag == "Tool" || hit.collider.tag == "pickup")
                {
                    p.PickedUpObject = hit.collider.gameObject;
                    //p.PickedUpObject.GetComponent<Outline>().enabled = true;
                    if (p.PickedUpObject != null)
                    {
                        oc.bring(p.PickedUpObject);
                        Destroy(p.PickedUpObject);
                    }
                }

            }
        }
    }
}