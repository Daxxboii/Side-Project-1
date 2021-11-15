using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintManager : MonoBehaviour
{

    public GameObject[] objective_Spots;
    public GameObject track_UI;
    public Camera cam;

    public int index;
    // Update is called once per frame
    void Update()
    {
        Track(index);
    }
    void Track(int var)
    {
        //  Debug.Log(hit.transform.gameObject.name);

        Vector3 pos = cam.WorldToScreenPoint(objective_Spots[var].transform.position);
        track_UI.transform.position = pos;
    }
}
