using DigitalRuby.RainMaker;
using UnityEngine;

public class RainStableizer : MonoBehaviour
{
    [SerializeField]
    private float Timerstert, MaxTime;
    [SerializeField]
    RainScript RS;

    private void Start()
    {
        if (!PlayerPrefsX.GetBool("Saved"))
        {
            FogActivator.inside = false;
        }
    }
    void FixedUpdate()
    {
        if (!FogActivator.inside)
        {
            Timerstert += Time.deltaTime;
            if (Timerstert >= MaxTime)
            {
                RS.RainIntensity = Random.Range(0.1f, 1f);
                Timerstert = 0.0f;

            }
        }
    }
}
