using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_inspection : MonoBehaviour
{
    PlayerControls controls;

    //Variables
    float x, y,speed;
    // Start is called before the first frame update
    void Awake()
    {
        controls = new PlayerControls();
        controls.Inspection.RotateX.performed += ctx => Rotate();
        controls.Inspection.RotateY.performed += ctx => Rotate();
        controls.Inspection.Mouse.performed += ctx => Rotate();
    }

    // Update is called once per frame
    void Rotate()
    {
        
    }

    void Activate()
    {
        controls.Inspection.Enable();
    }

    void Deactivate()
    {
        controls.Inspection.Disable();
    }
}
