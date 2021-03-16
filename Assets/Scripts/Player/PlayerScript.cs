using System.Collections;
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
        public static event Action<bool, int> PlayCutscene;
        public static event Action<bool, int> TellStory;
        [SerializeField] private Joystick joystick;
        

        [SerializeField] private Transform cameraTransform;
        [SerializeField] private CharacterController characterController;
        [SerializeField] private Animator camAnim;

        // Player settings
        [SerializeField] private float cameraSensitivity;
        [SerializeField] float TempSpeed, speed, CrouchSpeed, crawlSpeed, SprintSpeed, height, tempHeight, crouchHight, crawlheight, Health, RegenTimer;
        private bool isSprinting, isCrouching, IsCrawlling, isDead, canSprint;
        private Vector3 move;
        // Touch detection
        private int leftFingerId, rightFingerId;
        private float halfScreenWidth;

        // Camera control
        private Vector2 lookInput;
        private float cameraPitch;

        // Player movement
        private Vector2 moveTouchStartPosition;
        private Vector2 moveInput;

        // Start is called before the first frame update
        void Start()
        {
            TempSpeed = speed;
            characterController = gameObject.GetComponent<CharacterController>();
            tempHeight = characterController.height;
            height = characterController.height;
            TempSpeed = speed;
            // id = -1 means the finger is not being tracked
            leftFingerId = -1;
            rightFingerId = -1;

            // only calculate once
            halfScreenWidth = Screen.width / 2;
        }

        // Update is called once per frame
        void Update()
        {
            GetTouchInput();

            if (rightFingerId != -1)
            {
                Debug.Log("Rotating");
                LookAround();
            }
            LocoMotion();
        }

        void GetTouchInput()
        {
            // Iterate through all the detected touches
            for (int i = 0; i < Input.touchCount; i++)
            {

                Touch t = Input.GetTouch(i);

                // Check each touch's phase
                switch (t.phase)
                {
                    case TouchPhase.Began:

                        if (t.position.x < halfScreenWidth && leftFingerId == -1)
                        {
                            // Start tracking the left finger if it was not previously being tracked
                            leftFingerId = t.fingerId;

                            // Set the start position for the movement control finger
                            moveTouchStartPosition = t.position;
                        }
                        else if (t.position.x > halfScreenWidth && rightFingerId == -1)
                        {
                            // Start tracking the rightfinger if it was not previously being tracked
                            rightFingerId = t.fingerId;
                        }

                        break;
                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:

                        if (t.fingerId == leftFingerId)
                        {
                            // Stop tracking the left finger
                            leftFingerId = -1;
                            Debug.Log("Stopped tracking left finger");
                        }
                        else if (t.fingerId == rightFingerId)
                        {
                            // Stop tracking the right finger
                            rightFingerId = -1;
                            Debug.Log("Stopped tracking right finger");
                        }

                        break;
                    case TouchPhase.Moved:

                        // Get input for looking around
                        if (t.fingerId == rightFingerId)
                        {
                            lookInput = t.deltaPosition * cameraSensitivity * Time.deltaTime;
                        }
                        else if (t.fingerId == leftFingerId)
                        {

                            // calculating the position delta from the start position
                            moveInput = t.position - moveTouchStartPosition;
                        }

                        break;
                    case TouchPhase.Stationary:
                        // Set the look input to zero if the finger is still
                        if (t.fingerId == rightFingerId)
                        {
                            lookInput = Vector2.zero;
                        }
                        break;
                }
            }
        }

        void LookAround()
        {

            // vertical (pitch) rotation
            cameraPitch = Mathf.Clamp(cameraPitch - lookInput.y, -90f, 90f);
            cameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0, 0);

            // horizontal (yaw) rotation
            transform.Rotate(transform.up, lookInput.x);
        }

        void LocoMotion()
        {
            
            float x = joystick.Horizontal;
            float z = joystick.Vertical;

            if (joystick.Direction.x > 0 || joystick.Direction.y > 0)
            {
                camAnim.SetBool("IsMoving", true);
            }
            else
            {
                camAnim.SetBool("IsMoving", false);
            }

            move = x * transform.right + z * transform.forward;



            if (characterController.isGrounded == false)
            {
                move += Physics.gravity * Time.deltaTime * 9.5f;
            }
            characterController.Move(move * Time.deltaTime * speed);

        }
        public void croutch()
        {
            isCrouching = !isCrouching;
            if(isCrouching == false)
            {
                speed = TempSpeed;
                characterController.height = Mathf.Lerp(crouchHight, tempHeight, 1f *Time.deltaTime); ;
            }
            else
            {
                speed = CrouchSpeed;
                characterController.height = Mathf.Lerp(tempHeight, crouchHight, 1f * Time.deltaTime); ;
            }
        }
        public void Crawl()
        {
            IsCrawlling = !IsCrawlling;
            if (IsCrawlling == false)
            {
                speed = TempSpeed;
                characterController.height = Mathf.Lerp(crawlheight, tempHeight, 1f * Time.deltaTime);
            }
            else
            {
                speed = crawlSpeed;
                characterController.height = Mathf.Lerp(tempHeight, crawlheight, 1f * Time.deltaTime);
            }
        }
        public void Sprint()
        {
            if(canSprint == true)
            {
                speed = SprintSpeed;
                canSprint = false;
                StartCoroutine(SprintS());
            }
        }
        IEnumerator SprintS()
        {
            yield return new WaitForSeconds(3f);
            speed = TempSpeed;
        }

    }
}
