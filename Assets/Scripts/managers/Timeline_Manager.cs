 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using Scripts.Objects;
using Scripts.Buttons;
using TMPro;
using Scripts.Player;
namespace Scripts.Timeline

{
    public class Timeline_Manager : MonoBehaviour
    {
        public int delay;
        public GameObject skip;
        public PlayerScript ps;
       private string text,_text;
        [SerializeField]
        TextAsset subtitles,objectives;
        [SerializeField]
        private TextMeshProUGUI comments,objective_text;
        
        public int index,objective_index;
        [SerializeField]
        ObjectController oc;
        [SerializeField]
        ButtonOpen bo;
        [SerializeField]

        GameObject Button;
        public Enemy.Principal.AiFollow aiFollow;
        public Enemy.girlHostile.GirlAiGhost girl;


        public int Current_cutscene;
        [SerializeField]
        private TimelineAsset[] timeline_assets;

        [SerializeField]
        private PlayableDirector director;

        [SerializeField]
        private GameObject player,player_cam;

        public GameObject[] guides;
        [SerializeField]
        private GameObject cutscene_player,cutscene_cam;

        [SerializeField]
        private float y_offset;

        private Vector3 position;
        private Vector3 rotation,cam_rot;

        string[] lines, objective_lines;
       
        public Material princy;
        private void Start()
        {
              lines = subtitles.text.Split("\n"[0]);
            objective_lines = objectives.text.Split("\n"[0]);
           
           
            ObjectiveList();
            if (Current_cutscene >= 10)
            {
                princy.SetColor("_BaseColor", Color.white);
                aiFollow.angry = true;
            }
            else
            {
                princy.SetColor("_BaseColor", Color.black);
            }
            // Translate_Cutscene();
            if (Current_cutscene > 4)
            {
                girl.gameObject.SetActive(true);
                aiFollow.gameObject.SetActive(true);
                girl.agent.enabled = true;
                aiFollow.enabled = true;
            }
            text = lines[index];
        }

        private void FixedUpdate()
        {
            if (Current_cutscene >= 10)
            {
                aiFollow.angry = true;
                girl.angry = true;
            }

        }
        public void Translate_Player()
        {
            ps.gameObject.SetActive(false);
            position = cutscene_player.transform.position;
            rotation = cutscene_player.transform.rotation.eulerAngles;
            cam_rot = cutscene_cam.transform.rotation.eulerAngles;
            position.y -= y_offset;
         //   Debug.Log("ur mom");
          
            player.transform.position = position;
            player.transform.eulerAngles = rotation;
            player_cam.transform.eulerAngles = cam_rot;
            ps.gameObject.SetActive(true);
            if (Current_cutscene > 4)
            {
                girl.gameObject.SetActive(true);
                aiFollow.gameObject.SetActive(true);
                girl.agent.enabled = true;
                aiFollow.enabled = true;
            }
          
          

        }
        public void Translate_Cutscene()
        {
           if (Current_cutscene != 0)
            {
                position = guides[Current_cutscene-2].transform.position;
                rotation = guides[Current_cutscene-2].transform.rotation.eulerAngles;


                cutscene_player.transform.position = position;
                cutscene_player.transform.eulerAngles = rotation;
            }
          
        }

        public void TimeLine_Activator()
        {

            //  Debug.Log(Current_cutscene);
            if (Current_cutscene == 2)
            {
                girl.agent.enabled = false;
            }
           
        
            if (Current_cutscene != 2)
            {
                girl.gameObject.SetActive(false);
                aiFollow.gameObject.SetActive(false);
                aiFollow.enabled = false;
            }

            director.playableAsset = timeline_assets[Current_cutscene];
           
         

            if (oc.had != null)
            {
                oc.had.SetActive(false);
                oc.had = null;
            }
           
            Button.SetActive(false);
            if (Current_cutscene >= 10)
            {
                princy.SetColor("_BaseColor", Color.white);
            }
            else
            {
                princy.SetColor("_BaseColor", Color.black);
            }
 
            director.time = 0;
            director.Play();
            director.playableGraph.GetRootPlayable(0).SetSpeed(1);
            StartCoroutine("skipper");
           
            Current_cutscene++;
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
         //   Debug.Log(objective_index++);
            _text = objective_lines[objective_index++];
            objective_text.text = _text;
            skip.SetActive(false);
            //   Debug.Log("objective");

        }

        public void Skip()
        {
            skip.SetActive(true);
        }

        IEnumerator skipper()
        {
            yield return new WaitForSeconds(delay);
            Skip();
        }

        public void End()
        {
            director.playableGraph.GetRootPlayable(0).SetSpeed(5);
            skip.SetActive(false);
        }
    }
    
}

