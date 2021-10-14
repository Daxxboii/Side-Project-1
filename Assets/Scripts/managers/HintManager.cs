using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintManager : MonoBehaviour
{

    public GameObject[] objective_Spots;
    public GameObject track_UI;

    public int index;
    // Update is called once per frame
    void Update()
    {
        Track(index);
    }
    void Track(int index)
    {
        //  Debug.Log(hit.transform.gameObject.name);

        Vector3 pos = Camera.main.WorldToScreenPoint(objective_Spots[index].transform.position);
        track_UI.transform.position = pos;
    }
}
