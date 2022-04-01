using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Instructions : MonoBehaviour
{
    [SerializeField]
    private GameObject Fps_Canvas;
    public Sprite[] instructions;
    public GameObject panel;
    public Image image;
    private int index;
    public PostProcessingManager postProcessingManager;
    void Start()
    {
        if (!PlayerPrefsX.GetBool("Saved"))
        {
            Fps_Canvas.SetActive(false);
            panel.SetActive(true);
            postProcessingManager.Blur();
        }
    }

   public void Next()
    {
        if (index < instructions.Length-1)
        {
            index++;
            image.sprite = instructions[index];
        }
   
    }

   public void Previous()
    {
        if (index > 0)
        {
            index--;
            image.sprite = instructions[index];
        }
       
    }
    public void Close()
    {
        postProcessingManager.UnBlur();
        Fps_Canvas.SetActive(true);
        panel.SetActive(false);
    }
}
