using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Enemy.Principal;
public class Visbbality : MonoBehaviour
{
    [SerializeField]
    private bool _Visible;
  
    private void Update()
    {
        AiFollow._isVisible = _Visible;
    }
    void OnBecameVisible()
    {
        _Visible = true;

    }

    void OnBecameInvisible()
    {
        _Visible = false;

    }
}
