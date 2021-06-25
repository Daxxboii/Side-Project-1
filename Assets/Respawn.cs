using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    Vector3 pos;
    public int offset;
  public  CharacterController cc;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.transform.name);
        cc = collision.transform.GetComponentInParent<CharacterController>();
        cc.enabled = false;
            pos = collision.gameObject.transform.position;
            pos.y += offset;
            collision.gameObject.transform.position = pos;
       // cc.enabled = true;
        
    }
}
