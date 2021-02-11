using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineActive : MonoBehaviour
{
    Outline o;
    private void Awake()
    {
        o = gameObject.GetComponent<Outline>();
        o.enabled = false;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            o.enabled = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            o.enabled = false;
        }
    }
}
