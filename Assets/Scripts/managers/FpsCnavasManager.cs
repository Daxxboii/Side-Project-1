using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Scripts.Player;

namespace Scripts.Managers
{
    public class FpsCnavasManager : MonoBehaviour
    {

        [SerializeField]
        private Button _pickUp, _hide, _interact;

        public static event Action<bool> OnPickUpPressed;
        public static event Action<bool> OnHidePressed;
        public static event Action<bool> OnInteractPressed;

        private void Awake()
        {
            _pickUp.interactable = false;
            _hide.interactable = true;
            _interact.interactable = true;
        }

        private void OnEnable()
        {
            PlayerNew.OnFindingInteractables += PlayerNew_OnFindingInteractables;
        }

        private void PlayerNew_OnFindingInteractables(bool obj)
        {
            if(obj == true)
            {
                _pickUp.interactable = true;
            }
            else
            {
               _pickUp.interactable = false;
            }

            Debug.Log("Intractable: " + obj);
        }

        public void PickUp()
        {
            OnPickUpPressed(true);
        }

        public void HideOrUnhide()
        {
            OnHidePressed(true);
        }

        public void Interact()
        {
            OnInteractPressed(true);
        }

    }
}
