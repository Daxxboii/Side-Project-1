using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timeline_Manager : MonoBehaviour
{
    [SerializeField]
   private GameObject player;
    [SerializeField]
   private GameObject cutscene_player;

    private Vector3 position;
    private Vector3 rotation;
  public  void Translate_Player()
    {
        position = cutscene_player.transform.position;
       rotation = cutscene_player.transform.rotation.eulerAngles;
      
        player.transform.position = position;
        player.transform.eulerAngles = rotation;
    }
    public void Translate_Cutscene()
    {
        position = player.transform.position;
        rotation = player.transform.rotation.eulerAngles;

        cutscene_player.transform.position = position;
        cutscene_player.transform.eulerAngles = rotation;
    }
}

