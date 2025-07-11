using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


[System.Serializable]
public class LevelData : ScriptableObject {

	// List of all XML files containing level information
	public List<TextAsset> levelData;

	public LevelData() {
		levelData = new List<TextAsset>();
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


#if UNITY_EDITOR

public class LevelWindow : EditorWindow {

	string path;
	Vector2 scrollPos;
	LevelData levelXML;


	[MenuItem("Window/Level XML")]
	static void Init() {

		// Get existing open window or if none, make a new one
		LevelWindow window = (LevelWindow)GetWindow(typeof(LevelWindow));
		window.Show();
	}

	void OnEnable() {

		string[] files = AssetDatabase.FindAssets("t:LevelData");
		if (files.Length != 0) {
			path = AssetDatabase.GUIDToAssetPath(files[0]);
		}
		Load(path);
	}

	private void Load(string filePath) {

		levelXML = ScriptableObject.CreateInstance<LevelData>();
		string fileName = string.Empty;
		if (!string.IsNullOrEmpty(filePath)) {
			int lastSlash = filePath.LastIndexOf('/');
			fileName = filePath.Substring(lastSlash + 1, filePath.Length - ".asset".Length - lastSlash - 1);
		}
		UnityEngine.Object file = Resources.Load<LevelData>(fileName);
		if (file != null)
			levelXML = (LevelData)file;
		else
			Debug.LogWarning("File not found - will create new one.");
	}

	public void OnGUI() {

		scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

		int count = EditorGUILayout.IntField("Level count", levelXML.levelData.Count);
		ItemWindow.EqualizeLength<TextAsset>(levelXML.levelData, count);
		for (int i = 0; i < levelXML.levelData.Count; i++) {
			levelXML.levelData[i] = EditorGUILayout.ObjectField("Level " + (i + 1).ToString(), levelXML.levelData[i], typeof(TextAsset), false) as TextAsset;
		}


		// This is necessary to save between Unity sessions
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		path = EditorGUILayout.TextField("Asset path", path);
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Load"))
			Load(path);
		if (GUILayout.Button("Save"))
			levelXML.SaveAsset(path);
		GUILayout.EndHorizontal();

		EditorGUILayout.EndScrollView();
	}
}
#endif