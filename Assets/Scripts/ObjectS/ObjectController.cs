using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

namespace Scripts.Objects
{
    public class ObjectController : MonoBehaviour
    {
        public GameObject have, had;
        public Transform hand;
        public bool hadnsFull;
        void Update()
        {
            if(have != null)
            {
                if(!hadnsFull)
                {
                    had = Instantiate(have, hand.position, hand.r``otation);
                    had.transform.SetParent(hand.transform);
                    hadnsFull = true;
                }
                else if(hadnsFull)
                {
                    
                }
            }
        }
        public void getnum(GameObject o)
        {
            have = o;
        }
    }
}