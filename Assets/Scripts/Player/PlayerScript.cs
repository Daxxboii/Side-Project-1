using UnityEngine;
using UnityEngine.UI;
using Scripts.Enemy.girlHostile;
using UnityEngine.Rendering.PostProcessing;
using DG.Tweening;

public class PlayerScript : MonoBehaviour
{
    public static bool is_Being_Chased;

    [Header("UI")] [SerializeField] private Sprite crouch;
    [SerializeField] private Sprite stand;
    [SerializeField] private GameObject Buttons_Canvas;

    public GameObject admenu;
    [SerializeField] private Image StandStateButton;


    [Header("Damage")] public Image hit_image;
    public Sprite hit_low;
    public Sprite hit_high;
    public PostProcessVolume Damage;

    [SerializeField] private GirlAiGhost GirlAI;
    [SerializeField] private Camera fpsCam;
    [SerializeField] private Transform Camera_Shake;
    [SerializeField] private Joystick joystick;


    [SerializeField] private Transform cameraTransform, ghost;
    [SerializeField] public CharacterController characterController;

    // Player settings
    [SerializeField] private float cameraSensitivity;

    [SerializeField]
    public float speed, Footsteps_frequency, CrouchSpeed, height, crouchHight, Health, Crouch_Interpolation_speed;

    private float TempSpeed;
    [HideInInspector] public bool isCrouching;
    private Vector3 move;
    public bool isDead;

    // Touch detection
    private int leftFingerId, rightFingerId;
    private float halfScreenWidth;

    // Camera control
    private Vector2 lookInput;
    private float cameraPitch;

    // Player movement
    private Vector2 moveTouchStartPosition;
    private Vector2 moveInput;

    // Audio 
    public AudioManager AudioM;

    public GameManager gameManager;

    Touch InitialTouch;
    [HideInInspector] public float x, z;


    // Start is called before the first frame update
    private void Start()
    {
        hit_image.gameObject.SetActive(true);
        TempSpeed = speed;
        characterController = gameObject.GetComponent<CharacterController>();
        TempSpeed = speed;

        // id = -1 means the finger is not being tracked
        leftFingerId = -1;
        rightFingerId = -1;

        // only calculate once
        halfScreenWidth = 1280 / 2;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Health < 75)
        {
            Health_Manager();
        }

        if (!isDead)
        {
            GetTouchInput();

            if (rightFingerId != -1)
            {
                LookAround();
            }

            LocoMotion();
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            gameManager.Pause();
        }
    }

    #region inputs

    private void GetTouchInput()
    {
        // Iterate through all the detected touches
        for (var i = 0; i < Input.touchCount; i++)
        {
            InitialTouch = Input.GetTouch(i);

            switch (InitialTouch.phase)
            {
                // Check each touch's phase
                case TouchPhase.Began when InitialTouch.position.x < halfScreenWidth && leftFingerId == -1:
                    // Start tracking the left finger if it was not previously being tracked
                    leftFingerId = InitialTouch.fingerId;

                    // Set the start position for the movement control finger
                    moveTouchStartPosition = InitialTouch.position;
                    break;
                case TouchPhase.Began:
                {
                    if (InitialTouch.position.x > halfScreenWidth && rightFingerId == -1)
                    {
                        // Start tracking the rightfinger if it was not previously being tracked
                        rightFingerId = InitialTouch.fingerId;
                    }

                    break;
                }
                case TouchPhase.Ended or TouchPhase.Canceled when InitialTouch.fingerId == leftFingerId:
                    // Stop tracking the left finger
                    leftFingerId = -1;
                    //  Debug.Log("Stopped tracking left finger");
                    break;
                case TouchPhase.Ended or TouchPhase.Canceled:
                {
                    if (InitialTouch.fingerId == rightFingerId)
                    {
                        // Stop tracking the right finger
                        rightFingerId = -1;
                        //   Debug.Log("Stopped tracking right finger");
                    }

                    break;
                }
                // Get input for looking around
                case TouchPhase.Moved when InitialTouch.fingerId == rightFingerId:
                    lookInput = InitialTouch.deltaPosition * cameraSensitivity * Time.deltaTime;
                    break;
                case TouchPhase.Moved:
                {
                    if (InitialTouch.fingerId == leftFingerId)
                    {
                        // calculating the position delta from the start position
                        moveInput = InitialTouch.position - moveTouchStartPosition;
                    }

                    break;
                }
                case TouchPhase.Stationary:
                {
                    // Set the look input to zero if the finger is still
                    if (InitialTouch.fingerId == rightFingerId)
                    {
                        lookInput = Vector2.zero;
                    }

                    break;
                }
            }
        }
    }

    private void LookAround()
    {
        // vertical (pitch) rotation
        cameraPitch = Mathf.Clamp(cameraPitch - lookInput.y, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0, 0);

        // horizontal (yaw) rotation
        transform.Rotate(transform.up, lookInput.x);
    }

    private void LocoMotion()
    {
        x = joystick.Horizontal;
        z = joystick.Vertical;


        if (joystick.Direction.x != 0 || joystick.Direction.y != 0)
        {
            move = x * transform.right + z * transform.forward;
        }
        else
        {
            move = Vector3.zero;
        }


        if (!characterController.isGrounded)
        {
            move += Physics.gravity * Time.deltaTime * 9.5f;
        }

        characterController.Move(move * Time.deltaTime * speed);
    }

    #endregion

    public void croutch()
    {
        isCrouching = !isCrouching;
        _crouch();
    }

    private void _crouch()
    {
        if (isCrouching)
        {
            DOVirtual.Float(characterController.height, crouchHight, Crouch_Interpolation_speed,
                    v => { characterController.height = v; }).SetEase(Ease.InSine)
                .OnComplete(() => { StandStateButton.sprite = crouch; });
        }
        else
        {
            DOVirtual.Float(characterController.height, height, Crouch_Interpolation_speed,
                    v => { characterController.height = v; }).SetEase(Ease.InSine)
                .OnComplete(() => { StandStateButton.sprite = stand; });
        }
    }


    public void SetSensi(float Sensitivity)
    {
        cameraSensitivity = Sensitivity;
    }


    public void PlayerTakeDamage(float hminus)
    {
        Health -= hminus;
        //  CameraShaker.Instance.ShakeOnce(10f, 10f, 0.1f, 1f);
        Camera_Shake.DOShakePosition(0.5f, 1, 10, 90, false, true);
    }

    private void Regenerate()
    {
        if (Health <= 75)
        {
            Health += Time.deltaTime * 2;
        }
    }

    private void Health_Manager()
    {
        Regenerate();
        //Set Hit Alpha
        var tempColor = hit_image.color;
        tempColor.a = (1 - Health / 75) / 2;
        hit_image.color = tempColor;
        //Set Hit Image
        hit_image.sprite = Health < 50 ? hit_high : hit_low;

        if (Health > 60)
        {
            Damage.weight = Mathf.Lerp(Damage.weight, 0f, Time.deltaTime * 3f);
        }
        else
        {
            Damage.weight = 1;
        }

        //Death
        if (!(Health <= 0)) return;
        GirlAI.Cooldown_period = 0;
        isDead = true;
        fpsCam.transform.LookAt(ghost);
    }

    void Player_death()
    {
        //   yield return new WaitForSeconds(death_timer);
        admenu.SetActive(true);
        Buttons_Canvas.SetActive(false);
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