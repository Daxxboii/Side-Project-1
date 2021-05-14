using UnityEngine;
using Scripts.Timeline;
using Scripts.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class SaveManager : MonoBehaviour
{
    //referenced
    public SavePrefab sm;
    public CharacterController col;
    public Serializer[] serializers;
    public PlayerScript ps;
    public ComicManager comic = null;
    public Timeline_Manager tm = null;
    public Camcorder cam = null;

    //temp
    int[] _state,_collider_state,_rigidbodies;
    float[] _location, _rotation;
    
    //local
    private int index, Current_cutscene, Subtitle_index, Objective_index, Comic_index;
    private float Health,Time;
    public bool[] state,collider_state,rigidbodies;
    public Vector3[] location,rotation;
    string savepath;
    private bool saved;


    public void Start()
    {
        savepath = Application.persistentDataPath + "/TheChallenge.doxx";
        if ( SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (saved)
            {
                Load();
            }
         
        }
    }

   
    public void Loader(int load_index)
    {
        PlayerPrefs.SetInt("loadindex", load_index);
        PlayerPrefs.Save();
    }

   public  void Save()
    {
        check_state();
        Save_transforms();
        Save_colliders();
        Save_rigidbodies();
       
       sm. saved = true;
       sm.Current_cutscene = tm.Current_cutscene;
       sm. Subtitle_index = tm.index;
       sm. Objective_index = tm.objective_index;
       sm.Comic_index = comic.comic_index;
       sm.Health = ps.Health;
       sm.Time = cam.time;


        sm.state = _state;
        sm.collider_state = _collider_state;
        sm.rigidbodies = _rigidbodies;
        sm.location = _location;
        sm.rotation = _rotation;
       _Save();
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
      _state = BoolToInt(state, _state);

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
        _location = VectorToFloat(location, _location);
        _rotation = VectorToFloat(rotation, _rotation);
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
        _collider_state = BoolToInt(collider_state, _collider_state);
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
        _rigidbodies = BoolToInt(rigidbodies, _rigidbodies);
    }
    public void Load()
    {
       
        if (PlayerPrefs.GetInt("loadindex") == 1)
        {
            _Load();
            ps.enabled = false;
            col.enabled = false;
            load_arrays();
            assign();
           
        }
    }

    void load_arrays()
    {
    //  id = PlayerPrefsX.GetStringArray("id");
      state =  sm.state;
      collider_state = sm.collider_state;
      location = sm.location;
      rotation =  sm.rotation;
    
        rigidbodies =  sm.rigidbodies;
      tm.Current_cutscene =  sm.Current_cutscene;
        comic.comic_index = sm.Comic_index;
        ps.Health = sm.Health;
     
      
        cam.time = sm.Time;
       tm.index =  sm.Subtitle_index;
        tm.objective_index = sm.Objective_index;
    }

    void assign()
    {
        foreach(Serializer i in serializers)
        {
            //Active
            serializers[index].gameObject.SetActive(state[index]);
           
            //Location & Rotation
            serializers[index].gameObject.transform.position = location[index];
            serializers[index].gameObject.transform.eulerAngles = rotation[index];
            //Collider
            if (serializers[index].gameObject.GetComponent<Collider>() != null)
            {
                serializers[index].gameObject.GetComponent<Collider>().enabled = collider_state[index];
            }
            //Rigidbody
            if (serializers[index].gameObject.GetComponent<Rigidbody>() != null)
            {
                    serializers[index].gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
            if (serializers[index].gameObject.tag == "Player")
            {
               // col.gameObject.transform.position = new Vector3(location[index].x, location[index].y, location[index].z);
                 ps.enabled = true;
                col.enabled = true;
            }

            index++;
        }
      
        index = 0;
      

    }
   public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }

    void _Save()
    {
        var formatter = new BinaryFormatter();
        using (var stream = File.Create(savepath))
        {
            formatter.Serialize(stream, sm);
        }
        Debug.Log("saved");
    }

    void _Load()
    {
        if (File.Exists(savepath))
        {
            var formatter = new BinaryFormatter();
            using (var stream = File.Open(savepath,FileMode.Open))
            {
                sm = (SavePrefab)formatter.Deserialize(stream);
            }
            Debug.Log("Loaded");
        }
    }

    float[] VectorToFloat(Vector3[] v ,float[] f)
    {
        var counter = -1;
        foreach(Vector3 q in v){
            f[counter++] = q.x;
            f[counter++] = q.y;
            f[counter++] = q.z;
        }
       

        return f;
    }

    int[] BoolToInt(bool[] b, int[] i)
    {
        var counter = 0;
        foreach(bool q in b)
        {
            if (q)
            {
                i[counter] = 1;
            }
            else
            {
                i[counter] = 0;
            }
            counter++;
        }
        return i;
    }

    Vector3[] VectorToFloat(float[]f,Vector3[] v)
    {
        var counter = 0;
       for ( int i = 0; i < v.Length; i += 3)
		{
			]
		}
    }

}
