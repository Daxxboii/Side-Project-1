using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
public class TestingStoryline : MonoBehaviour
{
    [SerializeField] private bool is_Testing;
    [SerializeField,ConditionalHide(("is_Testing"),true)] private GameObject girl,Principal;
    [SerializeField,ConditionalHide(("is_Testing"),true)] private Timeline_Manager timeline_Manager;
    [SerializeField,ConditionalHide(("is_Testing"),true)] private PlayableDirector Director;
    [SerializeField,ConditionalHide(("is_Testing"),true)] private float Cutscene_index, Comic_index;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();
        //Play all Cutscenes
    }

    // Update is called once per frame
    void Update()
    {
        girl.SetActive(false);
        Principal.SetActive(false);
    }
}
