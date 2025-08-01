using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using CharacterCreation;

public static class Serialization {

    public static string fileName = "SaveData.ucd";
    public static string path = Path.Combine(Application.persistentDataPath, fileName);

    public static void Serialize(SaveData obj) {
        FileStream file = File.Open(path, FileMode.Create);
        SurrogateFormatter().Serialize(file, obj);
        file.Close();
        file.Dispose();
    }

    public static object Deserialize() {
        object result = null;
        bool SaveExists = File.Exists(path);        
        if (SaveExists) {
            FileStream file = File.Open(path, FileMode.Open);
            result = SurrogateFormatter().Deserialize(file);
            file.Close();
            file.Dispose();
        }

        return result;
    }

    private static BinaryFormatter SurrogateFormatter() {
        BinaryFormatter bf = new BinaryFormatter();
        SurrogateSelector ss = new SurrogateSelector();

        ss.AddSurrogate(typeof(Color), new StreamingContext(StreamingContextStates.All), new ColorSerializationSurrogate());
        ss.AddSurrogate(typeof(Version), new StreamingContext(StreamingContextStates.All), new VersionSerializationSurrogate());
        ss.AddSurrogate(typeof(Vector2), new StreamingContext(StreamingContextStates.All), new Vector2SerializationSurrogate());
        ss.AddSurrogate(typeof(Item), new StreamingContext(StreamingContextStates.All), new ItemSerializationSurrogate());
        ss.AddSurrogate(typeof(Dictionary<string, string>), new StreamingContext(StreamingContextStates.All), new DictionarySerializationSurrogate<string, string>());
        ss.AddSurrogate(typeof(Dictionary<string, int>), new StreamingContext(StreamingContextStates.All), new DictionarySerializationSurrogate<string, int>());
        ss.AddSurrogate(typeof(Dictionary<Item, int>), new StreamingContext(StreamingContextStates.All), new DictionarySerializationSurrogate<Item, int>());

        bf.SurrogateSelector = ss;
        return bf;
    }
}


// Serialization Surrogate for UnityEngine.Color
sealed class ColorSerializationSurrogate : ISerializationSurrogate {
    public void GetObjectData(object obj, SerializationInfo info, StreamingContext context) {
        Color col = (Color)obj;
        info.AddValue("r", col.r);
        info.AddValue("g", col.g);
        info.AddValue("b", col.b);
        info.AddValue("a", col.a);
    }

    public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector) {
        Color col = (Color)obj;
        col.r = (float)info.GetValue("r", typeof(float));
        col.g = (float)info.GetValue("g", typeof(float));
        col.b = (float)info.GetValue("b", typeof(float));
        col.a = (float)info.GetValue("a", typeof(float));
        return col;
    }
}

// Serialization Surrogate for UnityEngine.Vector2
sealed class Vector2SerializationSurrogate : ISerializationSurrogate {

    public void GetObjectData(object obj, SerializationInfo info, StreamingContext context) {

        Vector2 v = (Vector2)obj;
        info.AddValue("x", v.x);
        info.AddValue("y", v.y);
    }

    public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector) {

        Vector2 v = (Vector2)obj;
        v.x = (float)info.GetValue("x", typeof(float));
        v.y = (float)info.GetValue("y", typeof(float));
        return v;
    }
}

// Serialization Surrogate for Item
sealed class ItemSerializationSurrogate : ISerializationSurrogate {

    public void GetObjectData(object obj, SerializationInfo info, StreamingContext context) {

        Item item = (Item)obj;
        info.AddValue("item", Inventory.globalItems.IndexOf(item));
    }

    public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector) {

        Item item = (Item)obj;
        int index = info.GetInt32("item");
        if (Inventory.globalItems != null && Inventory.globalItems.Count > index)
            item = Inventory.globalItems[index];
        if (item == null) item = new Item();
        return item;
    }
}

// Serialization Surrogate for System.Collections.Generics.Dictionary<T1, T2>
sealed class DictionarySerializationSurrogate<T1, T2> : ISerializationSurrogate {

    public void GetObjectData(object obj, SerializationInfo info, StreamingContext context) {
        Dictionary<T1, T2> d = (Dictionary<T1, T2>)obj;
        T1[] keys = new T1[d.Keys.Count];
        d.Keys.CopyTo(keys, 0);
        T2[] values = new T2[d.Values.Count];
        d.Values.CopyTo(values, 0);
        info.AddValue("keys", keys);
        info.AddValue("values", values);
    }

    public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector) {
        Dictionary<T1, T2> d = new Dictionary<T1, T2>();
        T1[] keys = (T1[])info.GetValue("keys", typeof(T1[]));
        T2[] values = (T2[])info.GetValue("values", typeof(T2[]));
        for (int i = 0; i < keys.Length; i++) {
            d.Add(keys[i], values[i]);
        }
        return d;
    }

}

// Serialization Surrogate for CharacterCreation.Version instances from the Wardrobe asset
sealed class VersionSerializationSurrogate : ISerializationSurrogate {

    public void GetObjectData(object obj, SerializationInfo info, StreamingContext context) {

        Version ver = (Version)obj;
        Wardrobe w = Wardrobe.Singleton;

        int hash = ver.GetHashCode();
        //Debug.Log(hash + "  " + Time.time);
        for (int i = 0; i < w.locations.Length; i++) {
            for (int j = 0; j < w.locations[i].versions.Count; j++) {
                Version v = w.locations[i].versions[j];
                if (v.GetHashCode() == hash) {
                    //Debug.Log("MATCH: " + v.GetHashCode());
                    info.AddValue("location", i);
                    info.AddValue("version", j);
                    return;
                }
            }
        }
        Debug.LogWarning("This shouldn't happen");
    }

    public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector) {

        int location = info.GetInt32("location");
        int version = info.GetInt32("version");

        Version ver = (Version)obj;
        ver = Wardrobe.Singleton.locations[location].versions[version];

        return ver;
    }
}