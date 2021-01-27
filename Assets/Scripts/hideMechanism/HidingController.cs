using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Player;
public class HidingController : MonoBehaviour
{
    [SerializeField] GameObject cam;
    MeshRenderer player;
    private void OnEnable()
    {
        Hide.isHideing += OnHideing;
    }
    public void OnHideing(bool h, GameObject g)
    {
        player = g.transform.gameObject.GetComponent<MeshRenderer>();
        if(h == true)
        {
            cam.SetActive(true);
            player.enabled = false;
            Debug.Log("loda");
        }
        else if(h == false)
        {
            cam.SetActive(false);
            player.enabled = true;
            Debug.Log("loda2");
        }
    }
    private void OnDisable()
    {
        Hide.isHideing -= OnHideing;
    }
}
