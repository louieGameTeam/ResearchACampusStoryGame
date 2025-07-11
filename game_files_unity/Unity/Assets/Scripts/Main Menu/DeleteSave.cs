using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeleteSave : MonoBehaviour {

    static KeyCode escapeKey = KeyCode.Escape;
    static int numOfPresses = 3;
    static float delayThreshhold = 0.25f;

    static int currentPresses;
    static float delayTimer = 0;


    void Awake () {
        if (GameObject.Find("DeleteSave-1") != null || !MainMenu.demoMode)
            DestroyImmediate(gameObject);
        else {
            DontDestroyOnLoad(gameObject);
            name = "DeleteSave-1";
        }
    }

	void Update () {

        if (currentPresses > 0)
            delayTimer += Time.deltaTime;
        if (Input.GetKeyDown(escapeKey)) {
            delayTimer = 0;
            if (delayTimer < delayThreshhold)
                currentPresses++;
        }
        if (delayTimer > delayThreshhold)
            currentPresses = 0;

        if (currentPresses >= numOfPresses) {
            currentPresses = 0;
            System.IO.File.Delete(SaveData.filePath);
            SaveData.cachedSave = null;
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
            Debug.LogWarning("Save file deleted at " + Time.realtimeSinceStartup);
        }
	}
}
