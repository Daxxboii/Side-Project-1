using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Test : MonoBehaviour
{
    public VideoPlayer anims;
    public Material material;
    // Start is called before the first frame update
    private void Start()
    {
        anims.Prepare();    
    }
}
