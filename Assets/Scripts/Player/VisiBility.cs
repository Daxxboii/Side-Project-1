using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisiBility : MonoBehaviour
{

    [SerializeField]
    Transform cam;
    [SerializeField]
    GameObject  principal;
    public float radius,sphere_Radius;
    public LayerMask Layer;
    public bool visible;
    RaycastHit hit;
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(cam.gameObject.transform.position, principal.transform.position) < radius)
        {
            visible = IsTargetVisible(cam, principal);
        }
        else
        {
            visible = false;
        }
       
    }
    bool IsTargetVisible(Transform c, GameObject go)
    {
        if(Physics.SphereCast(cam.position,sphere_Radius,transform.forward,out hit, radius, Layer))
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
