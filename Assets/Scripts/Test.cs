using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(TEst());
    }

    IEnumerator TEst()
	{
        yield return new WaitForSeconds(5);
        Debug.Log("try");
	}
}
