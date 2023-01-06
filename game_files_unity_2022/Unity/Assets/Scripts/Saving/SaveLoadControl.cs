/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;

[RequireComponent(typeof(SwitchScene))]
public class SaveLoadControl : MonoBehaviour
{

    public static string fileName = "SaveData.ucd";
    public static string filePath = Application.persistentDataPath + "/" + fileName;

    // Outputs data to a binary file
    public void Save() {
        
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file;
        SaveData data;

        // Update existing save file
        if (File.Exists(filePath)) {
            file = File.Open(filePath, FileMode.Open);
            data = (SaveData)bf.Deserialize(file);
            file.Close();
            file = File.Create(filePath);

        // Create new file to save to
        } else {
            file = File.Create(filePath);
            data = new SaveData();
        }

        // Set necessary data
        data.scene = SceneManager.GetActiveScene().name;

        // Write to file
        bf.Serialize(file, data);
        file.Close();
    }

    // Outputs data to a binary file, but can input scene to save
    public void SaveWithScene(string scene)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file;
        SaveData data;

        // If there is already a save file, open it up; do not overwrite
        if (File.Exists(filePath))
        {   // A file exists
            file = File.Open(filePath, FileMode.Open);
            data = (SaveData)bf.Deserialize(file);
            file.Close();
            file = File.Create(filePath);
        }
        else
        {   // There is no file
            file = File.Create(filePath);
            data = new SaveData();
        }

        // Set necessary data
        data.scene = scene;

        // Write to file
        bf.Serialize(file, data);
        file.Close();
    }

    // Load from binary file
    public void Load()
    {
        if (File.Exists(filePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(filePath, FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();

            // Load data
            GetComponent<SwitchScene>().ChangeScene(data.scene);
        }
    }
}*/