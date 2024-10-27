using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem instance { get; private set; }
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    public string saveName = "SaveData_";
    [Range(0, 10)]
    public int saveDataIndex = 1;

    public void SaveData(string dataToSave, int saveDataIndex=1)
    {
      
            if (WriteToFile(saveName + saveDataIndex, dataToSave))
        {
            Debug.Log("Successfully saved data");
        }
    }

    public string LoadData()
    {
        string data = "";
        if (ReadFromFile(saveName + saveDataIndex, out data))
        {
            Debug.Log("Successfully loaded data");
        }
        return data;
    }
    public string LoadFirstData()
    {
        string data = "";
        if (ReadFromFile(saveName + 0, out data))
        {
            Debug.Log("Successfully loaded data");
        }
        return data;
    }

    private bool WriteToFile(string name, string content)
    {
        var fullPath = Path.Combine(Application.persistentDataPath, name);

        try
        {
            File.WriteAllText(fullPath, content);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError("Error saving to a file " + e.Message);
        }
        return false;
    }

    private bool ReadFromFile(string name, out string content)
    {
        var fullPath = Path.Combine(Application.persistentDataPath, name);
        try
        {
            //Debug.LogWarning(fullPath);
            content = File.ReadAllText(fullPath);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError("Error when loading the file " + e.Message);
            content = "";
            
        }
        return false;
    }
}