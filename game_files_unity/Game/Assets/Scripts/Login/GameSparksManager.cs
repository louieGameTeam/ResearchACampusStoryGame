using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSparksManager : MonoBehaviour {

    /// <summary>The GameSparks Manager singleton - copied from:
    /// https://docs.gamesparks.com/getting-started/using-authentication/unity-authentication.html</summary>
    private static GameSparksManager instance = null;
    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this.gameObject);
        }
    }
}
