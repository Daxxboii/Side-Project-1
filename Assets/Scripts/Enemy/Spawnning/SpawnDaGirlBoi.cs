using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Enemy.girlHostile;
namespace Scripts.Enemy
{
    namespace Spawn
    {
        public class SpawnDaGirlBoi : MonoBehaviour
        {
            [SerializeField]
            private Level[] _level;
            [SerializeField]
            private GameObject[] GirlGhost;
            [SerializeField]
            private int levelNo;
            [SerializeField]
            GirlAiGhost gAi;
            [System.Serializable]
            public class Level
            {
                public Transform _posOfObjective;
                public bool isOver;
                public float radious;
            }

            private void Awake()
            {
                
            }

            private void Update()
            {
                switch (levelNo)
                {
                    case 1:
                      LoadLevel(1);
                    break;
                    case 2:
                        LoadLevel(2);
                        break;
                    case 3:
                        LoadLevel(3);
                        break;
                    case 4:
                        LoadLevel(4);
                        break;
                    case 5:
                        LoadLevel(5);
                        break;
                    case 6:
                        LoadLevel(6);
                        break;
                    case 7:
                        LoadLevel(7);
                        break;
                    case 8:
                        LoadLevel(8);
                        break;
                    default:
                        Debug.Log("Enter a valid number boi");
                        break;
                }

            }
            void LoadLevel(int LevelNo)
            {
                if(levelNo <=6 || levelNo >= 1)
                {
                    
                }
                if (levelNo <= 8 || levelNo >= 7)
                {

                }
            }
            
        }
    }
}
