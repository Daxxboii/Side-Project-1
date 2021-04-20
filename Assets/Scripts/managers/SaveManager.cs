using UnityEngine;
using Scripts.Timeline;
using Scripts.Player;
using UnityEngine.SceneManagement;


public class SaveManager : MonoBehaviour
{
    public bool saved;
    public Serializer[] serializers;
    public Settings settings;
    private int index;
    public string[] id;
    public bool[] state,collider_state,rigidbodies;
    public Vector3[] location,rotation;
    public ComicManager comic;
    public Timeline_Manager tm = null;
   
    public void Start()
    {
        assign_id();
        if ( SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (PlayerPrefsX.GetBool("Saved"))
            {
                Load();
            }
         
        }
 
    }

    void assign_id()
    {
        serializers = Resources.FindObjectsOfTypeAll<Serializer>();
        id = new string[serializers.Length];
        foreach (Serializer i in serializers)
        {
            serializers[index].Assign_id();
            id[index] = serializers[index].name;
            index++;
        }
        index = 0;
    }

    public void Loader(int load_index)
    {
        PlayerPrefs.SetInt("loadindex", load_index);
        PlayerPrefs.Save();
    }

   public  void Save()
    {
        saved = true;
        check_state();
        Save_transforms();
        Save_colliders();
        Save_rigidbodies();
      //  PlayerPrefsX.SetStringArray("id", id);
        PlayerPrefsX.SetBoolArray("state", state);
        PlayerPrefsX.SetBoolArray("collider_state", collider_state);
        PlayerPrefsX.SetVector3Array("location", location);
        PlayerPrefsX.SetVector3Array("rotation", rotation);
        PlayerPrefsX.SetBoolArray("rigidbodies", rigidbodies);
        PlayerPrefs.SetInt("Current_cutscene", tm.Current_cutscene);
        PlayerPrefs.SetInt("Comic_index", comic.comic_index);
        PlayerPrefs.SetInt("Page_index", comic.page_index);
        PlayerPrefs.SetFloat("Volume", settings.volume);
        PlayerPrefs.SetFloat("Senci", settings.senci);
        PlayerPrefsX.SetBool("Saved", saved);
        PlayerPrefs.Save();
    }

    void check_state()
    {
        state = new bool[serializers.Length];
        foreach (Serializer i in serializers)
        {
            if (serializers[index].gameObject.activeSelf)
            {
                state[index] = true;
            }
            else
            {
                state[index] = false;
            }
          index++;
        }
        index = 0;
    }
    void Save_transforms()
    {
        location = new Vector3[serializers.Length];
        rotation = new Vector3[serializers.Length];
        foreach (Serializer i in serializers)
        {
            serializers[index].Assign_transforms();
            location[index] = serializers[index].loc;
            rotation[index] = serializers[index].rot;
            index++;
        }
        index = 0;
    }

    void Save_colliders()
    {
        collider_state = new bool[serializers.Length];
        foreach (Serializer i in serializers)
        {
            serializers[index].collider();
            collider_state[index] = serializers[index].collider_state;
            index++;
        }
        index = 0;
    }

    void Save_rigidbodies()
    {
        rigidbodies = new bool[serializers.Length];
        foreach (Serializer i in serializers)
        {
            rigidbodies[index] = serializers[index].Rigidbody_Exists();
            index++;
        }
        index = 0;
    }
    public void Load()
    {

        if (PlayerPrefs.GetInt("loadindex") == 1)
        {
            load_arrays();
            assign();
        }
    }

    void load_arrays()
    {
    //  id = PlayerPrefsX.GetStringArray("id");
      state =  PlayerPrefsX.GetBoolArray("state");
      collider_state = PlayerPrefsX.GetBoolArray("collider_state");
      location = PlayerPrefsX.GetVector3Array("location");
      rotation =  PlayerPrefsX.GetVector3Array("rotation");
      rigidbodies =  PlayerPrefsX.GetBoolArray("rigidbodies");
      tm.Current_cutscene =  PlayerPrefs.GetInt("Current_cutscene");
        comic.comic_index = PlayerPrefs.GetInt("Comic_index");
        comic.page_index = PlayerPrefs.GetInt("Page_index");
    }

    void assign()
    {
        foreach(Serializer i in serializers)
        {
            //Active
            serializers[index].gameObject.SetActive(state[index]);
            //Collider
            if (serializers[index].gameObject.GetComponent<Collider>() != null)
            {
                serializers[index].gameObject.GetComponent<Collider>().enabled = collider_state[index];
            }
            //Location & Rotation
            serializers[index].gameObject.transform.position = location[index];
            serializers[index].gameObject.transform.eulerAngles = rotation[index];
            if(serializers[index].gameObject.tag == "Player")
            {
                serializers[index].gameObject.GetComponent<PlayerScript>().enabled = false;
                serializers[index].gameObject.transform.position = location[index];
                serializers[index].gameObject.transform.eulerAngles = rotation[index];
                serializers[index].gameObject.GetComponent<PlayerScript>().enabled = true;
            }

            //Rigidbody
            if (serializers[index].gameObject.GetComponent<Rigidbody>() != null)
            {
                if (rigidbodies[index]== false)
                {
                    serializers[index].gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                }
                else
                {
                    serializers[index].gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
            }

            index++;
        }
        index = 0;
       

    }


}
