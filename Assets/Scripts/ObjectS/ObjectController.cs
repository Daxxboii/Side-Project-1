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
        GameObject have, had;
        bool isHolding;
        Rigidbody rb;
        [SerializeField]
        Transform hand;
        void equip()
        {
            had = Instantiate(have, hand.transform.position, hand.transform.rotation);
            had.transform.SetParent(hand);
            rb = had.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeAll;
            rb.useGravity = false;
            isHolding = true;
        }
        public void bring(GameObject o)
        {
            have = o;
            if(!isHolding)
                equip();
        }

        public void DropDown()
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.useGravity = true;
            had = Instantiate(have, hand.transform.position, hand.transform.rotation);
            had = null;
            have = null;
            isHolding = false;
        }
        public void swap()
        {

        }
    }
}