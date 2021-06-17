using UnityEngine;
using Scripts.Timeline;
using Scripts.Player;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using Scripts.Objects;
public class SaveManager : MonoBehaviour
{
    //referenced
  [SerializeField]
    private GameObject save_panel,fps_canvas;
    public CharacterController col;
    public Serializer[] serializers;
    public PlayerScript ps;
    public ComicManager comic = null;
    public Timeline_Manager tm = null;
    public Camcorder cam = null;
    public ObjectController oc;
    public GameObject minimap_button;
    //temp
    int[] _state, _collider_state, _rigidbodies;
    float[] _location, _rotation;
    [SerializeField]
    SavePrefab sm;
    GameObject temp;

    //local
    private int index;
  
    public List<bool> state, collider_state, rigidbodies = new List<bool>();
    public List<Vector3> location, rotation =  new List<Vector3>();
    string savepath;
   
   

    public void Start()
    {
       
        //  Debug.Log(PlayerPrefsX.GetBool("Saved"));
        savepath = Application.persistentDataPath + "/TheChallenge.doxx";
        if ( SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (PlayerPrefsX.GetBool("Saved"))
            {
                Load();
            }
            else
            {
                
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
        
       save_panel.SetActive(true);
        fps_canvas.SetActive(false);
      
        StartCoroutine("Loading");
        _state = new int[serializers.Length];
         _collider_state = new int[serializers.Length];
        _rigidbodies = new int[serializers.Length];
        _location = new float[(serializers.Length*3)+3];
        _rotation = new float[(serializers.Length * 3)];
       
        check_state();
       
        Save_transforms();
       
        Save_colliders();
       
        Save_rigidbodies();
      
        PlayerPrefsX.SetBool("Saved", true);
       
        sm. saved = true;
       
        sm.Current_cutscene = tm.Current_cutscene;
       sm. Subtitle_index = tm.index;
       sm. Objective_index = tm.objective_index;
       sm.Comic_index = comic.comic_index;
       sm.Health = ps.Health;
       sm.Time = cam.time;
     
        _location = VectorToFloat(location, _location);
      
        _collider_state = BoolToInt(collider_state, _collider_state);
        _state = BoolToInt(state, _state);
      
        _rotation = VectorToFloat(rotation, _rotation);
        _rigidbodies = BoolToInt(rigidbodies, _rigidbodies);
     
        sm.state = _state;
        sm.collider_state = _collider_state;
        sm.rigidbodies = _rigidbodies;
        sm.location = _location;
        sm.rotation = _rotation;
     
        _Save();
     
     

    }

    void check_state()
    {
       
        state = new List<bool>();
       if(oc.had != null)
        {
            sm.had = true;
            oc.had.SetActive(false);
        }
        else
        {
            sm.had = false;
        }
      
        foreach (Serializer i in serializers)
        {

            if (i.gameObject.activeSelf)
            {
                state.Add(true);
            }
            else
            {
                state.Add(false);
            }
        
        }
      
     

    }
    void Save_transforms()
    {
      
        location=  new List<Vector3>();
        rotation = new List<Vector3>();
        foreach (Serializer i in serializers)
        {
            i.Assign_transforms();
            location.Add(i.loc);
            rotation.Add(i.rot);
        }
    }

    void Save_colliders()
    {
        collider_state = new List<bool>();
        foreach (Serializer i in serializers)
        {
           i._collider();
            collider_state.Add(i.collider_state);
        }
    }

    void Save_rigidbodies()
    {
        rigidbodies = new List<bool>();
        foreach (Serializer i in serializers)
        {
            rigidbodies.Add(i.Rigidbody_Exists());
            index++;
        }
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
        else
        {
            escape_door.read = false;
        }
    }

    void load_arrays()
    {
        if(sm.had == true)
        {
            temp = new GameObject();
            oc.had = temp;
        }
        minimap_button.SetActive(true);
        FogActivator.inside = true;
      
    //  id = PlayerPrefsX.GetStringArray("id");
      _state =  sm.state;
     _collider_state = sm.collider_state;
      _location = sm.location;
      _rotation =  sm.rotation;
    
        _rigidbodies =  sm.rigidbodies;
      tm.Current_cutscene =  sm.Current_cutscene;
        comic.comic_index = sm.Comic_index;
        ps.Health = sm.Health;

        state = IntToBool(state,_state);
        collider_state = IntToBool(collider_state, _collider_state);
        rigidbodies = IntToBool(rigidbodies, _rigidbodies);
       
        location = FloatToVector(_location, location);
        rotation = FloatToVector(_rotation, rotation);
    

      
      
        cam.time = sm.Time;
       tm.index =  sm.Subtitle_index;
        tm.objective_index = sm.Objective_index-1;
    }

    void assign()
    {
        index = 0;
       
        foreach(Serializer i in serializers)
        {
            //Active
           i.gameObject.SetActive(state[index]);
           
            //Location & Rotation
           i.gameObject.transform.position = location[index];
           i.gameObject.transform.eulerAngles = rotation[index];
            //Collider
            if (i.gameObject.GetComponent<Collider>() != null)
            {
                i.gameObject.GetComponent<Collider>().enabled = collider_state[index];
            }
            //Rigidbody
            if (i.gameObject.GetComponent<Rigidbody>() != null)
            {
                   i.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
            if (i.gameObject.tag == "Player")
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
       
       
        PlayerPrefs.Save();

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
           
        }
    }

    float[] VectorToFloat(List<Vector3> v ,float[] f)
    {
       int counter = -1;
        Vector3[] temp = v.ToArray();

        //Debug.Log(temp.Length);
        foreach(Vector3 q in temp)
        {
          //  Debug.Log(counter);
            f[counter+1] = q.x;
            f[counter+2] = q.y;
            f[counter+3] = q.z;
            counter+=3;
        }
        counter = -1;

        return f;
    }

    int[] BoolToInt(List<bool> b, int[] i)
    {
      
        int counter = 0;
        foreach(bool q in b)
        {
            if (q == true)
            {
                i[counter] = 1;
            }
            else
            {
                i[counter] = 0;
            }
            counter+=1;
        }
        return i;
    }

   List<Vector3> FloatToVector(float[]f,List<Vector3> v)
    {
       
        v = new List<Vector3>();
       int countr = -1;
       for ( int i = 0; i < serializers.Length; i ++)
		{
         
            v.Add(new Vector3(f[countr+1], f[countr + 2], f[countr + 3]));
            countr += 3;
        }
        return v;
    }

   List<bool> IntToBool(List<bool> b , int[] i)
    {
        b = new List<bool>();
        foreach(int q in i)
        {
            if (q == 0)
            {
                b.Add(false);
            }
            else
            {
                b.Add(true);
            }
          
        }
        return b;
    }

    IEnumerator Loading()
    {
        yield return new WaitForSeconds(3);
       
            save_panel.SetActive(false);
        fps_canvas.SetActive(true);


    }

    public void New()
    {
        PlayerPrefsX.SetBool("Saved", false);
    }
}
