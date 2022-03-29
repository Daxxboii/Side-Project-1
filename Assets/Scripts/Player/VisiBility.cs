using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisiBility : MonoBehaviour
{

    [SerializeField]
    Transform cam;
    [SerializeField]
    GameObject  principal;
    public float radius,spherecast_radius;
    public LayerMask Layer;
    public bool visible;
    RaycastHit hit;
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(cam.gameObject.transform.position, principal.transform.position) < radius)
        {
            visible = IsTargetVisible(principal);
        }
        else
        {
            visible = false;
        }
       
    }
    bool IsTargetVisible( GameObject go)
    {
        if(Physics.Raycast(cam.position,cam.transform.TransformDirection(Vector3.forward), out hit, spherecast_radius, Layer))
        {
			if (hit.transform.gameObject == go)
			{
                return true;
            }
			else
			{
                return false;
			}
		}
		else
		{
            return false;
		}
       
    }
}
