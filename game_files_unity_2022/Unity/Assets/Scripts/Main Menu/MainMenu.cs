using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {


    /********* COMPILE TIME SETTINGS TO CONTROL BUILD TYPE *********/

    // Use a local save instead of an online backend save, bypasses login and writes to binary
    public static bool useOffline = false;

    // Allows all levels to be playable from Level Select, forces offline mode
    // Saved Task data is reinitialized when loading nonconsecutive levels
    public static bool demoMode = false;


    /***************************************************************/





    public static bool isOffline { get { return useOffline || !LoginControl.loggedIn || demoMode; } }

    [SerializeField] RectTransform main;
    [SerializeField] RectTransform levelsGrid;
    [SerializeField] RectTransform about;
    [SerializeField] RectTransform howToPlay;

    List<RectTransform> pages;
    public static string activeScene;
    public static int levelSchedule = 0; // 1-based
    public static bool toSelect = false;
    private bool credits = false;

	private List<Level> levels;

	void Awake () {

        if (isOffline) {
            transform.Find("Main/Buttons/Bottom/Logout").gameObject.SetActive(false);
        }
	}

    private void Start() {

        // For debugging
        //Tasks.game = new Game("7");
        //Debug.Log(Tasks.game.ToString());

        pages = new List<RectTransform>();
        LoadLevelSelect();
        Audio.StopMusic();
        if (toSelect) {
            toSelect = false;
            Show(levelsGrid);
        }
    }

	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.R))
            Back();
	}

    public void Show (RectTransform target) {

        if (target == levelsGrid && toSelect)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        if (!pages.Contains(target))
            pages.Add(target);
        else
            return;

        // Go to Character Creation if this is the first load
        if (target == levelsGrid) {
            SaveData sd = SaveData.ReadGameSave();
            if (sd == null || (sd != null && sd.character == null)) {
                BlackFade.LoadScene("CharacterCreation");
                return;
            }
        }

        target.gameObject.SetActive(true);
        main.gameObject.SetActive(false);
    }

    public void Back () {
        if (pages.Count == 0) return;
        pages[pages.Count - 1].gameObject.SetActive(false);
        pages.RemoveAt(pages.Count - 1);
        main.gameObject.SetActive(true);
    }

    static string FormatTime (float sec) {
        int seconds = Mathf.CeilToInt(sec);
        int days = Mathf.FloorToInt(seconds / (60 * 60 * 24));
        seconds -= days * 60 * 60 * 24;
        int hours = Mathf.FloorToInt(seconds / (60 * 60));
        seconds -= hours * 60 * 60;
        int minutes = Mathf.FloorToInt(seconds / 60);
        seconds -= minutes * 60;
        return days.ToString() + ":" +
               (hours < 10 ? "0" : "") + hours.ToString() + ":" +
               (minutes < 10 ? "0" : "") + minutes.ToString() + ":" +
               (seconds < 10 ? "0" : "") + seconds.ToString();
    }

    IEnumerator LevelTimer(Text timer, int index) {
        string week = timer.text;
        float remaining = LoginControl.schedule[index] - (Time.realtimeSinceStartup - LoginControl.scheduleTime);
        if (remaining < 0) yield break;
        while (remaining > 0) {
            remaining = LoginControl.schedule[index] - (Time.realtimeSinceStartup - LoginControl.scheduleTime);
            timer.text = week + '\n' + FormatTime(remaining);
            yield return new WaitForSeconds(1);
        }
        toSelect = true;
        if (timer.gameObject.activeInHierarchy)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadLevelSelect() {

        List<float> schedule = LoginControl.schedule;
        if (isOffline || schedule == null) {
            levelSchedule = Tasks.levels.Count;
        }
        else {
            bool notFound = false;
            for (int i = 0; i < schedule.Count; i++) {
                if (notFound) continue;
                float elapsed = Time.realtimeSinceStartup - LoginControl.scheduleTime;
                float remaining = schedule[i] - elapsed;
                if (remaining < 0)
                    levelSchedule = i + 1;
                else
                    notFound = true;
            }
        }

        levels = Tasks.levels;
        int index = Tasks.levels.IndexOf(Tasks.currentLevel);   // 0-based
        int lastPlayable = Mathf.Min(index, levelSchedule - 1); // 0-based
        Transform list = levelsGrid.Find("Levels");
        if (!isOffline) {
            int start = Mathf.Max(0, levelSchedule);
            for (int i = start; i < list.childCount; i++) {
                Text timer = list.GetChild(i).Find("Week").GetComponent<Text>();
                timer.fontSize = 32;
                StartCoroutine(LevelTimer(timer, i));
            }
        }
        if (demoMode || Tasks.game.finished)
            lastPlayable = list.childCount - 1;
        if (lastPlayable < 0) return;
        activeScene = levels[lastPlayable].scene;
        for (int i = 0; i <= lastPlayable; i++) {
            float p = levels[i].progress;
            Text week = list.GetChild(i).Find("Week").GetComponent<Text>();
            string progress = "Week " + (i + 1).ToString() + '\n';
            if (p >= 1 || Tasks.game.finished)
                progress += "Complete";
            else
                progress += Mathf.FloorToInt(p * 100).ToString() + "%";
            week.text = progress;
            week.fontSize = 32;
            Text title = list.GetChild(i).Find("Title").GetComponent<Text>();
            title.enabled = true;
            title.text = levels[i].name;
            list.GetChild(i).Find("Lock").GetComponent<Image>().enabled = false;
        }
		if (demoMode)
			for (int i = 0; i < list.childCount; i++)
				list.GetChild (i).GetComponent<Button> ().interactable = true;
        else if (index <= levelSchedule - 1 && !Tasks.game.finished)
            list.GetChild(lastPlayable).GetComponent<Button>().interactable = true;
		
    }

	public void StartGame (int index) {

        Serialization.lastTime = Time.realtimeSinceStartup;

        if (Tasks.levels.IndexOf(Tasks.currentLevel) != index) {
            Tasks.game = new Game(index.ToString());
            activeScene = levels[index].scene;
        } else if (isOffline)
            activeScene = levels[index].scene;

        if (!string.IsNullOrEmpty(activeScene))
            BlackFade.LoadScene(activeScene);
    }

    public void OpenURL (string url) {
        Application.OpenURL(url);
    }

    public void CreditsToggle () {
        credits = !credits;
        about.Find("Title").GetComponent<Text>().text = credits ? "Credits" : "About";
        about.Find("Credits/Text").GetComponent<Text>().text = !credits ? "Credits" : "About";
        about.Find("Info").gameObject.SetActive(!credits);
        about.Find("List").gameObject.SetActive(credits);

    }

    public void Logout () {
        //new EndSessionRequest()
        //.Send((response) => {
        //    SceneManager.LoadScene("Login");
        //});
    }
}
