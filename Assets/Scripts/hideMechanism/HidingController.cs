using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Player;
public class HidingController : MonoBehaviour
{
    [SerializeField] GameObject cam;
    GameObject player;
    private void OnEnable()
    {
        Hide.isHideing += OnHideing;
    }
    public void OnHideing(bool h, GameObject g)
    {
        player = g.transform.gameObject;
        if(h == true)
        {
            cam.SetActive(true);
            player.SetActive(false);
        }
        else if(h == false)
        {
            cam.SetActive(false);
            player.SetActive(true);
        }
    }
    private void OnDisable()
    {
        Hide.isHideing -= OnHideing;
    }
}
