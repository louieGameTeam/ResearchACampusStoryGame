using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControl : MonoBehaviour {

    [SerializeField] string levelTheme;

    void Start() {

        PlayMain();

        // For debubbing purposes because it's getting annoying
        //Audio.playMusic = false;
	}

    public void PlayMain () {
        Audio.Play(levelTheme);
    }
}
