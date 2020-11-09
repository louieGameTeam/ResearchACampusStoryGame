using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class FadeManager : MonoBehaviour {

    public List<Action> fadeActions;
    public bool hasRun { get; private set; }
    public bool newScene = false;
    public bool hasReset { get; private set; }

    void Awake() {
        fadeActions = new List<Action>();
        DontDestroyOnLoad(gameObject);
    }


    public void Fade(bool crossScene, float time, Action action) {
        fadeActions.Add(action);
        newScene = crossScene;
        StartCoroutine(FadeProcess(time));
    }

    IEnumerator FadeProcess (float time) {
        BlackFade bf = GetCamera();
        EventSystem eventSystem = EventSystem.current;
        Canvas[] canvases = new Canvas[0];
        RenderMode[] modes = new RenderMode[0];
        if (newScene) { 
            if (eventSystem != null)
                eventSystem.enabled = false;
            canvases = GameObject.FindObjectsOfType<Canvas>();
            for (int i = 0; i < canvases.Length; i++) {
                canvases[i].renderMode = RenderMode.ScreenSpaceCamera;
                canvases[i].worldCamera = Camera.main;
            }
        }
        while (bf.intensity < 1f) {
            yield return null;
            bf.intensity += Time.deltaTime / time;
        }
        bf.intensity = 1.0f;
        foreach (Action item in fadeActions) {
            item.Invoke();
        }
        hasRun = true;
        if (newScene) {
            yield return null;
            hasReset = true;
            if (eventSystem == null)
                eventSystem = EventSystem.current;
            if (eventSystem != null)
            eventSystem.enabled = false;
            bf = GetCamera();
            bf.intensity = 1.0f;
            canvases = GameObject.FindObjectsOfType<Canvas>();
            modes = new RenderMode[canvases.Length];
            for (int i = 0; i < canvases.Length; i++) {
                modes[i] = canvases[i].renderMode;
                canvases[i].renderMode = RenderMode.ScreenSpaceCamera;
                canvases[i].worldCamera = Camera.main;
            }
        }
        while (bf.intensity > 0f) {
            yield return null;
            bf.intensity -= Time.deltaTime / time;
        }
        bf.intensity = 0f;
        if (newScene) {
            if (eventSystem != null)
                eventSystem.enabled = true;
            for (int i = 0; i < canvases.Length; i++)
                canvases[i].renderMode = modes[i];
        }
        Destroy(gameObject);
    }

    public static BlackFade GetCamera () {
        BlackFade bf = Camera.main.GetComponent<BlackFade>();
        if (bf == null)
            bf = Camera.main.gameObject.AddComponent<BlackFade>();
        return bf;
    }
}
