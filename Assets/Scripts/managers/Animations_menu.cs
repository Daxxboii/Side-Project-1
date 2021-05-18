using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations_menu : MonoBehaviour
{
    [SerializeField]
    private Animator cam,settings,menu,save_manager,door;
   
   public void _Back()
    {
        cam.SetInteger("State", 0);
        settings.SetBool("Open", false);
        menu.SetBool("Open", true);
        save_manager.SetBool("Open", false);
    }

    public void _Start()
    {
        cam.SetInteger("State", 1);
        save_manager.SetBool("Open", true);
       
    }

    public void Settings()
    {
        cam.SetInteger("State", 2);
        settings.SetBool("Open", true);
        menu.SetBool("Open", false);
    }

    public void _Exit()
    {
        cam.SetInteger("State", 3);
        door.SetBool("Open", true);
    }
    public void _Credits()
    {
        cam.SetInteger("State", 4);
    }
   
   public void Start_2()
    {
        cam.SetInteger("State", 5);
        door.SetBool("IsOpen", true);
    }

    public void About()
    {
        cam.SetInteger("State", 6);
    }
    public void Gamemode()
    {
        cam.SetInteger("State", 7);
    }
}
