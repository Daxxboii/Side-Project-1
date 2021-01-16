using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

namespace Scripts.Objects
{
    public class ObjectController : MonoBehaviour
    {
        public GameObject have, had, havetemp;
        public Transform hand, objectsT;
        public bool hadnsFull;
        void Update()
        {
            if(have != null)
            {
                if(!hadnsFull)
                {
                    had = Instantiate(have, hand.position, hand.rotation);
                    had.transform.SetParent(hand.transform);
                    had.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    had.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    hadnsFull = true;
                }
            }
        }
        public void getnum(GameObject o)
        {
            have = o;
        }
        public void drop()
        {
            
            had.gameObject.GetComponent<Rigidbody>().useGravity = true;
            had.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            Instantiate(had, transform.position, transform.rotation);
            Destroy(had);
            hadnsFull = false;
        }
    }
}