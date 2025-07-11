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
    // Outputs data to a binary file
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file;
        SavedData data;

        // If there is already a save file, open it up; do not overwrite
        if (File.Exists(Application.persistentDataPath + "/saveData.dat"))
        {   // A file exists
            file = File.Open(Application.persistentDataPath + "/saveData.dat", FileMode.Open);
            data = (SavedData)bf.Deserialize(file);
            file.Close();
            file = File.Create(Application.persistentDataPath + "/saveData.dat");
        }
        else
        {   // There is no file
            file = File.Create(Application.persistentDataPath + "/saveData.dat");
            data = new SavedData();
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
        SavedData data;

        // If there is already a save file, open it up; do not overwrite
        if (File.Exists(Application.persistentDataPath + "/saveData.dat"))
        {   // A file exists
            file = File.Open(Application.persistentDataPath + "/saveData.dat", FileMode.Open);
            data = (SavedData)bf.Deserialize(file);
            file.Close();
            file = File.Create(Application.persistentDataPath + "/saveData.dat");
        }
        else
        {   // There is no file
            file = File.Create(Application.persistentDataPath + "/saveData.dat");
            data = new SavedData();
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
        if (File.Exists(Application.persistentDataPath + "/saveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/saveData.dat", FileMode.Open);
            SavedData data = (SavedData)bf.Deserialize(file);
            file.Close();

            // Load data
            GetComponent<SwitchScene>().ChangeScene(data.scene);
        }
    }
}

[Serializable]
public class SavedData
{
    public SavedData()
    {
        playerData = new PlayerData();
    }
    public string scene;
    public float test;
    public PlayerData playerData;
    public SerializableVector3 playerPos;
}

// Contains information about the player character's appearance
[Serializable]
public class PlayerData
{
    public SerializableVector4 skinColor;
    public SerializableVector4 hairColor;
    public SerializableVector4 hoodieColor;
    public SerializableVector4 pantsColor;
    public SerializableVector4 shoesColor;
    public SerializableVector4 eyeColor;
    public int hairStyle;
}

// Can convert to a Vector4
[Serializable]
public struct SerializableVector4
{
    public float x;     // x component
    public float y;     // y component
    public float z;     // z component
    public float w;     // w component

    // Constructor
    public SerializableVector4(float tempX, float tempY, float tempZ, float tempW)
    {
        x = tempX;
        y = tempY;
        z = tempZ;
        w = tempW;
    }

    // Returns a string representation of the object
    public override string ToString()
    {
        return String.Format("[{0}, {1}, {2}, {3}]", x, y, z, w);
    }

    // Automatic conversion from SerializableVector4 to Vector4
    public static implicit operator Vector4(SerializableVector4 temp)
    {
        return new Vector4(temp.x, temp.y, temp.z, temp.w);
    }

    // Automatic conversion from SerializableVector4 to Color
    public static implicit operator Color(SerializableVector4 temp)
    {
        return new Color(temp.x, temp.y, temp.z, temp.w);
    }

    // Automatic conversion from Vector4 to SerializableVector4>
    public static implicit operator SerializableVector4(Vector4 temp)
    {
        return new SerializableVector4(temp.x, temp.y, temp.z, temp.w);
    }

    // Automatic conversion from Color to SerializableVector4>
    public static implicit operator SerializableVector4(Color temp)
    {
        return new SerializableVector4(temp.r, temp.g, temp.b, temp.a);
    }
}

// Can convert to a Vector3
[Serializable]
public struct SerializableVector3
{
    public float x;     // x component
    public float y;     // y component
    public float z;     // z component

    // Constructor
    public SerializableVector3(float tempX, float tempY, float tempZ)
    {
        x = tempX;
        y = tempY;
        z = tempZ;
    }

    // Returns a string representation of the object
    public override string ToString()
    {
        return String.Format("[{0}, {1}, {2}]", x, y, z);
    }

    // Automatic conversion from SerializableVector4 to Vector4
    public static implicit operator Vector3(SerializableVector3 temp)
    {
        return new Vector3(temp.x, temp.y, temp.z);
    }

    // Automatic conversion from Vector4 to SerializableVector4>
    public static implicit operator SerializableVector3(Vector3 temp)
    {
        return new SerializableVector3(temp.x, temp.y, temp.z);
    }
}*/