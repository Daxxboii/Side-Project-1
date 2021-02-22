using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Scripts.Player;
using unityCore.Audio;
namespace unityCore
{
    namespace Events
    {
        public class EventManager : MonoBehaviour
        {
            [SerializeField]
            VideoPlayer CutscenePlayer;

            

            public EventManager instance;

            [SerializeField]
            StoryBoard storyBoard;


            [System.Serializable]
            public class StoryBoard
            {
                public Levels[] levels;
            }
            [System.Serializable]
            public class Levels
            {
                public LevelsExpanded[] subLevels;
            }
            [System.Serializable]
            public class LevelsExpanded
            {
                public VideoClip cutscene;
                public AudioClip ac;
                public bool PrincipalIsActive;
                public GameObject[] inaccessableObjects;
            }


            #region unity functions
            private void Awake()
            {
                if(!instance)
                {
                    Configure();
                }
            }

            #endregion

            #region private functions
            void Configure()
            {
                instance = this;
                PlayerScript.PlayCutscene += InatilizeCutscene;
            }
            #endregion

            #region public Functions
            // for cut scene
            void InatilizeCutscene(bool Play, int Level)
            {
                if(Play)
                {
                    playCutscene(Level);
                }
            }

            void playCutscene(int level)
            {
                VideoClip vc = storyBoard.levels[level].subLevels[level].cutscene;
                CutscenePlayer.Play(vc);
            }

            // for audio playing

            // for activision of un accessable object


            #endregion
        }
    }
}
