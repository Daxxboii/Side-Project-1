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
            public ComicManager instance;

            public Comic[] comic;

            private Hashtable m_ComicTable;
            private Hashtable m_jobtable;

            enum ComicAction
            {
                next,
                previous,
                restart,
                Exit
            }

            [System.Serializable]
            public class Comic
            {
                public Chapter[] chapter;
            }

            [System.Serializable]
            public class Chapter
            {
                public Pages[] pages;
            }
            [System.Serializable]
            public class Pages 
            {
                public Material page;
            }

            #region Unity Functions
            private void Awake()
            {
                if (!instance)
                {
                    Configure();
                }
            }
            #endregion

            #region Public Functions

            #endregion

            #region Private functions
            private void Configure()
            {
                instance = this;
                m_ComicTable = new Hashtable();
                m_jobtable = new Hashtable();
                GenerateComicTable();


            }
            private void GenerateComicTable()
            {
                foreach(Comic _comic in comic)
                {
                    foreach(Chapter _chapter in _comic.chapter)
                    {
                        foreach (Pages _pages in _chapter.pages)
                        {
                            if(m_ComicTable.ContainsKey(_pages.page))
                            {
                                Debug.Log("page allredy exist");
                            }
                            else
                            {
                                m_ComicTable.Add(_pages.page, _chapter);
                            }
                        }
                    }
                }
            }
            #endregion

        }
    }
}
