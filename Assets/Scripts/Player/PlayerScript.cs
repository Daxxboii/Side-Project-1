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
       
        [SerializeField]
        Sprite crouch,stand;
        public GameObject admenu;
        [SerializeField]
        Image Button;
        public VolumeProfile volume;
        public float death_timer;
        public Image hit_image;
        public Sprite hit_low;
        public Sprite hit_high;
       
        [SerializeField]
        GirlAiGhost gi;
        [SerializeField] private Camera fpsCam;
        public static event Action<bool, int> PlayCutscene;
        public static event Action<bool, int> TellStory;
        [SerializeField] private Joystick joystick;
        

        [SerializeField] private Transform cameraTransform, ghost;
        [SerializeField] public CharacterController characterController;
        [SerializeField] private Animator camAnim;

        // Player settings
        [SerializeField] private float cameraSensitivity, deathAnimationtime;
        [SerializeField] public float TempSpeed, speed, CrouchSpeed, crawlSpeed, SprintSpeed, height, tempHeight, crouchHight, crawlheight, Health, RegenTimer;
        private bool isSprinting, isCrouching, IsCrawlling, canSprint;
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
            tempHeight = characterController.height;
            height = characterController.height;
            TempSpeed = speed;
            // id = -1 means the finger is not being tracked
            leftFingerId = -1;
            rightFingerId = -1;

            // only calculate once
            halfScreenWidth = Screen.width / 2;
            volume.TryGet(out vig);
            vig.color.Override(Color.black);
        }

        // Update is called once per frame
        void Update()
        {
            if (isCrouching == false)
            {
                speed = TempSpeed;
                if (characterController.height < tempHeight)
                {
                    characterController.height += 0.6f;
                }
               

            }
            else
            {
                speed = CrouchSpeed;
                characterController.height = crouchHight;
            }
            Health_Manager();
            if (Health <= 0)
            {
                gi.Cooldown_period = 0;
                isDead = true;
                fpsCam.transform.LookAt(ghost);
              
                StartCoroutine("Player_death");

            }
            if (!isDead)
            {
                Regenerate();
                cameraSensitivity = sensi;
                GetTouchInput();

                if (rightFingerId != -1)
                {
                 //   Debug.Log("Rotating");
                    LookAround();
                }
                LocoMotion();
            }
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
            
              
          /*  if (move.x == 0.0f)
            {
                OnPlayerIdle(true);
            }
            else
            {
                OnPlayerIdle(false);
            }*/
        }
        public void croutch()
        {
            isCrouching = !isCrouching;
          
        }
        public void Crawl()
        {
            IsCrawlling = !IsCrawlling;
            if (IsCrawlling == false)
            {
                speed = TempSpeed;
                characterController.height = tempHeight;
            }
            else
            {
                speed = crawlSpeed;
                characterController.height = crawlheight;

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

        static public void SetSensi(float S)
        {
            sensi = S;
        }
        IEnumerator SprintS()
        {
            yield return new WaitForSeconds(3f);
            speed = TempSpeed;
        }

        public void PlayerTakeDamage(float hminus)
        {
            
            Health -= hminus;
         //   Debug.Log("OWO");
        }

        public void ChangeUI()
        {
            if (isCrouching)
            {
                Button.sprite = crouch;
            }
            else
            {
                 Button.sprite = stand;
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
            var tempColor = hit_image.color;
            tempColor.a = (1-Health/75)/2;
            hit_image.color = tempColor;
            if (Health < 50){
                hit_image.sprite = hit_high;
            }
            else
            {
                hit_image.sprite = hit_low;
            }
        }
        IEnumerator Player_death()
        {
            AudioM.Player_stop();
            yield return new WaitForSeconds(death_timer);
            admenu.SetActive(true);
            Time.timeScale = 0;
        }

       
    }
}
