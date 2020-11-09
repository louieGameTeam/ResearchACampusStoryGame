using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

    // Public function to call the IEnumator
    public void SwitchScene(string scene) {
        StartCoroutine(Transition(scene));
    }

    // Actually switches the scene
    IEnumerator Transition(string scene) {
        float fadeTime = GameObject.Find("GameManager").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(scene);
    }

    public void ExitGame(){
        Application.Quit();
    }
}