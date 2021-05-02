using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Serializer2 : MonoBehaviour
{
    public string name_of_bool;

   public SaveManager[] sm;
    // Start is called before the first frame update
    void Start()
    {
      /*  sm = Resources.FindObjectsOfTypeAll<SaveManager>();
        if (!sm[0].saved)
        {
            if (PlayerPrefsX.GetBool(name_of_bool))
            {
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        else
        {
            gameObject.SetActive(true);
            PlayerPrefsX.SetBool(name_of_bool, true);
        }*/
    }

    // Update is called once per frame
    void OnDestroy()
    {
        PlayerPrefsX.SetBool(name_of_bool, false);
    }
}
