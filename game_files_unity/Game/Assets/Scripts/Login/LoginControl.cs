using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using GameSparks.Core;
using GameSparks.Api.Requests;


public class LoginControl : MonoBehaviour {

    public static string sid;
    private static string secondSID = "";
    private static string token;
    public static bool loggedIn { get; private set; }
    public static List<float> schedule;
    public static float scheduleTime = 0;

    //GameObject logo;
    //Text title;
    Text notice;
    InputField inputSID;
    InputField inputConfirm;
    GameObject submit;
    GameObject signup;
    GameObject login;
    GameObject back;
    bool inPrimary = true;

    int idLength = 5;


	void Awake() {

        loggedIn = true;
        if (MainMenu.isOffline)
            MainPage();

        //logo = transform.Find("Logo").gameObject;
        //title = transform.Find("Title").GetComponent<Text>();
        notice = transform.Find("Error").GetComponent<Text>();
        inputConfirm = transform.Find("Confirm/Field").GetComponent<InputField>();
        inputSID = transform.Find("SID/Field").GetComponent<InputField>();
        submit = transform.Find("Submit").gameObject;
        signup = transform.Find("Signup").gameObject;
        login = transform.Find("Login").gameObject;
        back = transform.Find("Back").gameObject;
	}

    void Update() {

        GameObject g = EventSystem.current.currentSelectedGameObject;
        GameObject t = null;

        bool tab = Input.GetKeyDown(KeyCode.Tab);
        bool enter = Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter);
        bool shift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        bool forward = (tab && !shift) || enter;
        bool backward = tab && shift && !enter;

        if (forward) {
            if (g == inputSID.gameObject)
                t = inPrimary ? login : inputConfirm.gameObject;
            else if (g == inputConfirm.gameObject)
                t = submit;
        } else if (backward) {
            if (g == login || g == inputConfirm.gameObject)
                t = inputSID.gameObject;
            else if (g == submit)
                t = inputConfirm.gameObject;
        }

        if (enter) {
            Button b = g.GetComponent<Button>();
            if (b != null && b.interactable)
                b.onClick.Invoke();
            if (t != null) {
                b = t.GetComponent<Button>();
                if (b != null && b.interactable)
                    b.onClick.Invoke();
            }
        } else if (t != null) {
            Selectable s = t.GetComponent<Selectable>();
            if (s == null || (s != null && s.interactable))
                EventSystem.current.SetSelectedGameObject(t);
        }
	}

    public void Continue () {
        StartCoroutine(Login());
    }

    public void EditSID (string input) {
        sid = input;
        bool valid = input.Length >= idLength;
        login.GetComponent<Button>().interactable = valid;
        submit.GetComponent<Button>().interactable = valid && secondSID == input;

        if (!inPrimary)
            EditConfirm(secondSID);
    }

    public void SetSID (string input) {
        bool valid = input.Length >= idLength;
        //if (!inPrimary)
            //ConfirmSID(secondSID);

        if (!valid && !inPrimary) {
            inputSID.text = string.Empty;
            SetNotice("SID must be at least " + idLength.ToString() + " characters long");
        } else {
            SetNotice("");
        }
    }

    public void EditConfirm (string input) {
        bool valid = input == sid;
        secondSID = input;
        submit.GetComponent<Button>().interactable = valid && sid.Length >= idLength;
        if (valid) SetNotice("");
    }

    public void SetConfirm (string input) {
        SetNotice(input == sid ? "" : "IDs do not match");
    }

    IEnumerator Login () {

        Button but = login.GetComponent<Button>();
        but.interactable = false;
        GSData data = null;
        bool done = false;
        bool errors = false;
        new AuthenticationRequest()
            .SetUserName(sid)
            .SetPassword(string.Empty)
            .Send((response) => {
                if (response.HasErrors) {
                    errors = true;
                    if (response.Errors.GetString("DETAILS") == "UNRECOGNISED")
                        SetNotice("Error: Unknown login information");
                    else if (response.Errors.GetString("DETAILS") == "LOCKED")
                        SetNotice("Error: Too many failed attempts - This account has been temporarily locked, try again in a few minutes");
                    else
                        SetNotice("Error: Could not login");
                } else {
                    data = response.ScriptData;
                }
                done = true;
            }
        );
        const float timeout = 5f;
        float t = 0;
        for (t = 0; t < 5f && !done; t += Time.unscaledDeltaTime)
            yield return null;
        but.interactable = true;
        if (t > timeout || errors) {
            if (t > timeout)
                SetNotice("Error: Connection timed out");
            yield break;
        }

        done = false;
        new AccountDetailsRequest()
        .Send((response) => {
            data = response.ScriptData;
            done = true;
            errors = response.HasErrors;
        });
        for (t = 0; t < 5f && !done; t += Time.unscaledDeltaTime)
            yield return null;
        if (t > timeout || errors) {
            if (t > timeout)
                SetNotice("Error: Connection timed out");
            yield break;
        }

        if (data != null) {
            string saveString = data.GetString("DAT");
            if (!string.IsNullOrEmpty(saveString)) {
                SaveData sd = JsonUtility.FromJson<SaveData>(saveString);
                Serialization.cached = (object)sd;
            }
            string progressLog = data.GetString("PROGRESS");
            if (!string.IsNullOrEmpty(progressLog))
                Serialization.log = JsonUtility.FromJson<GameLog>(progressLog);
            schedule = data.GetFloatList("SCHEDULE");
            scheduleTime = Time.realtimeSinceStartup;
        }

        MainPage();
    }

    void MainPage () {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    private void SetNotice(string input) {
        notice.enabled = true;
        notice.text = input;
    }

    public void ToConfirm (bool enter) {
        inPrimary = !enter;

        if (!notice.text.Contains("Registration successful"))
            SetNotice("");

        inputConfirm.transform.parent.gameObject.SetActive(enter);
        submit.SetActive(enter);
        signup.SetActive(!enter);
        login.SetActive(!enter);
        back.SetActive(enter);

        submit.transform.Find("Text").GetComponent<Text>().text = "Create account";

        EventSystem.current.SetSelectedGameObject(inputSID.gameObject);
    }

    public void Register () {
        // TODO: Check if username is on list of enrolled students
        bool enrolled = true;
        if (!enrolled) {
            SetNotice("Error: The ID above is not registered for this course");
            return;
        }

        new RegistrationRequest()
            .SetDisplayName(sid)
            .SetPassword(string.Empty)
            .SetUserName(sid)
            .Send((response) => {
                if (!response.HasErrors) {
                    SetNotice("Registration successful: account created for " + sid);
                    ToConfirm(false);
                }
                else if (response.Errors.ContainsKey("USERNAME")) {
                    SetNotice("Error: This address is already registered");
                }
                else {
                    SetNotice("Error: Could not register player");
                }
            }
        );
    }
}
