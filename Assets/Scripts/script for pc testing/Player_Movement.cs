using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public CharacterController Controller;
    public float speed = 10;
    public float spritSpeed = 25f;
    public float slidspeed = 30f;
    public float Gravity = -19.01f;
    public float jumpheight = 1f;
    public float speedX = 5f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
   
    CharacterController crouch;

    

    private void Start()
    {
        crouch = gameObject.GetComponent<CharacterController>();
        
    }

    void Update()
    {  
        //Ground check & Gravity 
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
       



        //jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpheight * -2f * Gravity);
        }

        //move
        Vector3 move = transform.right * x + transform.forward * z;
        Controller.Move(move * speed * Time.deltaTime);

        velocity.y += Gravity * Time.deltaTime;

        Controller.Move(velocity * Time.deltaTime);

        //sprint
        if (speed == 3f && Input.GetKey(KeyCode.LeftShift))
        {
            speed = spritSpeed;
        }
        else
        {
            speed = 3f;
        }




        //Crouch
        if (Input.GetKey("c"))
        {
            crouch.height = 0.1f;
        }
        else
        {
            crouch.height = 2f;
        }

        //sliding
        if (spritSpeed == 25f && Input.GetKey("c"))
        {

            transform.Translate(Vector3.forward * slidspeed * Time.deltaTime* speedX);
            if (Input.GetKeyDown(KeyCode.C) && slidspeed > 100f)
            {
                slidspeed = 0f;
            }
        }







        
    }
}