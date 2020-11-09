using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;

[ExecuteInEditMode]
public class BlackFade : MonoBehaviour {

    [Range(0, 1)]
    public float intensity;
    private Material material;

    void Awake() {
        material = new Material(Shader.Find("Hidden/FadeBlack"));
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination) {
        if (intensity == 0) {
            Graphics.Blit(source, destination);
            return;
        }
        material.SetFloat("_blackFade", intensity);
        Graphics.Blit(source, destination, material);
    }

    //public static void StartFade (bool crossScene, float time, Action action) {
    //    List<Action> actions = new List<Action>();
    //    FadeManager[] managers = GameObject.FindObjectsOfType<FadeManager>();
    //    for (int i = managers.Length - 1; i >= 0; i--) {
    //        if (!managers[i].hasRun)
    //            foreach (Action item in managers[i].fadeActions)
    //                actions.Add(item);
    //        if (managers[i].newScene)
    //            crossScene = true;
    //        DestroyImmediate(managers[i].gameObject);
    //        if (managers[i].hasRun && !managers[i].hasReset)
    //            FadeManager.GetCamera().intensity = 1.0f;
    //    }
    //    GameObject manager = new GameObject();
    //    manager.name = "Fade Manager";
    //    FadeManager fm = manager.AddComponent<FadeManager>();
    //    foreach (Action item in actions)
    //        fm.fadeActions.Add(item);
    //    fm.Fade(crossScene, time, action);
    //}

    public static void LoadScene(string scene) {
        /*StartFade(true, 1f, () => */SceneManager.LoadScene(scene);//);
    }

    public static void LoadScene(int index) {
        /*StartFade(true, 1f, () => */SceneManager.LoadScene(index);//);
    }
}