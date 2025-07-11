using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fading : MonoBehaviour
{
    [SerializeField]
    Texture2D fadeOutTexture;       // Texture that will overlay the screen
    [SerializeField]
    float fadeSpeed = 0.0f;         // Fading speed

    private int drawDepth = -1000;  // Texture's order in draw hiearchy; low number renders on top
    private float alpha = 1.0f;     // The transparency value between 0 and 1
    private int fadeDir = -1;       // Direction to fade: in = -1 or out = 1

    private void OnGUI()
    {   // Fade out/in the alpha value using a direction, a speed and Time.deltaTime to convert operation to seconds
        alpha += fadeDir * fadeSpeed * Time.deltaTime;
        // Force (clamp) the number between 0 and 1
        alpha = Mathf.Clamp01(alpha);

        // Set color of GUI. All color values remain the same
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);            // Set alpha
        GUI.depth = drawDepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);   // Draw texture to fit entire screen
    }

    // Set fadeDir to direction parameter making the scene fade in if -1 and out if 1
    public float BeginFade(int direction)
    {
        fadeDir = direction;
        return fadeSpeed;       // Return fadespeed variable so it's easy to time the SceneManager.LoadScene();
    }

    // Check for a scene change
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Stop listening for a scene change when script is disabled
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    // Called when level is loaded through OnEnable(). It takes loaded level index (int) as a paramter so you can limit the fade in to certain scenes
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        BeginFade(-1);
    }
}