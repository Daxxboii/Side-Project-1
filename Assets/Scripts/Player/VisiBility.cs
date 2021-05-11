using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisiBility : MonoBehaviour
{

    [SerializeField]
    Camera cam;
    [SerializeField]
    LayerMask lm;
    [SerializeField]
    GameObject Player;
    RaycastHit hit;
    public float radius;
    public bool visible;
    // Update is called once per frame
    void Update()
    {
        if (Player.activeInHierarchy == true && Physics.SphereCast(cam.transform.position,radius, cam.transform.forward, out hit, 10f, lm))
        {
            visible = true;
        }
        else
        {
            visible = false;
        }
    }
}
