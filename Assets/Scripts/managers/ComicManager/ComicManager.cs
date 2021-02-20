using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Player;

namespace unityCore
{
    namespace Comic
    {
        public class ComicManager : MonoBehaviour
        {
            [SerializeField]
            GameObject panel, canvas;
            [SerializeField]
            chapter[] ch;
            [SerializeField]
            Material pannelMaterial;
            int eventNumber;
            

            [System.Serializable]
            public class chapter
            {
                public int chapternumber;
                public pages[] _pages;
            }

            [System.Serializable]
            public class pages
            {
                public Material coverPage;
                public Material PageMeterial;
            }

            private void Awake()
            {
                pannelMaterial = panel.gameObject.GetComponent<Material>();
                panel.SetActive(false);
                canvas.SetActive(false);
            }

            private void OnEnable()
            {
                PlayerScript.TellStory += ComicBookOpen;
            }

            void ComicBookOpen(bool  open, int chapter)
            {
                if (open)
                {
                    OpenChapter(chapter);
                }
                else
                    return;
            }

            void OpenChapter(int chapterNumber)
            {
                panel.SetActive(true);
                canvas.SetActive(true);

                pannelMaterial = ch[1]._pages[1].coverPage;
            }
        }
    }
}
