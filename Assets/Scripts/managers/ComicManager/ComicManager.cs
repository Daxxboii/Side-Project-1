using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Player;
using UnityEngine.UI;
using unityCore.Audio;

namespace unityCore
{
    namespace Comic
    {
        public class ComicManager : MonoBehaviour
        {
            [SerializeField]
            GameObject panel, canvas;
            [SerializeField]
            Sprite panelImage;
            [SerializeField]
            int n = 0;
            [SerializeField]
            chapter[] ch;

            int CurChapter;
            
            

            [System.Serializable]
            public class chapter
            {
                public pages[] _pages;
            }

            [System.Serializable]
            public class pages
            {
                public Sprite PageImage;
            }

            private void Awake()
            {
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
                    panel.SetActive(true);
                    canvas.SetActive(true);
                    CurChapter = chapter;
                    StartComic(chapter);
                }
                else
                {
                    ExtiComic();
                }
                    
            }
            private void change()
            {
                panel.gameObject.gameObject.GetComponent<Image>().sprite = panelImage;
            }
            public void StartComic(int chapterNumber)
            {
                panelImage = ch[chapterNumber]._pages[0].PageImage;
                change();
            }
            public void Next()
            {
                if(n == ch[CurChapter]._pages.Length - 1)
                { 
                    n = 0;
                    panelImage = ch[CurChapter]._pages[n].PageImage;
                    change();
                }
                else
                {
                    n++;
                    panelImage = ch[CurChapter]._pages[n].PageImage;
                    change();
                }

            }

            public void back()
            {
                if (n == 0)
                {
                    n = 2;
                    panelImage = ch[CurChapter]._pages[n].PageImage;
                    change();
                }
                else
                {
                    n--;
                    panelImage = ch[CurChapter]._pages[n].PageImage;
                    change();
                }
            }
            public void ExtiComic()
            {
                panel.SetActive(false);
                canvas.SetActive(false);
            }

        }
    }
}
