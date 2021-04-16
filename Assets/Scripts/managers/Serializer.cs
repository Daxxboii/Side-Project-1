using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Serializer : MonoBehaviour
{
    public string name;
  public void Save()
    {
        name = "#" + (Random.value*1000).ToString();
    } 
}
