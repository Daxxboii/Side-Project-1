
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SavePrefab 
{ public int  Current_cutscene, Subtitle_index, Objective_index, Comic_index;
    public float Health, Time;
    public int[] state, collider_state, rigidbodies;
    public float[] location, rotation;
    public bool saved,had;
}