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
        public GameObject pickup, dropButton;


        private void Awake()
        {
            pickup = GameObject.Find("pickup");
            dropButton = GameObject.Find("Throw");
            dropButton.SetActive(false);
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
                pickup.SetActive(false);
                dropButton.SetActive(true);
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
            pickup.SetActive(true);
            dropButton.SetActive(false);
        }

        public void Swap()
        {

        }
    }
}