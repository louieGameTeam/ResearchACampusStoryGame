using System.Collections.Generic;
using UnityEngine;
using CharacterCreation;

[System.Serializable]
public class SaveData {


    public static SaveData cachedSave;


    // Journal data
    public string taskProgress;

    // Player position
    public Vector2 playerPos;

    // Inventory
    public Dictionary<Item, int> inventory {
        get {
            Dictionary<Item, int> inv = new Dictionary<Item, int>();
            if (ownedItems == null || ownedCounts == null) {
                inventory = inv;
            } else {
                for (int i = 0; i < ownedItems.Length; i++)
                    inv.Add(ownedItems[i], ownedCounts[i]);
            }
            return inv;
        }
        set {
            ownedItems = new Item[value.Keys.Count];
            ownedCounts = new int[value.Values.Count];
            value.Keys.CopyTo(ownedItems, 0);
            value.Values.CopyTo(ownedCounts, 0);
        }
    }
    [SerializeField] Item[] ownedItems;
    [SerializeField] int[] ownedCounts;

    // All Chattable dictionaries
    public Dictionary<string, SerializableNestedDictionary<string,string>> dialogMemory;

    // Player dictionary
    public Dictionary<string, string> playerMemory;

    // Player customizations
    public CharacterSetting character;

    // Settings for Music and SFX playback
    public MuteSettings muteSettings;

    public UserInfo userInfo;
    void BuildGameSave () {

		taskProgress = Tasks.game.ToString();
		
        playerPos = (Vector2)GameObject.FindObjectOfType<PlayerControl>().transform.position;

        inventory = Inventory.backpack;

        dialogMemory = new Dictionary<string, SerializableNestedDictionary<string, string>>();
        foreach (Chattable item in GameObject.FindObjectsOfType<Chattable>()) {
            SerializableNestedDictionary<string, string> d = new SerializableNestedDictionary<string, string>(item.properties);
            dialogMemory.Add(item.GetInstanceHash(), d);
        }

        playerMemory = Dialog.playerProperties;

        if (character == null || character.locations.Length == 0)
            character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().cs;

        muteSettings = new MuteSettings();
        muteSettings.Collect();
    }

    public static SaveData WriteGameSave () {
        SaveData sd = ReadGameSave();
        if (sd == null) { 
            sd = new SaveData();
        }
        sd.BuildGameSave();
        Saving.Save(sd);
        return sd;
    }

    public static SaveData WriteCharacterSave () {
        SaveData sd = SaveData.ReadGameSave();

        if (sd == null) sd = new SaveData();
        CharacterSetting cs = null;
        foreach (Character item in GameObject.FindObjectsOfType<Character>()) {
            if (item.isPlayer)
                cs = item.cs;
        }
        if (cs == null)
            return null;    
        sd.character = cs;

        Saving.Save(sd);
        return sd;
    }

    public static SaveData ReadGameSave () {
        return (SaveData) Saving.GetSave();
    }

    public static SaveData CreateInitialSave(UserInfo newUserInfo)
    {
        SaveData sd = new SaveData();
        sd.taskProgress = "Welcome to University!/GoToOA/0/0";
        sd.playerPos = new Vector2(40, -40);
        sd.dialogMemory = new Dictionary<string, SerializableNestedDictionary<string, string>>();
        foreach (Chattable item in GameObject.FindObjectsOfType<Chattable>()) {
            SerializableNestedDictionary<string, string> d = new SerializableNestedDictionary<string, string>(item.properties);
            sd.dialogMemory.Add(item.GetInstanceHash(), d);
        }
        sd.playerMemory = Dialog.playerProperties;
        sd.character = null;
        sd.muteSettings = new MuteSettings();
        sd.userInfo = newUserInfo;
        return sd;
    }

    [System.Serializable]
    public class MuteSettings {
        public bool playMusic = true;
        public bool playSounds = true;

        public void Collect () {
            playMusic = Audio.playMusic;
            ChatManager cm = ChatManager.GetCM();
            if (cm != null)
                playSounds = ChatManager.GetCM().playAudio;
            else
                playSounds = true;
        }

        public void Set () {
            Audio.playMusic = playMusic;
            ChatManager cm = ChatManager.GetCM();
            if (cm != null)
                cm.playAudio = playSounds;
        }
    }

    [System.Serializable]
    public class UserInfo 
    {
        public string firstName;
        public string lastName;
        public string email;

        public UserInfo(string firstName, string lastName, string email)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
        }
    }
}

// The serializer sucks and can't serialize nested dictionaries without a wrapper class, ergo this
[System.Serializable]
public class SerializableNestedDictionary<T1, T2> {
    public Dictionary<T1, T2> dictionary;

    public SerializableNestedDictionary(Dictionary<T1, T2> d)
    {
        dictionary = d;
    }
}