using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intraction : MonoBehaviour
{
    [SerializeField]
    IntractionSettings it = new IntractionSettings();
    [SerializeField]
    Camera FPScam;

    [Serializable]
    private struct IntractionSettings
    {
        public float range, CloseTime;
        public LayerMask Intractable;
        public Animator anim;
        public bool IsOpened;
    }
    public void PlayerInteraction()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPScam.transform.position, FPScam.transform.forward, out hit, it.range, it.Intractable))
        {
            it.anim = hit.transform.GetComponentInParent<Animator>();
            if (hit.collider.tag == "Door")
            {
                IntractWithDoor();
            }
        }
    }
    void IntractWithDoor()
    {
        if (it.anim.GetBool("IsOpen") == false)
        {
            it.anim.SetBool("IsOpen", true);
            StartCoroutine(closeDoor());
        }

    }

    IEnumerator closeDoor()
    {
        yield return new WaitForSeconds(it.CloseTime);
        it.anim.SetBool("IsOpen", false);
    }
}
