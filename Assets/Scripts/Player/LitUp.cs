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
    Outline outline,had;
    [SerializeField]
    ObjectController oc;
   
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 6f, lm))
        {
            if (hit.transform.gameObject.layer != 0)
            {
                had = outline;
                outline = hit.transform.gameObject.GetComponent<Outline>();
                if (had != outline)
                {
                    had.enabled = false;
                }
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

        else
        {
            if (outline != null)
            {
                outline.enabled = false;
            }
        }
        

    }
}
