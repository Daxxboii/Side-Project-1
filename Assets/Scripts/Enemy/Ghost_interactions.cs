using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_interactions : MonoBehaviour
{
    public LayerMask Intractable;
    public float range;
    RaycastHit hit;
    private Animator anim = null;
 

    private void Update()
    {
        if (!Physics.Raycast(transform.position, transform.forward, out hit, range, Intractable)) return;
        if (hit.collider.tag != "Door") return;
        anim = hit.transform.GetComponentInParent<Animator>();
        InteractWithDoor();
    }

    public void InteractWithDoor()
    {
        if (anim.GetBool("IsOpen") != false) return;
        anim.SetBool("IsOpen", true);
        StartCoroutine(closeDoor());
    }

    IEnumerator closeDoor()
    {
        yield return new WaitForSeconds(3);
        anim.SetBool("IsOpen", false);
    }

}
