
using UnityEngine;
namespace unityCore
{
    namespace Audio
    {
        public class TestAudio : MonoBehaviour
        {
            public AudioController audioController;

            private void Update()
            {
                if(Input.GetKeyUp(KeyCode.T))
                {
                    audioController.PlayAudio(AudioType.ST_01);
                }
                if (Input.GetKeyUp(KeyCode.R))
                {
                    audioController.StopAudio(AudioType.ST_01);
                }
                if (Input.GetKeyUp(KeyCode.W))
                {
                    audioController.RestartAudio(AudioType.ST_01);
                }
            }

        }
    }
}
