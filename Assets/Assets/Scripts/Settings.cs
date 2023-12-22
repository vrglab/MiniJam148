using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class Settings : PersistantSingleton<Settings>
{
    private Dictionary<string, object> settings = new Dictionary<string, object>();

    /// <summary>
    /// Loads the settings from the given file
    /// </summary>
    /// <param name="file">The absolute file path</param>
    /// <b>Authors</b>
    /// <returns>true if succeeded in loading the false otherwise</returns>
    /// <br>Arad Bozorgmehr (Vrglab)</br>
    public bool Load(string file)
    {
        if (File.Exists(file))
        {
            FileStream fileS = File.OpenRead(file);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                settings =  (Dictionary<string, object>)formatter.Deserialize(fileS);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return false;
            }
            fileS.Close();
            return true;
        }  
        return false;
    }

    /// <summary>
    /// Saves the settings to the given file
    /// </summary>
    /// <param name="file">The absolute file path</param>
    /// <b>Authors</b>
    /// <br>Arad Bozorgmehr (Vrglab)</br>
    public void Save(string file)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fileS = null;
        if (!File.Exists(file))
        {
            fileS = File.Create(file);
        }
        else
        {
            fileS = File.OpenWrite(file);
        }

        try
        {
            formatter.Serialize(fileS, settings);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }

        fileS.Close();
    }

    /// <summary>
    /// Gets the setting based on the given key
    /// </summary>
    /// <param name="key">The wanted setting</param>
    /// <returns>The found value, null if nothing found</returns>
    /// <b>Authors</b>
    /// <br>Arad Bozorgmehr (Vrglab)</br>
    public object GetSetting(string key)
    {
        return settings[key];
    }


    /// <summary>
    /// Sets the settings value based on the given key and data
    /// </summary>
    /// <param name="key">The wanted setting</param>
    /// <param name="data">The value to set to</param>
    /// <b>Authors</b>
    /// <br>Arad Bozorgmehr (Vrglab)</br>
    public void SetSetting(string key, object data)
    {
        settings[key] = data;
    }
}
