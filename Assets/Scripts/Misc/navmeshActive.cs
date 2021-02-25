using System.Collections;
using unityCore.Audio;
using UnityEngine;
using UnityEngine.AI;

public class navmeshActive : MonoBehaviour
{
    public NavMeshSurface nav;
    public bool y;

    private void Start()
    {
        nav = GetComponent<NavMeshSurface>();
        nav.enabled = false;
    }
    private void Update()
    {
        if (y)
        {
            nav.enabled = true;
        }
        else
            nav.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            y = true;
        }   
        else
        {
            y = false;
        }
    }
}
