using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using unityCore.Audio;
using UnityEngine.UI;
using System.Threading;

namespace Scripts.Player
{
    [System.Serializable]
    public struct LocoMotionVariables
    {
        [Tooltip("Base Speed")]
        public float speed;

        [Tooltip("Speed While Getting hased")]
        public float Chasedspeed;
        [Tooltip("max time for chaseing")]
        public float ChaseTime;
        [Tooltip("if enemy hits the collider this timer will start")]
        public float ChaseStart;
        [Tooltip("saved base speed inside of it")]
        public float SpeedTemp;
        [Tooltip("gravity")]
        public float Gravity;

        public float crouchspeed, chasedcrouchspeed, crouchHightn, crouchHightTemp, health, attackTimer, AttackTime;

        public bool gettingChased, CroutchButtonPressed;
    }

    [System.Serializable]
    public struct PickitUpVariables
    {
        [Tooltip("layerSelection for pickable items")]
        public LayerMask pickables;

        [Tooltip("Is inventory full or not")]
        public bool isFull;
        [Tooltip("how far should be the object in order to pickup")]
        public float Range;

        public GameObject tempInMe;
    }


    public class PlayerScript : MonoBehaviour
    {
        public AudioController audioController;
        [SerializeField] LayerMask IC;
        bool open, hiding, holding;
        CharacterController ch;
        Vector3 move;
        Camera FpsCam;
        [SerializeField]
        Joystick joystick;
        [SerializeField]
        Scripts.Objects.ObjectController oc;
        [SerializeField]
        LocoMotionVariables motionVariables;
        [SerializeField]
        PickitUpVariables pickUpVariables;
        [SerializeField]
        IntractionSettings ic = new IntractionSettings();
        [SerializeField]
        buttons bc = new buttons();
        [Serializable]
        struct buttons
        {

            public GameObject PickUp, DropDown, hide, intract, unhide;
        }
        [Serializable]
        struct IntractionSettings
        {

            public float IntractionRange, autoCloseTimer, timer;
            public LayerMask IntractableObjects;
            public Animator _anim;
        }

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

        [SerializeField]
        private CameraSettings _camera = new CameraSettings { };


        private void Awake()
        {
            bc.DropDown.SetActive(false);
            bc.PickUp.SetActive(false);
            bc.hide.SetActive(false);
            bc.unhide.SetActive(false);
            bc.intract.SetActive(false);
            ch = GetComponent<CharacterController>();
            FpsCam = GetComponentInChildren<Camera>();
            motionVariables.SpeedTemp = motionVariables.speed;
            joystick = FindObjectOfType<FixedJoystick>();
            motionVariables.crouchHightTemp = ch.height;
            //###### for camera

            // id = -1 means no finger is touching the screen
            _camera.leftFingerID = -1;
            _camera.rightFingerID = -1;

            _camera.halfScreenWidth = Screen.width / 2;
        }


        private void Update()
        {
            LocoMotion();
            PickItUp();

            GetTouchInput();

            if (_camera.rightFingerID != -1)
            {
                LookAround();
                
            }
            ActivasionOfButtons();
            TakeDamage();
        }

        void TakeDamage()
        {
            if(GhostInAttackRange() < 3f)
            {
                motionVariables.attackTimer += Time.deltaTime;
                if(motionVariables.attackTimer > motionVariables.AttackTime)
                {
                    motionVariables.health--;
                    motionVariables.attackTimer = 0.0f;
                }
            }
            if(motionVariables.health <= 0)
            {
                PlayerDeath();
            }
        }
        void PlayerDeath()
        {
            this.gameObject.SetActive(false);
        }
        
        float GhostInAttackRange()
        {
            GameObject ghost = GameObject.FindWithTag("enemy");
            return Vector3.Distance(transform.position, ghost.transform.position);
        }
        
        void ActivasionOfButtons()
        {
            RaycastHit h;
            if(Physics.Raycast(FpsCam.transform.position, FpsCam.transform.forward,out h, 4f ,IC))
            {
                if(h.collider.tag == "pickup")
                {
                    bc.PickUp.SetActive(true);
                }
                else if(oc.had != null)
                {
                    bc.DropDown.SetActive(true);
                }
                if(h.collider.tag == "Hideable")
                {
                    bc.hide.SetActive(true);
                    
                }
                if(h.collider.tag == "Door")
                {
                    bc.intract.SetActive(true);
                }
            }
            else
            {
                if (oc.had == null)
                    bc.DropDown.SetActive(false);
                bc.PickUp.SetActive(false);
                bc.intract.SetActive(false);
                bc.hide.SetActive(false);
                if (hiding)
                    bc.unhide.SetActive(true);
                else
                    bc.unhide.SetActive(false);
            }
            
        }
        void LocoMotion()
        {
            float x = joystick.Horizontal;
            float z = joystick.Vertical;

            move = x * transform.right + z * transform.forward;

            if (motionVariables.gettingChased && !motionVariables.CroutchButtonPressed)
            {
                motionVariables.speed = motionVariables.Chasedspeed;
                motionVariables.ChaseStart += Time.deltaTime;
                if (motionVariables.ChaseStart > motionVariables.ChaseTime)
                {
                    motionVariables.speed = motionVariables.SpeedTemp;
                    motionVariables.ChaseStart = 0.0f;
                    motionVariables.gettingChased = false;
                }
            }
            if (ch.isGrounded == false)
            {
                move += Physics.gravity * Time.deltaTime * motionVariables.Gravity;
            }
            
            ch.Move(move * Time.deltaTime * motionVariables.speed);
        }
        public void Crouch()
        {
            
            motionVariables.CroutchButtonPressed = !motionVariables.CroutchButtonPressed;
            if (motionVariables.CroutchButtonPressed)
            {
                ch.height = motionVariables.crouchHightn;
                motionVariables.speed = motionVariables.crouchspeed;
            }
            else if (!motionVariables.CroutchButtonPressed)
            {
                ch.height = motionVariables.crouchHightTemp;
                motionVariables.speed = motionVariables.SpeedTemp;
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Enemy")
            {
                motionVariables.gettingChased = true;
            }
        }

        void PickItUp()
        {
            Collider c;
            RaycastHit hit;
            Color Temp;


            if (Physics.Raycast(FpsCam.transform.position, FpsCam.transform.forward, out hit, pickUpVariables.Range, pickUpVariables.pickables))
            {
                c = hit.collider;
                pickUpVariables.tempInMe = hit.transform.gameObject;
                Temp = c.transform.gameObject.GetComponent<Renderer>().material.color;
            }
            else
            {
                pickUpVariables.tempInMe = null;
            }

        }

        public void PickUpObject()
        {
            if (oc.had == null)
            {
                if (pickUpVariables.tempInMe != null)
                {
                    holding = true;
                    oc.GetIT(pickUpVariables.tempInMe);
                    Destroy(pickUpVariables.tempInMe);
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
        /// </summary>
        public void playerInteraction()
        {
            RaycastHit hit;
            if (Physics.Raycast(FpsCam.transform.position, FpsCam.transform.forward, out hit, ic.IntractionRange, ic.IntractableObjects))
            {
                open =! open;
                ic._anim = hit.collider.gameObject.GetComponentInParent<Animator>();
                if (hit.collider.CompareTag("Door"))
                {
                    IntractWithDoor(ic._anim);
                }
                
            }
            
        }
        void IntractWithDoor(Animator a)
        {
            a.SetBool("Open", open);
        }
        
    }
}
