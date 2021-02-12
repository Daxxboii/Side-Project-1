using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Scripts.Player
{
    public class PlayerNew : MonoBehaviour
    {
        //Delegates
        public static event Action<bool> OnFindingInteractables;


        [System.Serializable]
        private struct Player
        {
            [Tooltip("Player Speed")] public float currentPlayerspeed;
            [Tooltip("Walking speed")] public float walkingSpeed;
            [Tooltip("Sprint Speed")] public float sprint;
            [Tooltip("crouch speed")] public float crouchSpeed;
            [Tooltip("Gravity")] public float gravity;

            [Tooltip("Originalk Height")] public float height;
            [Tooltip("Crouch Height")] public float crouchHeight;

            public float chaseTime;
            public float chasingTime;

            public bool isGettingChased, isCrouching;
        }

        [System.Serializable]
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

        [SerializeField]
        private Player _player = new Player {};
        [SerializeField]
        private Joystick _joystick;

        [SerializeField]
        private float _rayDistance;
        
        private Vector3 _velocity;        

        //componenets
        private CharacterController _char;
        

        private void Awake()
        {

            // id = -1 means no finger is touching the screen
            _camera.leftFingerID = -1;
            _camera.rightFingerID = -1;

            _camera.halfScreenWidth = Screen.width / 2;
        }

        private void Start()
        {
            _char = GetComponent<CharacterController>();
        }

        private void Update()
        {
            
            Locomotion();
            GetTouchInput();
            if (_camera.rightFingerID != -1)
            {
                LookAround();
            }

            CastRay();

        }

        private void Locomotion()
        {
            float x = _joystick.Horizontal;
            float z = _joystick.Vertical;

            _velocity = (x * transform.right + z * transform.forward);

            if(_player.isGettingChased == true && _player.isCrouching == false)
            {
                _player.currentPlayerspeed = _player.sprint;
                _player.chasingTime += Time.deltaTime;
                if(_player.chasingTime >= _player.chaseTime)
                {
                    _player.currentPlayerspeed = _player.walkingSpeed;
                    _player.chasingTime = 0f; // will produce time bug
                }

            }

            if(_player.isCrouching == true)
            {
                _char.height = _player.crouchHeight;
                _player.currentPlayerspeed = _player.crouchSpeed;
            }
            else
            {
                _char.height = _player.height;
                _player.currentPlayerspeed = _player.walkingSpeed;

                Debug.Log("Walking");
            }

            if(_char.isGrounded == false)
            {
                _velocity.y -= _player.gravity * Time.deltaTime;
            }

            _velocity = _velocity * _player.currentPlayerspeed;

            _char.Move(_velocity * Time.deltaTime );
           
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

        void CastRay()
        {
            RaycastHit hitInfo;            

            if (Physics.Raycast(_camera.cameraTransform.position,transform.TransformDirection(Vector3.forward), out hitInfo, _rayDistance))
            {
                Debug.Log(hitInfo);
                if(hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Pickups"))
                {
                    if(OnFindingInteractables != null)
                    {
                        OnFindingInteractables(true);
                        Debug.Log("Found Something");
                    }
                }

                if(hitInfo.collider == null)
                {
                    if (OnFindingInteractables != null)
                    {
                        OnFindingInteractables(false);
                        Debug.Log("Found Something");
                    }
                }
              
            }
        }
    }
}
