using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Enemy.girlHostile;

public class AnimationEvents : MonoBehaviour
{
    public delegate void Kill();
    public static event Kill kill;

    public GirlAiGhost girlAIGhost;
    public void Invoke_Death()
    {
        kill.Invoke();
    }

    public void MakeGirlCalm()
    {
        girlAIGhost.Animations(0, 0);
    }
}
