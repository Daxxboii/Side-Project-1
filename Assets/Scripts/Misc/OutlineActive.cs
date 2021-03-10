using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineActive : MonoBehaviour
{
    Outline o;
    bool active;
    private void Awake()
    {
        o = gameObject.GetComponent<Outline>();
        o.enabled = false;
    }
    private void Update()
    {
        if(active)
        {
            o.enabled = true;
        }
        else
        {
            o.enabled = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            active = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            active = false;
        }
    }
}
