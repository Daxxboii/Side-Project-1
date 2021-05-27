using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Timeline;
public class Objective_Trigger : MonoBehaviour
{
    public Timeline_Manager tm;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            tm.ObjectiveList();
            this.gameObject.SetActive(false);
        }
    }
}