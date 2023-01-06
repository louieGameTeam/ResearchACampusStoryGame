using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


#if UNITY_EDITOR
using UnityEditor;

public class ItemWindow : EditorWindow {
    
	string path;
    Vector2 scrollPos;
    List<bool> folded;
    string search = string.Empty;
	ItemDB idb;


	[MenuItem("Window/Item Database")]
	static void Init() {

		// Get existing open window or if none, make a new one
		ItemWindow window = (ItemWindow)GetWindow(typeof(ItemWindow));
		window.Show();
	}

	void OnEnable() {

		string[] files = AssetDatabase.FindAssets("t:ItemDB");
		if (files.Length != 0) {
			path = AssetDatabase.GUIDToAssetPath(files[0]);
		}
		Load(path);
	}

    private void Load(string filePath) {

        idb = ScriptableObject.CreateInstance<ItemDB>();
		int lastSlash = filePath.LastIndexOf('/');
		string fileName = filePath.Substring(lastSlash + 1, filePath.Length - ".asset".Length - lastSlash - 1);
		UnityEngine.Object file = Resources.Load<ItemDB>(fileName);
		if (file != null)
			idb = (ItemDB)file;
		else
			Debug.LogWarning("File not found - will create new one.");
        
		folded = new List<bool>();
	}

    public void OnGUI() {

		scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        search = EditorGUILayout.TextField("Search", search);
        EditorGUILayout.Space();

        List<Item> toRemove = new List<Item>();
        EqualizeLength<bool>(folded, idb.items.Count);
        for (int i = 0; i < idb.items.Count; i++) {
			Item item = idb.items[i];
            if (string.IsNullOrEmpty(search) || item.name.ToLower().Contains(search.ToLower())) {
                folded[i] = EditorGUILayout.Foldout(folded[i], string.IsNullOrEmpty(item.name) ? "[New Item]" : item.name);
                if (folded[i]) {
                    EditorGUI.indentLevel++;
                    item.name = EditorGUILayout.TextField("Name", item.name);
                    item.description = EditorGUILayout.TextField("Description", item.description);
                    item.icon = EditorGUILayout.ObjectField("Icon", item.icon, typeof(Sprite), false) as Sprite;
                    if (GUILayout.Button("Delete"))
                        toRemove.Add(item);
                    EditorGUILayout.Space();
                    EditorGUI.indentLevel--;
                }
            }
        }
        foreach (Item item in toRemove)
            idb.items.Remove(item);

        EditorGUILayout.Space();
		if (GUILayout.Button("New Item"))
            idb.items.Add(new Item());


		// This is necessary to save between Unity sessions
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		path = EditorGUILayout.TextField("Asset path", path);
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Load"))
			Load(path);
		if (GUILayout.Button("Save"))
			idb.SaveAsset(path);
		GUILayout.EndHorizontal();

		EditorGUILayout.EndScrollView();
	}

	public static void EqualizeLength<T>(List<T> input, int count) where T : new() {
		while (input.Count != count) {
			if (input.Count < count) {
				input.Add(new T());
			}
			else {
				input.RemoveAt(input.Count - 1);
			}
		}
	}
}
#endif


[Serializable]
public class ItemDB : ScriptableObject {

    public List<Item> items;

	public ItemDB() {
        items = new List<Item>();
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
