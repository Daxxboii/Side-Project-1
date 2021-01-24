using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Hide : MonoBehaviour
{
    [SerializeField]
    float range = 4f;
    GameObject Me;
    [SerializeField]
    LayerMask hideable;
    Camera camera;
    private void Awake()
    {
        Me = this.gameObject;
    }
    public void yesYou()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, range, hideable))
        {
            
            if (hit.collider.tag == "Hideable")
            { 
                Me.SetActive(false);
            }
        }
    }

    public void NoYou()
    {
        Me.SetActive(true);
    }
}
