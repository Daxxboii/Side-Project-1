using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using Scripts.Objects;
using Scripts.Buttons;
using TMPro;
using UnityEngine.UI;
using Scripts.Player;
using DG.Tweening;
namespace Scripts.Timeline

{
    public class Timeline_Manager : MonoBehaviour
    {
        [Header("Scripts")]
        [SerializeField] public AdManager adManager;
        [SerializeField] private ObjectController ObjectControllerScript;
        [SerializeField] private ButtonOpen ButtonOpenScript;
        [SerializeField]public PlayerScript _PlayerScript;
        [SerializeField] public Enemy.Principal.AiFollow aiFollow;
        [SerializeField]public Enemy.girlHostile.GirlAiGhost girl;


        [Header("Buttons")]
        [SerializeField] public GameObject SkipButton;
        [SerializeField]private  GameObject Timeline_Button;


        [Header("Text & Text Files")]
        [SerializeField]private TextAsset subtitles,objectives;
        [SerializeField]private TextMeshProUGUI comments,objective_text;
        private string text,_text;

        [Header("Arrays")]
        [SerializeField]private TimelineAsset[] timeline_assets;
        [SerializeField]public GameObject[] guides;


        [Header("Components")]
        [SerializeField]private PlayableDirector director;
        public Material princy;

        [Header("GameObjects")]
        [SerializeField]private GameObject player,player_cam;
        [SerializeField]private GameObject cutscene_player,cutscene_cam;
        [HideInInspector]public int index,objective_index,Current_cutscene;

        [Header("Variables")]
        [SerializeField]private float y_offset;
        [SerializeField]private float skip_speed;

        [Header("UI")]
        [SerializeField] private RectTransform Top;
        [SerializeField] private RectTransform Bottom;
        [SerializeField] private Image Objectives;
      /*  [HideInInspector] private Vector2 startposUI, startscaleUI;
        [SerializeField] private Vector3 endposUI, endScaleUI;*/


        private Vector3 position;
        private Vector3 rotation,cam_rot;
        private Vector2 target;
        string[] lines, objective_lines;
       
        private void Start()
        {
            // startposUI = Objectives.anchoredPosition;
            //startscaleUI = Objectives.localScale;
            target = new Vector2(0f, 50f);
            lines = subtitles.text.Split("\n"[0]);
            objective_lines = objectives.text.Split("\n"[0]);
            ObjectiveList();
            if (Current_cutscene >= 10)
            {
                princy.color = Color.white;
                aiFollow.angry = true;
                aiFollow.angry = true;
                girl.ChangeGirl();
            }
            else
            {
                princy.color = Color.black;
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
        public void Translate_Player()
        {
            player.SetActive(false);
            position = cutscene_player.transform.position;
            rotation = cutscene_player.transform.rotation.eulerAngles;
            cam_rot = cutscene_cam.transform.rotation.eulerAngles;
            position.y -= y_offset;
          
            player.transform.position = position;
            player.transform.eulerAngles = rotation;
            player_cam.transform.eulerAngles = cam_rot;
            _PlayerScript.gameObject.SetActive(true);
            Hide_Bars();
            if (Current_cutscene > 4)
            {
                girl.gameObject.SetActive(true);
                aiFollow.gameObject.SetActive(true);
                girl.agent.enabled = true;
                aiFollow.enabled = true;
            }
            HideSkip();
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
            ShowBars();
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
           
         

            if (ObjectControllerScript.had != null)
            {
                ObjectControllerScript.had.SetActive(false);
                ObjectControllerScript.had = null;
            }
           
            Timeline_Button.SetActive(false);
            if (Current_cutscene >= 10)
            {
                princy.color = Color.white;
                aiFollow.angry = true;
                aiFollow.angry = true;
                girl.ChangeGirl();
            }
            else
            {
                 princy.color = Color.black;

            }

            director.time = 0;
            director.Play();
            director.playableGraph.GetRootPlayable(0).SetSpeed(1);
            //Start Skip Countdown
            skipper();
           
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
            _text = objective_lines[objective_index++];
            objective_text.text = _text;
            HideSkip();
            /*  Objectives.localScale = endScaleUI;
               Objectives.anchoredPosition = endposUI;
              Objectives.DOScale(startscaleUI.x, 0.5f).SetUpdate(true).SetEase(Ease.InSine);
              Objectives.DOAnchorPos(startposUI, 0.5f, false).SetUpdate(true).SetEase(Ease.InSine);*/
            var tempColor = Objectives.color;
            tempColor.a = 0f;
            Objectives.color = tempColor;
            Objectives.DOFade(1, 0.25f).SetEase(Ease.InSine).SetLoops(3, LoopType.Yoyo);
        }

        private void ActivateSkip()
        {
            SkipButton.SetActive(true);
        }

        //Called when timeline Starts
        void skipper()
        {
            ActivateSkip();
        }
        // TO fast forward timeline (called when button is used)
        public void End()
        {
            director.playableGraph.GetRootPlayable(0).SetSpeed(skip_speed);
            SkipButton.SetActive(false);
        }

        public void HideSkip()
        {
            SkipButton.SetActive(false);
        }

        void Hide_Bars()
		{
            Top.DOSizeDelta(Vector2.zero, 1).SetEase(Ease.OutSine);
            Bottom.DOSizeDelta(Vector2.zero, 1).SetEase(Ease.OutSine);
        }

        void ShowBars()
		{
            Top.DOSizeDelta(target, 1).SetEase(Ease.InSine);
            Bottom.DOSizeDelta(target, 1).SetEase(Ease.InSine);
        }
    }
    
}

