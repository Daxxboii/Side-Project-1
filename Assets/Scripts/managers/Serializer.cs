using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Serializer : MonoBehaviour
{
    [HideInInspector]
    public string name;
    private int i;
    public Vector3 loc, rot;
    public bool collider_state;
   
   public void Assign_id()
    {
        name = "#" +gameObject.name+(i++).ToString();
    }
    public void Assign_transforms()
    {
        loc = transform.position;
        rot = transform.rotation.eulerAngles;
    }

    public void collider()
    {
        if (gameObject.GetComponent<Collider>()!=null)
        {
            if (gameObject.GetComponent<Collider>().enabled)
            {
                collider_state = true;
            }
            else
            {
                collider_state = false;
            }
        }

        else
        {
            collider_state = false;
        }
    }

    public bool Rigidbody_Exists()
    {
        if ( gameObject.GetComponent<Rigidbody>() != null)
        {
            if(gameObject.GetComponent<Rigidbody>().constraints == RigidbodyConstraints.None)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return false;
        }
    }
}
