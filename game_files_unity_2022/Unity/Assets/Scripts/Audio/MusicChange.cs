using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class MusicChange : MonoBehaviour {

    [SerializeField] string localTheme;
    [SerializeField] bool enterRandom;
    [SerializeField] bool exitRandom;
    private string previousTheme = string.Empty;


	void OnTriggerEnter2D(Collider2D collision) {
        if (!collision.CompareTag("Player")) return;
        previousTheme = Audio.currentMusic;
        MusicClip mc = new MusicClip(localTheme, enterRandom);
        Audio.Play(mc);
	}

    void OnTriggerExit2D(Collider2D collision) {
        if (!collision.CompareTag("Player")) return;
        MusicClip mc = new MusicClip(previousTheme, exitRandom);
        Audio.Play(mc);
    }


}
