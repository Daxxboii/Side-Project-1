using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingController : MonoBehaviour
{
    [SerializeField] GameObject cam, Unhide, player, FPSC;
    private void Awake()
    {
        Unhide = GameObject.Find("unhide");
        Unhide.SetActive(false);
        cam = this.gameObject.transform.GetChild(0).gameObject;
        player = GameObject.Find("Player");
        FPSC = GameObject.Find("FpsCanvas");
        
    }

    public void GiveItToMe()
    {
        player.SetActive(false);
        Unhide.SetActive(true);
        cam.SetActive(true);
        FPSC.SetActive(false);
    }

    public void back()
    {
        player.SetActive(true);
        Unhide.SetActive(false);
        cam.SetActive(false);
        FPSC.SetActive(true);
    }
}
