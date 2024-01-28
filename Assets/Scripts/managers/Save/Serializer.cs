using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Serializer : MonoBehaviour
{
    public Vector3 loc, rot;
    public bool collider_state;
   
    public void Assign_transforms()
    {
        loc = transform.position;
        rot = transform.rotation.eulerAngles;
    }

    public void _collider()
    {
        collider_state = gameObject.GetComponent<Collider>()!=null && gameObject.GetComponent<Collider>().enabled;
    }

    public bool Rigidbody_Exists()
    {
        return gameObject.GetComponent<Rigidbody>() != null;
    }
}
