using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

public class SaveManager : MonoBehaviour
{
  
    void Save()
    {
        Debug.Log("Saveing...");
       
        FileStream file = new FileStream(Application.persistentDataPath + "/Saves/SaveFile.dat", FileMode.OpenOrCreate);

        try
        {
           
            BinaryFormatter formatter = new BinaryFormatter();
            

            /*formatter.Serialize(file, #### )    // replace ### with data type         
             */
        }

        catch (SerializationException e)
        {
            Debug.LogError("There was an issue seriealizing the data: " + e.Message);
        }

        finally
        {
            file.Close();
        }

        

    }

    void Load()
    {
        Debug.Log("Loading...");

        //reverse 

        FileStream file = new FileStream(Application.persistentDataPath + "/Saves/SaveFile.dat", FileMode.Open);

        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            // variable the load should be assigned to  = (typeCast) formatter.Deserialize(file);
        }
        
        catch(SerializationException e)
        {
            Debug.LogError("Error Desrializing Data: " + e.Message);
        }
        
        finally
        {
            file.Close();
        }

    }
}
