using Scripts.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LitUp : MonoBehaviour
{
    
    [SerializeField]
    Camera cam;
    [SerializeField]
    LayerMask lm;
    Outline outline;
    [SerializeField]
    ObjectController oc;
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 6f, lm))
        {
            outline = hit.transform.gameObject.GetComponent<Outline>();
            outline.enabled = true;
        }

        else
        {
            if (outline != null)
            {
                outline.enabled = false;
            }
        }
        

    }
}
