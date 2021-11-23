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
    void Start()
    {
        if (!PlayerPrefsX.GetBool("Saved"))
        {
            Time.timeScale = 0;
            Fps_Canvas.SetActive(false);
            panel.SetActive(true);
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
        Time.timeScale = 1;
        Fps_Canvas.SetActive(true);
        panel.SetActive(false);
    }
}
