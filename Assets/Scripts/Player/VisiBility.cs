using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisiBility : MonoBehaviour
{

    [SerializeField]
    Camera cam;
    [SerializeField]
    GameObject  principal;
    public float radius;
    public bool visible;
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
    bool IsTargetVisible(Camera c, GameObject go)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(c);
        var point = go.transform.position;
        foreach (var plane in planes)
        {
            if (plane.GetDistanceToPoint(point) < 0)
                return false;
        }
        return true;
    }
}
