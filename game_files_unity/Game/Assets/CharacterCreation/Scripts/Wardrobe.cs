using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Runtime.Serialization;



namespace CharacterCreation {

#if UNITY_EDITOR
    using UnityEditor;

    public class WardrobeWindow : EditorWindow {

        List<bool>[] showVersion;
        List<bool> showPalette;
        List<List<bool>>[] showLayers;
        bool showAllTypes;
        bool showAllPalettes;
        Vector2 scrollPos;
        string path;
        static string[] dirs = {
            "Down",
            "Left",
            "Up",
            "Right"
        };
        Wardrobe w;


        [MenuItem("Window/Wardrobe")]
        static void Init() {

            // Get existing open window or if none, make a new one
            WardrobeWindow window = (WardrobeWindow)GetWindow(typeof(WardrobeWindow));
            window.Show();
        }

        void OnEnable() {

            string[] files = AssetDatabase.FindAssets("t:Wardrobe");
            if (files.Length != 0) {
                path = AssetDatabase.GUIDToAssetPath(files[0]);
            }
            Load(path);
        }

        private void Load(string filePath) {

            w = ScriptableObject.CreateInstance<Wardrobe>();
            int lastSlash = filePath.LastIndexOf('/');
            string fileName = filePath.Substring(lastSlash + 1, filePath.Length - ".asset".Length - lastSlash - 1);
            UnityEngine.Object file = Resources.Load<Wardrobe>(fileName);
            if (file != null)
                w = (Wardrobe)file;
            else
                Debug.LogWarning("File not found - will create new one.");

            showVersion = new List<bool>[w.locations.Length];
            showLayers = new List<List<bool>>[w.locations.Length];
            showPalette = new List<bool>();
            for (int i = 0; i < w.locations.Length; i++) {
                showVersion[i] = new List<bool>();
                showLayers[i] = new List<List<bool>>();
            }
        }

        private static void EqualizeLength<T>(List<T> input, int count) where T : new() {
            while (input.Count != count) {
                if (input.Count < count) {
                    input.Add(new T());
                } else {
                    input.RemoveAt(input.Count - 1);
                }
            }
        }


        public void OnGUI() {

            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

            showAllTypes = EditorGUILayout.Foldout(showAllTypes, "All sprites");
            if (showAllTypes) {
                EditorGUI.indentLevel++;
                for (int t = 0; t < w.locations.Length; t++) {

                    string typeName = w.locations[t].name;
                    string fName = string.IsNullOrEmpty(typeName) ? "Type " + (t + 1).ToString() : typeName;
                    int count = EditorGUILayout.IntField(fName, w.locations[t].versions.Count);
                    EqualizeLength(showVersion[t], count);
                    EqualizeLength(showLayers[t], count);
                    EqualizeLength(w.locations[t].versions, count);

                    EditorGUI.indentLevel++;

                    int index = 0;
                    for (int p = 0; p < w.palettes.Count; p++)
                        if (w.palettes[p].name == w.locations[t].locationPalette.name)
                            index = p;
                    string[] allNames = new string[w.palettes.Count];
                    for (int p = 0; p < w.palettes.Count; p++)
                        allNames[p] = w.palettes[p].name;
                    w.locations[t].name = EditorGUILayout.TextField("Name", w.locations[t].name);
                    int input = EditorGUILayout.Popup("Palette", index, allNames);
                    w.locations[t].locationPalette = w.palettes[input];

                    for (int h = 0; h < w.locations[t].versions.Count; h++) {
                        string versionName = w.locations[t].versions[h].name;
                        string foldoutName = string.IsNullOrEmpty(versionName) ? typeName + " " + (h + 1).ToString() : versionName;
                        if (string.IsNullOrEmpty(typeName)) foldoutName = "Version " + (h + 1).ToString();
                        showVersion[t][h] = EditorGUILayout.Foldout(showVersion[t][h], foldoutName);
                        if (!showVersion[t][h]) continue;
                        EditorGUI.indentLevel++;

                        w.locations[t].versions[h].name = EditorGUILayout.TextField("Name", w.locations[t].versions[h].name);
                        w.locations[t].versions[h].frames = EditorGUILayout.IntField("Frames", w.locations[t].versions[h].frames);
                        count = EditorGUILayout.IntField("Layers", w.locations[t].versions[h].layers.Count);
                        EditorGUI.indentLevel++;
                        EqualizeLength(showLayers[t][h], count);
                        EqualizeLength(w.locations[t].versions[h].layers, count);

                        for (int i = 0; i < w.locations[t].versions[h].layers.Count; i++) {
                            showLayers[t][h][i] = EditorGUILayout.Foldout(showLayers[t][h][i], "Layer " + (i + 1).ToString());
                            if (!showLayers[t][h][i]) continue;
                            Layer l = w.locations[t].versions[h].layers[i];
                            EditorGUI.indentLevel++;
                            l.applyTint = EditorGUILayout.Toggle("Apply tint", l.applyTint);
                            l.layer = EditorGUILayout.IntField("Render order", l.layer);
                            EditorGUILayout.LabelField("Directions");
                            EditorGUI.indentLevel++;
                            for (int j = 0; j < l.directions.Length; j++) {
                                EditorGUILayout.LabelField(dirs[j]);
                                EditorGUI.indentLevel++;
                                bool skip = false;
                                if (j == 1) {
                                    l.leftMirrorsRight = EditorGUILayout.Toggle("Mirror right", l.leftMirrorsRight);
                                    if (l.leftMirrorsRight)
                                        skip = true;
                                }
                                else if (j == 2) {
                                    l.upMatchesDown = EditorGUILayout.Toggle("Match down", l.upMatchesDown);
                                    if (l.upMatchesDown)
                                        skip = true;
                                }
                                if (skip) {
                                    EditorGUI.indentLevel--;
                                    continue;
                                }

                                GUILayout.BeginHorizontal();
                                int frameCount = w.locations[t].versions[h].frames;
                                EqualizeLength(l.directions[j].frames, frameCount);
                                for (int k = 0; k < frameCount; k++) {
                                    GUILayout.BeginVertical();
                                    Frame f = l.directions[j].frames[k];
                                    f.sprite = EditorGUILayout.ObjectField("Frame " + (k + 1).ToString(), (UnityEngine.Object)f.sprite, typeof(Sprite), false) as Sprite;
                                    f.flip = EditorGUILayout.Toggle("Flip horizontal", f.flip);
                                    GUILayout.EndVertical();
                                }
                                GUILayout.EndHorizontal();
                                EditorGUI.indentLevel--;
                            }
                            EditorGUI.indentLevel--;
                            EditorGUI.indentLevel--;
                        }
                        EditorGUI.indentLevel--;
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.indentLevel--;
                }
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.Space();

            showAllPalettes = EditorGUILayout.Foldout(showAllPalettes, "All palettes");
            if (showAllPalettes) {
                EditorGUI.indentLevel++;
                EqualizeLength(showPalette, w.palettes.Count);
                List<Palette> toRemove = new List<Palette>();
                for (int j = 0; j < w.palettes.Count; j++) {
                    Palette item = w.palettes[j];
                    item.name = EditorGUILayout.TextField("Name", item.name);
                    showPalette[j] = EditorGUILayout.Foldout(showPalette[j], "Colors");
                    if (showPalette[j]) {
                        EditorGUI.indentLevel++;
                        int size = EditorGUILayout.IntField("Size", item.colors.Count);
                        EqualizeLength<Color32>(item.colors, size);
                        for (int i = 0; i < item.colors.Count; i++) {
                            item.colors[i] = EditorGUILayout.ColorField(item.colors[i]);
                        }
                        EditorGUI.indentLevel--;
                    }
                    if (GUILayout.Button("Delete"))
                        toRemove.Add(item);
                    EditorGUILayout.Space();
                    EditorGUILayout.Space();
                }
                if (GUILayout.Button("Add Palette"))
                    w.palettes.Add(new Palette());
                foreach (Palette item in toRemove)
                    w.palettes.Remove(item);

                EditorGUI.indentLevel--;
            }

            // This is necessary to save between Unity sessions
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            path = EditorGUILayout.TextField("Asset path", path);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Load"))
                Load(path);
            if (GUILayout.Button("Save"))
                w.SaveAsset(path);
            GUILayout.EndHorizontal();

            EditorGUILayout.EndScrollView();
        }
    }
#endif

    [Serializable]
    public class Wardrobe : ScriptableObject {

        public Location[] locations;
        public List<Palette> palettes;

        public static Wardrobe Singleton {
            get {
                if (SingletonPrivate == null)
                    SingletonPrivate = (Wardrobe)Resources.Load("Wardrobe");
                return SingletonPrivate;
            }
        }
        private static Wardrobe SingletonPrivate;

        public Wardrobe() {
            locations = new Location[6];
            for (int i = 0; i < locations.Length; i++)
                locations[i] = new Location();
            palettes = new List<Palette>();
        }

        public void SaveAsset(string path) {
            #if UNITY_EDITOR
            EditorUtility.SetDirty(this);
            if (!AssetDatabase.Contains(this))
                AssetDatabase.CreateAsset(this, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            #endif
        }
    }

	[Serializable]
    public class Palette {
        public string name;
        public List<Color32> colors;

        public Palette () {
            colors = new List<Color32>();
        }
    }

    [Serializable]
    public class Location {
        public List<Version> versions;
        public string name;
        public Palette locationPalette;

        public Location() {
            versions = new List<Version>();
        }
    }

    [Serializable]
    public class Version : ISerializable {
        public string name;
        public int frames;
        public List<Layer> layers;

        public Version() {
            layers = new List<Layer>();
        }

        protected Version(SerializationInfo info, StreamingContext context) {
            int location = info.GetInt32("location");
            int version = info.GetInt32("version");
            Version ver = Wardrobe.Singleton.locations[location].versions[version];
            name = ver.name;
            frames = ver.frames;
            layers = ver.layers;
        }

        //[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context) {
            Wardrobe w = Wardrobe.Singleton;
            int hash = this.GetHashCode();
            Debug.Log(hash + "  " + Time.time);
            for (int i = 0; i < w.locations.Length; i++) {
                for (int j = 0; j < w.locations[i].versions.Count; j++) {
                    Version v = w.locations[i].versions[j];
                    if (v.GetHashCode() == hash) {
                        Debug.Log("MATCH: " + v.GetHashCode());
                        info.AddValue("location", i);
                        info.AddValue("version", j);
                        return;
                    }
                }
            }
            Debug.Log("This shouldn't happen");
        }
    }

    [Serializable]
    public class Layer {
        public Direction[] directions;
        public bool applyTint = true;
        public bool leftMirrorsRight = false;
        public bool upMatchesDown = false;
        public int layer;

        public Layer() {
            directions = new Direction[4];
            for (int i = 0; i < directions.Length; i++)
                directions[i] = new Direction();
        }
    }

    [Serializable]
    public class Direction {
        public List<Frame> frames;

        public Direction() {
            frames = new List<Frame>();
        }

        public override int GetHashCode () {
            return frames.GetHashCode();
        }
    }

    [Serializable]
    public class Frame {
        public Sprite sprite;
        public bool flip = false;

        public override int GetHashCode () {
            return (sprite.GetHashCode() + flip.GetHashCode()).GetHashCode();
        }
    }
}