using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using unityCore.Audio;
using UnityEngine.UI;
using System.Threading;
using Scripts.Objects;

namespace Scripts.Player
{

    public class PlayerScript : MonoBehaviour
    {
        [SerializeField]
        ObjectController oc;
        [SerializeField]
        private Joystick joystick;
        private Vector3 move, moveT;
        [SerializeField]
        Camera FPScam;
        private CharacterController ch;
        [SerializeField]
        private AudioController ac;
        private Animator anim;
        [SerializeField]
        private MovementVariable mv = new MovementVariable();
        [SerializeField]
        CameraSettings _camera = new CameraSettings();
        [SerializeField]
        Pickups p = new Pickups();
        [Serializable]
        private struct CameraSettings
        {
            [Tooltip("Reference to camera")] public Transform cameraTransform;
            [Tooltip("Camera Sensitivity")] public float cameraSensitivity;
            [Tooltip("Camera Pitch (limit of top and bottom)")] public float cameraPitch;
            [Tooltip("Camera Inout")] public Vector2 lookInput;

            public int leftFingerID, rightFingerID;
            public float halfScreenWidth;
        }
        [Serializable]
        private struct MovementVariable
        {
            public float speed, gravity, hight, tempHight, ChaseSpeed, crouchSpeed, crouchHight, SpeedBoosttimerStart, MAxSpeedBoostTime;
        }
        [Serializable]
        private struct Pickups
        {
            public GameObject PickedUpObject;
            public float range;
            public LayerMask pickableLayer;
        }

        
        private void Awake()
        {
            ch = gameObject.GetComponent<CharacterController>();
            anim = gameObject.GetComponent<Animator>();
            //###### for camera

            // id = -1 means no finger is touching the screen
            _camera.leftFingerID = -1;
            _camera.rightFingerID = -1;

            _camera.halfScreenWidth = Screen.width / 2;
        }

        private void Update()
        {
            Locomotion();
            GetTouchInput();

            if (_camera.rightFingerID != -1)
            {
                LookAround();

            }
        }
        private void Locomotion()
        {
            float x = joystick.Horizontal;
            float z = joystick.Vertical;

            move = x * transform.right + z * transform.forward;
           
            if(ch.isGrounded == false)
            {
                move += Physics.gravity * Time.deltaTime * mv.gravity;
            }
            
            ch.Move(move * Time.deltaTime * mv.speed);
        }

        public void Pickup()
        {
            RaycastHit hit;
            if(Physics.Raycast(FPScam.transform.position, FPScam.transform.forward, out hit, p.range, p.pickableLayer))
            {
                if(hit.collider.tag == "Key" || hit.collider.tag == "Tool")
                {
                    p.PickedUpObject = hit.collider.gameObject;
                    if(p.PickedUpObject != null)
                    {
                        oc.bring(p.PickedUpObject);
                    }
                }
            }
        }
        
        void GetTouchInput()
        {
            //Iterate through all detected touches
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch t = Input.GetTouch(i);

                //check touch phase
                switch (t.phase)
                {
                    case TouchPhase.Began:

                        if (t.position.x < _camera.halfScreenWidth && _camera.leftFingerID == -1)
                        {
                            _camera.leftFingerID = t.fingerId;
                            Debug.Log("Tracking left finger with ID: " + _camera.leftFingerID);
                        }
                        else if (t.position.x > _camera.halfScreenWidth && _camera.rightFingerID == -1)
                        {
                            _camera.rightFingerID = t.fingerId;
                            Debug.Log("Tracking right finger with ID: " + _camera.rightFingerID);
                        }
                        break;
                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:

                        if (t.fingerId == _camera.leftFingerID)
                        {
                            _camera.leftFingerID = -1;
                            Debug.Log("Stopped tracking left finger");
                        }
                        else if (t.fingerId == _camera.rightFingerID)
                        {
                            _camera.rightFingerID = -1;
                            Debug.Log("Sropped tracking right finger");
                        }
                        break;
                    case TouchPhase.Moved:

                        //get input to look around
                        if (t.fingerId == _camera.rightFingerID)
                        {
                            _camera.lookInput = t.deltaPosition * _camera.cameraSensitivity * Time.deltaTime;
                        }
                        break;

                    case TouchPhase.Stationary:

                        //set lookInput to zero if finger is still;
                        if (t.fingerId == _camera.rightFingerID)
                        {
                            _camera.lookInput = Vector2.zero;
                        }
                        break;
                        
                }
            }
        }


        void LookAround()
        {
            //Camera Pitch (Up & Down) 
            _camera.cameraPitch = Mathf.Clamp(_camera.cameraPitch - _camera.lookInput.y, -90.0f, 90.0f);
            _camera.cameraTransform.localRotation = Quaternion.Euler(_camera.cameraPitch, 0f, 0f);

            //Yaw (Right & Left)
            transform.Rotate(transform.up, _camera.lookInput.x);
        }
        /// <summary>
        ///  animations
        ///  added unlocked doors animations
        /// </summary

    }

}
