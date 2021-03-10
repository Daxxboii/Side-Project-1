using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Player;
using System;

namespace Scripts.AIGirl
{
    public class AIGirl : MonoBehaviour
    {
        [SerializeField]
        private List<Rooms> _roomsList;
        [SerializeField]
        private Rooms _currentRoomList;

        [SerializeField]
        private int _level;

        [SerializeField]
        private Vector3 _target;

        [SerializeField]
        private bool _isPlayerInsideBuilding;

        private void Start()
        {
            _currentRoomList = _roomsList[_level];
            _target = GetPosition();
        }

        private void Update()
        {
           
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Position: " + GetPosition());
                _target = GetPosition();
                
            }

            transform.position = Vector3.MoveTowards(transform.position, _target, 10 * Time.deltaTime);
        }

        private Vector3 GetPosition()
        {
            int targetRoom = UnityEngine.Random.Range(0, _currentRoomList._roomColliderList.Count);

            Vector3 center = _currentRoomList._roomColliderList[targetRoom].bounds.center;

            return center;            
        }
     
    }
}
