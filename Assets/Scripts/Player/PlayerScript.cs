using System.Collections;
using UnityEngine;
using System;
using unityCore.Audio;
using UnityEngine.UI;
using System.Threading;
using Scripts.Objects;
using Scripts.Enemy.girlHostile;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
namespace Scripts.Player
{
   
    public class PlayerScript : MonoBehaviour
    {
        //dont touch this ever and i mean it if someone edit this i will kill tharm :angry-emoje:
        private static float sensi = 10;

        [Header("UI")]
        [SerializeField] private Sprite crouch;
        [SerializeField] private Sprite stand;
        
        public GameObject admenu;
        [SerializeField]
        private Image StandStateButton;

        public VolumeProfile volume;
        public float death_timer;
        public Image hit_image;
        public Sprite hit_low;
        public Sprite hit_high;
       
        [SerializeField]
        private GirlAiGhost GirlAI;
        [SerializeField] private Camera fpsCam;
      
        [SerializeField] private Joystick joystick;
        

        [SerializeField] private Transform cameraTransform, ghost;
        [SerializeField] public CharacterController characterController;
        [SerializeField] private Animator camAnim;

        // Player settings
        [SerializeField] private float cameraSensitivity;
        [SerializeField] public float TempSpeed, speed, CrouchSpeed, crawlSpeed, SprintSpeed, height, crouchHight, Health, RegenTimer;
        private bool  isCrouching;
        private Vector3 move;
        public bool isDead;


        // Touch detection
        private int leftFingerId, rightFingerId;
        private float halfScreenWidth;
         Vignette vig;


        // Camera control
        private Vector2 lookInput;
        private float cameraPitch;
       
        // Player movement
        private Vector2 moveTouchStartPosition;
        private Vector2 moveInput;

        // Audio 
        public AudioManager AudioM;




        // Start is called before the first frame update
        void Start()
        {
            hit_image.gameObject.SetActive(true);
            TempSpeed = speed;
            characterController = gameObject.GetComponent<CharacterController>();
           // tempHeight = characterController.height;
          //  height = characterController.height;
            TempSpeed = speed;
            // id = -1 means the finger is not being tracked
            leftFingerId = -1;
            rightFingerId = -1;

            // only calculate once
            halfScreenWidth = Screen.width / 2;

            //volume
            volume.TryGet(out vig);
            vig.color.Override(Color.black);
        }

        // Update is called once per frame
        void Update()
        {
            Health_Manager();
            if (!isDead)
            {
                Regenerate();
              
                GetTouchInput();

                if (rightFingerId != -1)
                {
                    LookAround();
                }
                LocoMotion();
            }
        }

        #region inputs
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
                          //  Debug.Log("Stopped tracking left finger");
                        }
                        else if (t.fingerId == rightFingerId)
                        {
                            // Stop tracking the right finger
                            rightFingerId = -1;
                         //   Debug.Log("Stopped tracking right finger");
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

            if (joystick.Direction.x != 0 || joystick.Direction.y != 0 )
            {
                camAnim.SetBool("IsMoving", true);
                if (!isCrouching)
                {
                    AudioM.Player_walk();
                }
               
            }
            else
            {
                camAnim.SetBool("IsMoving", false);
                AudioM.Player_stop();
            }

            move = x * transform.right + z * transform.forward;

            


            if (characterController.isGrounded == false)
            {
                move += Physics.gravity * Time.deltaTime * 9.5f;
            }

         
           characterController.Move(move * Time.deltaTime * speed);
         
        }
        #endregion
        public void croutch()
        {
            isCrouching = !isCrouching;

            if (isCrouching == false)
            {
                speed = TempSpeed;
            
                    characterController.height = height;
                
            }
            else
            {
                speed = CrouchSpeed;
                characterController.height = crouchHight;
            }
        }

       
      

         public void SetSensi(float S)
        {
            sensi = S;
            cameraSensitivity = sensi;
        }


        

        public void PlayerTakeDamage(float hminus)
        {
            Health -= hminus;
        }

        public void ChangeUI()
        {
            if (isCrouching)
            {
                StandStateButton.sprite = crouch;
            }
            else
            {
                 StandStateButton.sprite = stand;
            }
        }
      
        private void Regenerate()
        {
            if(Health <=75)
            {
                RegenTimer += Time.deltaTime;
                Health += RegenTimer;
                RegenTimer = 0;
            }
        }
        private void Health_Manager()
        {
            vig.color.Override(new Color((1-Health/75)/2, 0, 0));
            //Set Hit Alpha
            var tempColor = hit_image.color;
            tempColor.a = (1-Health/75)/2;
            hit_image.color = tempColor;

            //Set Hit Image
            if (Health < 50){
                hit_image.sprite = hit_high;
            }
            else
            {
                hit_image.sprite = hit_low;
            }
            //Death
            if (Health <= 0)
            {
                GirlAI.Cooldown_period = 0;
                isDead = true;
                fpsCam.transform.LookAt(ghost);

            }
        }
        void Player_death()
        {
            AudioM.Player_stop();
         //   yield return new WaitForSeconds(death_timer);
            admenu.SetActive(true);
            Time.timeScale = 0;
        }

        private void OnEnable()
        {
            AnimationEvents.kill += Player_death;
        }

        private void OnDisable()
        {
            AnimationEvents.kill -= Player_death;
        }
    }
}
