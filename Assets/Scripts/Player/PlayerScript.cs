using System.Collections;
using UnityEngine;
using System;
using unityCore.Audio;
using UnityEngine.UI;
using Scripts.Enemy.girlHostile;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;
namespace Scripts.Player
{
   
    public class PlayerScript : MonoBehaviour
    {
       
        private static float sensi = 10;

        //Input
        [SerializeField] PlayerControls controls;

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
        float mouseX, mouseY;

        [SerializeField] private Joystick joystick;
        

        [SerializeField] private Transform cameraTransform, ghost;
        [SerializeField] public CharacterController characterController;
        [SerializeField] private Animator camAnim;

        // Player settings
        [SerializeField] private float cameraSensitivity;
        [SerializeField] public float TempSpeed, speed, CrouchSpeed, crawlSpeed, SprintSpeed, height, crouchHight, Health, RegenTimer;
        private bool  isCrouching, canSprint;
        private Vector3 move;
        public bool isDead;


        
         Vignette vig;


        // Camera control
        private Vector2 lookInput;
        private float cameraPitch;
       
        // Player movement
        private Vector2 moveInput;

        // Audio 
        public AudioManager AudioM;


        private void Awake()
        {
            controls = new PlayerControls();
            controls.Controls.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
            controls.Controls.Move.canceled += ctx => moveInput = Vector2.zero;
            controls.Controls.Rotate.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
            controls.Controls.Rotate.canceled += ctx => lookInput = Vector2.zero;
            controls.Controls.Crouch.performed += ctx => croutch();
        }

        // Start is called before the first frame update
        void Start()
        {
            hit_image.gameObject.SetActive(true);
            characterController = gameObject.GetComponent<CharacterController>();
            TempSpeed = speed;


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
                LookAround();
                LocoMotion();
            }
        }

        #region inputs
       

        void LookAround()
        {

            //Mouse input
            mouseX += lookInput.x * cameraSensitivity * Time.deltaTime;
            mouseY += lookInput.y * cameraSensitivity * Time.deltaTime;
            mouseY = Mathf.Clamp(mouseY, -90f, 90f);
            transform.eulerAngles = new Vector3(0, mouseX, 0);
            cameraTransform.transform.localRotation = Quaternion.Euler(-mouseY, 0, 0);
        }

        void LocoMotion()
        {   
            float x = moveInput.x;
            float z = moveInput.y;

            if (moveInput!=Vector2.zero)
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

            move = transform.right * x + transform.forward * z;

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

            //ChangeUI();
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

         public void SetSensi(float S)
        {
            sensi = S;
            cameraSensitivity = sensi;
        }


        IEnumerator SprintS()
        {
            yield return new WaitForSeconds(3f);
            speed = TempSpeed;
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

              //  StartCoroutine("Player_death");

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
            controls.Controls.Enable();
        }

        private void OnDisable()
        {
            AnimationEvents.kill -= Player_death;
            controls.Controls.Disable();
        }
    }
}
