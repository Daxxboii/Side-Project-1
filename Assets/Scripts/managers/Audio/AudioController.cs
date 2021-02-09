
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
            }
            private void Dispose()
            {

            }
            #endregion

        }
    }
}