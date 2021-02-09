
using System.Collections;
using UnityEngine;

namespace unityCore
{
    namespace Audio
    {
        public class AudioController : MonoBehaviour
        {
            //members
            public static AudioController instance;
            public bool debug;
            public AudioTrack[] tracks;

            private Hashtable m_audioTable;
            private Hashtable m_jobTable;



            [System.Serializable]
            public class AudioObject 
            { 
                public AudioType type;
                public AudioClip Clip;
            }




            [System.Serializable]
            public class AudioTrack 
            {
                public AudioSource source;
                public AudioObject[] audio;
            }







            #region Unity Functions
            private void Awake()
            {
                // instance
                if(!instance)
                {
                    Configure();
                    //configure

                }
            }
            private void OnDisable()
            {
                Dispose();
            }
            #endregion



            #region Public Functions

            public void PlayAudio(AudioType _type)
            {

            }

            public void StopAudio(AudioType _type)
            {

            }

            public void RestartAudio(AudioType _type)
            {

            }

            #endregion



            #region private Functions
            private void log(string _msg)
            {
                if (!debug) return;
                Debug.Log("[AudioController]: " + _msg);
            }
            private void logWarning(string _msg)
            {
                if (!debug) return;
                Debug.LogWarning("[AudioController]: " + _msg);
            }
            private void Configure()
            {
                instance = this;
                m_audioTable = new Hashtable();
                m_jobTable = new Hashtable();
                GenerateAudio();
            }
            private void Dispose()
            {
                
            }
            private void GenerateAudio()
            {
                foreach (AudioTrack _tracks in tracks)
                {
                    foreach (AudioObject _obj in _tracks.audio)
                    {
                        if (m_audioTable.ContainsKey(_obj.type))
                        {
                            m_audioTable.Add(_obj.type, _tracks);
                            log("registreing Audio lamo : " + _obj.type);
                        }
                    }
                }
            }
            #endregion

        }
    }
}