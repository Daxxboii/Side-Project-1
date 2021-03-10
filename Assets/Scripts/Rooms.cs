using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Availabe Rooms", menuName = "Rooms")]
public class Rooms : ScriptableObject
{
    [SerializeField]
    public List<Collider> _roomColliderList;
}
