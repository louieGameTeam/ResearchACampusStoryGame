using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace AudioSystem {

    #if UNITY_EDITOR
	using UnityEditor;

    public class AudioWindow : EditorWindow {

        string path;
        Vector2 scrollPos;
        List<bool>[] folded;
        string search = string.Empty;
        AudioDB adb;


        [MenuItem("Window/Audio Database")]
        static void Init() {

            // Get existing open window or if none, make a new one
            AudioWindow window = (AudioWindow)GetWindow(typeof(AudioWindow));
            window.Show();
        }

        void OnEnable() {

            string[] files = AssetDatabase.FindAssets("t:AudioDB");
            if (files.Length != 0) {
                path = AssetDatabase.GUIDToAssetPath(files[0]);
            }
            Load(path);
        }

        private void Load(string filePath) {

            adb = ScriptableObject.CreateInstance<AudioDB>();
            if (!string.IsNullOrEmpty(filePath)) {
                int lastSlash = filePath.LastIndexOf('/');
                string fileName = filePath.Substring(lastSlash + 1, filePath.Length - ".asset".Length - lastSlash - 1);
                UnityEngine.Object file = Resources.Load<AudioDB>(fileName);
                if (file != null)
                    adb = (AudioDB)file;
            } else {
                adb = new AudioDB();
                Debug.LogWarning("File not found - will create new one.");
            }

            folded = new List<bool>[3];
            for (int i = 0; i < 3; i++)
                folded[i] = new List<bool>();
        }

        public void OnGUI() {

            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

            search = EditorGUILayout.TextField("Search", search);
            EditorGUILayout.Space();

            for (int type = 0; type < 3; type++) {

                string typeName = string.Empty;
                List<Clip> source = new List<Clip>();
                if (type == 0) {
                    typeName = "Music";
                    source = adb.music;
                } else if (type == 1) {
                    typeName = "Ambient";
                    source = adb.ambient;
                } else if (type == 2) {
                    typeName = "Sound";
                    source = adb.sound;
                }

                EditorGUILayout.LabelField(typeName);
                EditorGUI.indentLevel++;

                List<Clip> toRemove = new List<Clip>();
                EqualizeLength<bool>(folded[type], source.Count);
				for (int i = 0; i < source.Count; i++) {
                    Clip clip = source[i];
                    if (string.IsNullOrEmpty(search) || clip.name.ToLower().Contains(search.ToLower())) {
                        folded[type][i] = EditorGUILayout.Foldout(folded[type][i], string.IsNullOrEmpty(clip.name) ? "[New " + typeName + " Clip]" : clip.name);
                        if (folded[type][i]) {
                            EditorGUI.indentLevel++;
                            clip.name = EditorGUILayout.TextField("Name", clip.name);
                            if (clip.clips.Count > 1)
                                clip.playRandom = EditorGUILayout.Toggle("Pick random", clip.playRandom);
                            int size = EditorGUILayout.IntField("AudioClip Selection", clip.clips.Count);
                            if (size < 1) size = 1;
                            EqualizeLengthNullable<AudioClip>(clip.clips, size);
                            EditorGUI.indentLevel++;
                            for (int t = 0; t < clip.clips.Count; t++) {
                                clip.clips[t] = EditorGUILayout.ObjectField(clip.clips[t], typeof(AudioClip), false) as AudioClip;
							}
                            EditorGUI.indentLevel--;

							int overrides = EditorGUILayout.IntField("Overrides", clip.overrides.Count);
							if (overrides < 0) overrides = 0;
							while (clip.overrides.Count != overrides) {
								if (clip.overrides.Count < overrides)
									clip.overrides.Add(string.Empty);
								else
									clip.overrides.RemoveAt(clip.overrides.Count - 1);
							}
							EditorGUI.indentLevel++;
							for (int t = 0; t < clip.overrides.Count; t++)
                                clip.overrides[t] = EditorGUILayout.TextField(clip.overrides[t]);
							EditorGUI.indentLevel--;

							if (GUILayout.Button("Delete"))
                                toRemove.Add(clip);
                            EditorGUILayout.Space();
                            EditorGUI.indentLevel--;
                        }
                    }
                }
                foreach (Clip item in toRemove)
                    source.Remove(item);

                EditorGUILayout.Space();
                if (GUILayout.Button("New " + typeName + " Clip"))
                    source.Add(new Clip());

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
                adb.SaveAsset(path);
            GUILayout.EndHorizontal();

            EditorGUILayout.EndScrollView();
        }

        private static void EqualizeLength<T>(List<T> input, int count) where T : new() {
            while (input.Count != count) {
                if (input.Count < count) {
                    input.Add(new T());
                }
                else {
                    input.RemoveAt(input.Count - 1);
                }
            }
        }

        private static void EqualizeLengthNullable<T>(List<T> input, int count) where T : class {
            while (input.Count != count) {
                if (input.Count < count) {
                    input.Add(null);
                }
                else {
                    input.RemoveAt(input.Count - 1);
                }
            }
        }
    }
    #endif


    [Serializable]
    public class AudioDB : ScriptableObject {

        public List<Clip> music;
        public List<Clip> ambient;
        public List<Clip> sound;

        public AudioDB() {
            music = new List<Clip>();
            ambient = new List<Clip>();
            sound = new List<Clip>();
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

        public static Clip GetClip (ClipSettings input) {
			AudioDB adb = ScriptableObject.CreateInstance<AudioDB>();
			adb = (AudioDB)Resources.Load<AudioDB>("AudioDB");
            List<Clip> source;
            if (input is SoundClip) source = adb.sound;
            else if (input is AmbientClip) source = adb.ambient;
            else if (input is MusicClip) source = adb.music;
            else return null;
            foreach (Clip item in source) {
                if (input.name == item.name)
                    return item;
            }
            Debug.LogWarning("Specified clip not found.");
            return null;
        }
    }

    // For serializing static clip information
    [Serializable]
    public class Clip {
        public string name;
        public List<AudioClip> clips;
        public List<string> overrides; // TODO: Make this work in editor and in API
		public bool playRandom;
        private int i = -1;
        public int index {
            get {
                if (playRandom || i < 0) {
                    if (i < 0) i = 0;
                    return UnityEngine.Random.Range(0, clips.Count);
                } else {
                    i++;
                    if (i >= clips.Count)
                        i = 0;
                    return i;
                }
            }
        }

        public Clip() {
            clips = new List<AudioClip>();
            overrides = new List<string>();
            clips.Add(null);
        }

        public AudioClip GetClip () {
            return clips[index];
        }
    }

    // Base class for creating Play information
    public class ClipSettings : IEquatable<ClipSettings> {
        public string name;
        public float volume = 1;
        public float pitch = 1;

        protected ClipSettings(string _name) {
            name = _name;
        }

        public virtual bool Equals (ClipSettings other) {
            return  other.name == name &&
                    other.volume == volume &&
                    other.pitch == pitch;
        }
    }
}

public class SoundClip : AudioSystem.ClipSettings {
	public float delay = 0;

	public SoundClip(string _name) : base(_name) {}

    public SoundClip (string _name, float _delay) : base(_name) {
        delay = _delay;
    }
}

public class AmbientClip : AudioSystem.ClipSettings {
	public float rolloff = 0;
    public Collider2D bounds;

	public AmbientClip(string _name, Collider2D _bounds, float _rolloff) : this(_name, _bounds) {
		rolloff = _rolloff;
	}

	public AmbientClip(string _name, Collider2D _bounds) : base(_name) {
		bounds = _bounds;
	}
}

public class MusicClip : AudioSystem.ClipSettings {

    public bool startRandom = false;

	public MusicClip(string _name) : base(_name) {}

    public MusicClip(string _name, bool random) : this(_name) {
        startRandom = random;
    }
}
