using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    
    public static event Action<bool> OnPlayerHiding;
    public static event Action<bool> OnPlayerIdle;

    [SerializeField]
    private float _speed;
    [SerializeField]
    private GameObject _camera;
    [SerializeField]
    private float _cameraSensitivity;    


    private float _rotateOnX;

    private CharacterController _controller;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();


        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Movement();
        CameraControl();

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void Movement()
    {
        float horInput = Input.GetAxis("Horizontal");
        float verInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horInput, 0f, verInput);

        Vector3 velocity = direction * _speed;

        velocity = transform.TransformDirection(velocity);

        _controller.Move(velocity * Time.deltaTime);
       

        if (velocity.x == 0.0f)
        {
            OnPlayerIdle(true);            
        }        
        else
        {
            OnPlayerIdle(false);
        }
    }

    void CameraControl()
    {
        float mouseY = Input.GetAxis("Mouse Y")* _cameraSensitivity* Time.deltaTime;
        float mouseX = Input.GetAxis("Mouse X")* _cameraSensitivity* Time.deltaTime;

        _rotateOnX -= mouseY;

        _rotateOnX = Mathf.Clamp(_rotateOnX, -90, 90);
        _camera.transform.localEulerAngles = new Vector3(_rotateOnX, 0f, 0f);

        Vector3 currRot = transform.localEulerAngles;
        currRot.y += mouseX;

        transform.localRotation = Quaternion.AngleAxis(currRot.y, Vector3.up);     

    }



}
