﻿
using UnityEngine;
using Scripts.Player;

namespace Scripts.Objects
{
    public class ObjectController : MonoBehaviour
    {
        [SerializeField]
        Intraction pickup;
        public GameObject have, had;
        Rigidbody rb;
        private void FixedUpdate()
        {
            if(had != null)
            {
                had.GetComponent<Outline>().enabled = false;
            }
        }
        void equip(GameObject pickup)
        {
            had = pickup;
           
           
            had.transform.SetParent(this.transform);
            had.transform.localPosition = Vector3.zero;
            pickup.transform.localRotation = Quaternion.identity;
          /*  rb = had.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeAll;
            rb.useGravity = false;*/
            have = null;
        }
        public void bring(GameObject o)
        {
            have = o;
          //  Debug.Log("hi");
            equip(o);
        }

        public void DropDown()
        {
            had.transform.SetParent(null);
          rb.constraints = RigidbodyConstraints.None;
            rb.useGravity = true;
            had = null;
            rb = null;
            have = null;
        }
      
    }
}