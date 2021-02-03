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


        public GameObject have, had, havetemp;
        public Transform hand;
        public bool hadnsFull;


        private void Awake()
        {
            
            
        }

        void Equip()
        {
            if(hadnsFull == false)
            {
                had = Instantiate(have, hand.position, hand.rotation);
                had.transform.SetParent(hand.transform);
                had.gameObject.GetComponent<Rigidbody>().useGravity = false;
                had.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                hadnsFull = true;
            }
        }

        public void GetIT(GameObject o)
        {
            have = o;
            if (have != null)
            {
                havetemp = have;
                Equip();
            }
            
                
        }

        public void Throw()
        {
            had.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            had.gameObject.GetComponent<Rigidbody>().useGravity = true;
            GameObject bb = Instantiate(had, transform.position, transform.rotation);
            bb.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 10);
            bb = null;
            have = null;
            Destroy(had);
            had = null;
            hadnsFull = false;
        }

        public void Swap()
        {

        }
    }
}