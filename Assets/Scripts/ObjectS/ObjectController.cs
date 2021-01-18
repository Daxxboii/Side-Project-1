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
        public Transform hand, objectsT;
        public bool hadnsFull;
        [SerializeField]
        GameObject pickup, dropButton;
        
        void Update()
        {
            if(have != null)
            {
                if(hadnsFull == false) 
                {
                    had = Instantiate(have, hand.position, hand.rotation);
                    had.transform.SetParent(hand.transform);
                    had.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    had.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    
                }
            }  
        }
        public void getnum(GameObject o)
        {
            have = o;
            if(have != null)
            {
                pickup.SetActive(false);
                dropButton.SetActive(true);
            }
        }
        public void drop()
        {
            had.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            had.gameObject.GetComponent<Rigidbody>().useGravity = true;
            Instantiate(had, transform.position, transform.rotation);
            have = null;
            Destroy(had);
            had = null;
            hadnsFull = false;
            pickup.SetActive(true);
            dropButton.SetActive(false);
        }
    }
}