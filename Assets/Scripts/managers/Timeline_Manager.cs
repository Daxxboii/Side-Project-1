 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using Scripts.Objects;
using Scripts.Buttons;
using TMPro;
namespace Scripts.Timeline

{
    public class Timeline_Manager : MonoBehaviour
    {
       
       private string text;
        [SerializeField]
        TextAsset subtitles,objectives;
        [SerializeField]
        private TextMeshProUGUI comments,objective_text;
        [HideInInspector]
        public int index,objective_index;
        [SerializeField]
        ObjectController oc;
        [SerializeField]
        ButtonOpen bo;
        [SerializeField]

        GameObject Button;

       
        public int Current_cutscene;
        [SerializeField]
        private TimelineAsset[] timeline_assets;

        [SerializeField]
        private PlayableDirector director;

        [SerializeField]
        private GameObject player,player_cam;
        [SerializeField]
        private GameObject cutscene_player,cutscene_cam;

        [SerializeField]
        private float y_offset;

        private Vector3 position;
        private Vector3 rotation,cam_rot;

        string[] lines, objective_lines;

        private void Start()
        {
              lines = subtitles.text.Split("\n"[0]);
            objective_lines = objectives.text.Split("\n"[0]);
            ObjectiveList();
        }
        public void Translate_Player()
        {
            position = cutscene_player.transform.position;
            rotation = cutscene_player.transform.rotation.eulerAngles;
            cam_rot = cutscene_cam.transform.rotation.eulerAngles;
            position.y -= y_offset;

            player.transform.position = position;
            player.transform.eulerAngles = rotation;
            player_cam.transform.eulerAngles = cam_rot;
        }
        public void Translate_Cutscene()
        {
            position = player.transform.position;
            rotation = player.transform.rotation.eulerAngles;
            position.y += y_offset;

            cutscene_player.transform.position = position;
            cutscene_player.transform.eulerAngles = rotation;
        }

        public void TimeLine_Activator()
        {

          //  Debug.Log(Current_cutscene);
            Current_cutscene++;

            director.playableAsset = timeline_assets[Current_cutscene];
            director.time = 0;
            director.Play();
            oc.had.SetActive(false);
            oc.had = null;
            Button.SetActive(false);



        }
      public  void ReadFile()
        {
            text = lines[index++];
         //   Debug.Log(text);
            comments.text = text;
        }
        public void Silence()
        {
         //   Debug.Log("sike");

            comments.text = "";
        }
        public void ObjectiveList()
        {
            text = objective_lines[objective_index++];
            objective_text.text = text;
     
        }
    }
    
}

