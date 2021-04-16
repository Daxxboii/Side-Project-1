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
      void Start()
    {
        Assign_id();
    } 
    void Assign_id()
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
}
