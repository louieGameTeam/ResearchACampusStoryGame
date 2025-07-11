using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterCreation;
using System.IO;
using UnityEngine.UI;

public class OptionsUpdater : MonoBehaviour {

    [SerializeField] GameObject customize;
    [SerializeField] Text playerName;
    [SerializeField] RectTransform photo;
    [SerializeField] RectTransform controls;

	void Start () {
		UpdateCard();
	}

    public void UpdateCard () {
        CharacterSetting cs = FindObjectOfType<PlayerControl>().GetComponent<Character>().cs;
        playerName.text = cs.name;
        ChatManager.GenerateIcon(cs, photo);
        Transform player = photo.GetChild(1);
        player.localScale = Vector3.one * 250;
        ShowControls(false);
        LoadAudioSettings();
    }

    public void OnHover () {
        customize.SetActive(true);
    }

    public void OffHover () {
        customize.SetActive(false);
    }

    public void OnClick () {
        SaveData.WriteGameSave();
        //UnityEngine.SceneManagement.SceneManager.LoadScene("CharacterCreation");
        BlackFade.LoadScene("CharacterCreation");
    }

    public void Exit () {
        SaveData.WriteGameSave();
        //UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        BlackFade.LoadScene("MainMenu");
    }

    public void ShowControls(bool isOn) {
        controls.gameObject.SetActive(isOn);
    }

    public void ToggleMusic (bool isOn) {
        Audio.playMusic = isOn;
    }

    public void ToggleSounds(bool isOn) {
        ChatManager.GetCM().playAudio = isOn;
    }

    private void LoadAudioSettings () {
        SaveData.MuteSettings ms = SaveData.ReadGameSave().muteSettings;
        if (ms == null) return;
        Toggle music = transform.Find("Music").GetComponent<Toggle>();
        Toggle sounds = transform.Find("Sounds").GetComponent<Toggle>();
        music.isOn = ms.playMusic;
        sounds.isOn = ms.playSounds;
    }
}
