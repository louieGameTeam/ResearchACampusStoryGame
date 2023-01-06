using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Switches the scene
[RequireComponent(typeof(Fading))]
public class SwitchScene : MonoBehaviour {

    // Public function to call the IEnumator
    public void ChangeScene(string scene) {
        StartCoroutine(Transition(scene));
    }

    // Actually switches the scene
    IEnumerator Transition(string scene) {
        float fadeTime = GetComponent<Fading>().BeginFade(1); // made a slight change to make it useable for the character creation scene
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(scene);
    }
}
