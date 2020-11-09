/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;

// Load the player character on startup
public class LoadPlayerSettings : MonoBehaviour {
    //Place this script on "Player"

    // Use this for initialization
    void Start () {
        LoadCharacter();
    }

    // Loads player data
    void LoadCharacter() {
        if (File.Exists(Application.persistentDataPath + "/saveData.dat")) {
            // Get data
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/saveData.dat", FileMode.Open);
            SavedData data = (SavedData)bf.Deserialize(file);
            file.Close();

            // Load data
            GameObject.Find("body").GetComponent<SpriteRenderer>().color = data.playerData.skinColor;
            GameObject.Find("hair").GetComponent<SpriteRenderer>().color = data.playerData.hairColor;
            GameObject.Find("eye_texture").GetComponent<SpriteRenderer>().color = data.playerData.eyeColor;
            GameObject.Find("hoodie").GetComponent<SpriteRenderer>().color = data.playerData.hoodieColor;
            GameObject.Find("pants_texture").GetComponent<SpriteRenderer>().color = data.playerData.pantsColor;
            GameObject.Find("shoes").GetComponent<SpriteRenderer>().color = data.playerData.shoesColor;
        }
    }
}
*/