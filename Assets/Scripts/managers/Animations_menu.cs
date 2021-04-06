using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations_menu : MonoBehaviour
{
    [SerializeField]
    private Animator camera;

   public void Awake()
    {
        camera.SetInteger("State", 0);
    }

    public void _Start()
    {
        camera.SetInteger("State", 1);
    }

    public void Settings()
    {
        camera.SetInteger("State", 2);
    }

    public void _Exit()
    {
        camera.SetInteger("State", 3);
    }

}
