using System.Collections;
using UnityEngine;
using System;
using unityCore.Audio;
using UnityEngine.UI;
using System.Threading;
using Scripts.Objects;
using Boo.Lang.Environments;

namespace Scripts.Player
{

    public class PlayerScript : MonoBehaviour
    {
        [SerializeField]
        int CloseTime;
        public static event Action<bool, int> PlayCutscene;
        public static event Action<bool, int> TellStory;
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
        [SerializeField]
        IntractionSettings it = new IntractionSettings();
        [SerializeField]
        
        [Serializable]
        private struct IntractionSettings 
        {
            public float range;
            public LayerMask Intractable;
            public Animator anim;
            public bool IsOpened;
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
        [Serializable]
        private struct MovementVariable
        {
            public float speed, tempSpeed, gravity, hight, tempHight, ChaseSpeed, crouchSpeed, crouchHight, SpeedBoosttimerStart, MAxSpeedBoostTime, crawlHeight, CrawlSpeed;
            public bool isJumping, isCrouching,isVenting,isGettingChased;
            public LayerMask vent;
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
           Application.targetFrameRate = 60;
            mv.tempSpeed = mv.speed;
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

            

            if(mv.isGettingChased && !mv.isCrouching)
            {
                mv.speed = mv.crouchSpeed;
                mv.SpeedBoosttimerStart += Time.deltaTime;
                if (mv.SpeedBoosttimerStart > mv.MAxSpeedBoostTime)
                {
                    mv.speed = mv.tempSpeed;
                    mv.SpeedBoosttimerStart = 0.0f;
                    mv.isGettingChased = false;
                }
            }
            if (ch.isGrounded == false)
            {
                move += Physics.gravity * Time.deltaTime * mv.gravity;
            }
            
            ch.Move(move * Time.deltaTime * mv.speed);
        }
        //crouch
        public void Crouch()
        {
            
            mv.isCrouching = !mv.isCrouching;
            if (mv.isCrouching == false)
            {
                
                ch.height = mv.tempHight;
                mv.speed = mv.tempSpeed;
            }
            else
            {
                ch.height = mv.crouchHight;
                mv.speed = mv.crouchSpeed;
            }
        }

        void isGettingChased()
        {
            if (ChasingMe() >= 7f)
            {
                mv.isGettingChased = true;
            }
            else
                mv.isGettingChased = false;
        }

        float ChasingMe()
        {
            GameObject e = GameObject.FindWithTag("Enemy");
            return Vector3.Distance(transform.position, e.transform.position);
        }

        //pickups
        public void Pickup()
        {
            RaycastHit hit;
            if(Physics.Raycast(FPScam.transform.position, FPScam.transform.forward, out hit, p.range, p.pickableLayer))
            {
                if(hit.collider.tag == "Key" || hit.collider.tag == "Tool" || hit.collider.tag == "pickup")
                {
                    p.PickedUpObject = hit.collider.gameObject;
                    if(p.PickedUpObject != null)
                    {
                        oc.bring(p.PickedUpObject);
                        Destroy(p.PickedUpObject);
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
                            
                        }
                        else if (t.position.x > _camera.halfScreenWidth && _camera.rightFingerID == -1)
                        {
                            _camera.rightFingerID = t.fingerId;
                            
                        }
                        break;
                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:

                        if (t.fingerId == _camera.leftFingerID)
                        {
                            _camera.leftFingerID = -1;
                            
                        }
                        else if (t.fingerId == _camera.rightFingerID)
                        {
                            _camera.rightFingerID = -1;
                            
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
            if(it.anim.GetBool("IsOpen") == false)
            {
                it.anim.SetBool("IsOpen", true);
                StartCoroutine(closeDoor());
            }
            
        }
        
        IEnumerator closeDoor()
        {
            yield return new WaitForSeconds(CloseTime);
            it.anim.SetBool("IsOpen", false);
        }
    }

}
