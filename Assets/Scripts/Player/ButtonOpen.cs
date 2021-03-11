using Scripts.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonOpen : MonoBehaviour
{
    [SerializeField]
    GameObject Player, Hide, Unhide, Intract, Pickup, Drop;
    [SerializeField]
    LayerMask lm;
    [SerializeField]
    ObjectController oc;
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        Pickup.SetActive(false);
        Intract.SetActive(false);
        Hide.SetActive(false);
    }

    void Update()
    {
        RaycastHit hit;
        if(Player.activeInHierarchy == true && Physics.Raycast(Player.transform.position, Player.transform.forward, out hit, 4f, lm))
        {
            if(hit.transform.CompareTag("Door"))
            {
                Intract.SetActive(true);
            }
            if(hit.transform.CompareTag("Hideable"))
            {
                Hide.SetActive(true);
                Unhide.SetActive(false);
            }
            if (hit.transform.CompareTag("pickup"))
            {
                Pickup.SetActive(true);
            }
            

        }
        else
        {
            Intract.SetActive(false);
            Pickup.SetActive(false);
            Drop.SetActive(false);
            Unhide.SetActive(false);
            Hide.SetActive(false);
            if (oc.had != null)
            {
                Drop.SetActive(true);
            }
            if(Player.activeInHierarchy == true)
            {
                Unhide.SetActive(false);
            }
            else
            {
                Unhide.SetActive(true);
            }
        }
    }
}
