using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Test : MonoBehaviour
{
    public VideoPlayer anims;
    public Material material;
    // Start is called before the first frame update
    void Start()
    {
       // material.EnableKeyword("_EmissionMap");
        anims.Prepare();    }

    // Update is called once per frame
    void Update()
    {
    }

  
}
