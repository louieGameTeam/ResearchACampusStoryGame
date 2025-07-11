using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioSystem;

public static class Audio {

    public static string currentMusic {
        get {
            return GetLocal().currentMusic.name;
        }
    }

    // Used as a setting for whether or not music should play
    public static bool playMusic {
        set {
            AudioSource ads = audioMan.GetComponent<AudioSource>();
            if (ads.enabled && !value) {
                wasPlaying = ads.isPlaying;
                playTime = ads.time;
            }
            ads.enabled = value;
            if (value && wasPlaying) {
                ads.time = playTime;
                ads.Play();
            }
        }
        get {
            return audioMan.GetComponent<AudioSource>().enabled;
        }
    }
    private static bool wasPlaying = false;
    private static float playTime = 0;

    // Multiplier for the %/sec that music fades in and out
    public static float fadeSpeed = 1.0f;

    // Request to play the given SoundClip
    public static void Play(SoundClip clip) {
        audioMan.sounds.Add(clip);
    }

    // Request to play the given AmbientClip
    public static void Play(AmbientClip clip) {
        GetLocal();
        AmbientTrigger at = clip.bounds.gameObject.AddComponent<AmbientTrigger>();
        at.ac = clip;
    }

    // Request to play the given MusicClip
    public static void Play(MusicClip clip) {
        audioMan.ChangeMusic(clip);
    }

    // Request to play the first Sound or Music clip with the given name with default settings
    public static void Play(string clip) {
        ClipSettings c = GetClip(clip);
        if (c is SoundClip)
            Play((SoundClip)c);
        else if (c is MusicClip)
            Play((MusicClip)c);
    }

    // Stop any music clip if there is one playing
    public static void StopMusic() {
        audioMan.ChangeMusic(null);
    }

    // Stop all ambient clips with the given name
    public static void StopAmbient(string _name) {
        AmbientTrigger[] ats = GameObject.FindObjectsOfType<AmbientTrigger>();
        foreach (AmbientTrigger item in ats) {
            if (item.ac.name == _name)
                Object.Destroy(item);
        }
    }

    // Stop all ambient clips attached to the given collider
    public static void StopAmbient(Collider2D _bounds) {
        AmbientTrigger[] ats = _bounds.GetComponents<AmbientTrigger>();
        foreach (AmbientTrigger item in ats) {
            Object.Destroy(item);
        }
    }

    public static ClipSettings GetClip (string clip) {
        AudioDB adb = ScriptableObject.CreateInstance<AudioDB>();
        adb = (AudioDB)Resources.Load<AudioDB>("AudioDB");
        foreach (Clip item in adb.sound) {
            if (item.name == clip) {
                SoundClip sc = new SoundClip(clip);
                return sc;
            }
        }
        foreach (Clip item in adb.music) {
            if (item.name == clip) {
                MusicClip mc = new MusicClip(clip);
                return mc;
            }
        }
        Debug.LogWarning("Clip '" + clip + "' not found.");
        return null;
    }

    /************************************************************/

    private static AudioManager audioMan {
        get {
            if (audioManReference == null)
                audioManReference = GetLocal();
            return audioManReference;
        }
    }
    private static AudioManager audioManReference;

    private static AudioManager GetLocal() {
        AudioManager[] allAudios = GameObject.FindObjectsOfType<AudioManager>();
        if (allAudios.Length > 0)
            return allAudios[0];
        GameObject go = new GameObject();
        go.name = "Audio";
        AudioManager a = go.AddComponent<AudioManager>();
        AudioSource music = a.gameObject.AddComponent<AudioSource>();
        music.spatialBlend = 0;
        music.playOnAwake = false;
        music.volume = 0;
        music.loop = true;
        Object.DontDestroyOnLoad(go);
        return a;
    }

}