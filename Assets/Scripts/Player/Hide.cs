using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Scripts.Player
{
    public class Hide : MonoBehaviour
    {
        public static event Action<bool, GameObject> isHideing;
        [SerializeField] LayerMask hideableLayer;
        [SerializeField] float range;
        HidingController h;
        public void OnHideButtonPressed()
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position,transform.forward,out hit, range, hideableLayer ))
            {
                h = hit.transform.gameObject.GetComponent<HidingController>();
                if (hit.collider.CompareTag("Hideable"))
                {
                    h.enabled = true;
                    if (isHideing != null)
                        isHideing(true, this.gameObject);
                }
            }
        }
        public void OnUnHideButtonPressed()
        {
            Debug.Log("manish smex");
            
            if (isHideing != null)
                isHideing(false, this.gameObject);
            h.enabled = false;
        }
    }
}
