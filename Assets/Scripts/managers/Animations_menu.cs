using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations_menu : MonoBehaviour
{
    [SerializeField]
    private Animator camera,settings,menu,save_manager;
   
   public void _Back()
    {
        camera.SetInteger("State", 0);
        settings.SetBool("Open", false);
        menu.SetBool("Open", true);
        save_manager.SetBool("Open", false);
    }

    public void _Start()
    {
        camera.SetInteger("State", 1);
        save_manager.SetBool("Open", true);
    }

    public void Settings()
    {
        camera.SetInteger("State", 2);
        settings.SetBool("Open", true);
        menu.SetBool("Open", false);
    }

    public void _Exit()
    {
        camera.SetInteger("State", 3);
    }
    public void _Credits()
    {
        camera.SetInteger("State", 4);
    }
   
   public void Start_2()
    {
        camera.SetInteger("State", 5);
    }

    public void About()
    {
        camera.SetInteger("State", 6);
    }
}
