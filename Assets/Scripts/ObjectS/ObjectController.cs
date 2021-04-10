using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;


namespace Scripts.Objects
{
    public class ObjectController : MonoBehaviour
    {
        
        public GameObject have, had;
        bool isHolding;
        Rigidbody rb;
        private void FixedUpdate()
        {
            if(had != null)
            {
                had.GetComponent<Outline>().enabled = false;
            }
        }
        void equip()
        {
            had = Instantiate(have, transform.position, transform.rotation);
            had.transform.SetParent(this.transform);
            rb = had.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeAll;
            rb.useGravity = false;
            isHolding = true;
            have = null;
        }
        public void bring(GameObject o)
        {
            have = o;
            Debug.Log("hi");
            equip();
        }

        public void DropDown()
        {
            had.transform.SetParent(null);
          rb.constraints = RigidbodyConstraints.None;
            rb.useGravity = true;
            had = null;
            rb = null;
            have = null;
            isHolding = false;
        }
      
    }
}