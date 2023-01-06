using Proyecto26;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Events;

public class Firebase : MonoBehaviour
{
    static Firebase _instance;

    public static Firebase instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Firebase>();
                if (_instance == null)
                {
                    _instance = new GameObject("Firebase").AddComponent<Firebase>();
                }
            }

            return _instance;
        }
    }

    [SerializeField]
    FirebaseSettings settings;
    string databaseLink => settings.database + "{0}.json";
    string updateProfileLink => string.Format("https://identitytoolkit.googleapis.com/v1/accounts:update?key={0}", settings.apiKey);
    string signUpLink => string.Format("https://identitytoolkit.googleapis.com/v1/accounts:signUp?key={0}", settings.apiKey);
    string signInLink => string.Format("https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={0}", settings.apiKey);
    string resetLink => string.Format("https://identitytoolkit.googleapis.com/v1/accounts:sendOobCode?key={0}", settings.apiKey);
    string scheduleLink => string.Format(databaseLink, "schedule");

    string progressLink => string.Format(databaseLink, "students/" + user.localId + "/progress");
    string dataLink => string.Format(databaseLink, "students/" + user.localId + "/data");

    const string dateFormat = "MM/dd/yyyy hh:mm tt UTC";

    User user = new User();
    SignInData signInData = new SignInData();
    public System.DateTime currentTime => System.DateTime.Now;

    void Awake()
    {
        if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

    }

    public void SignUp(string email, string password, string fullName, UnityAction onComplete, UnityAction<Proyecto26.RequestException> onFailed)
    {
        string json = "{\"email\":\"" + email + "\",\"password\":\"" + password + "\",\"returnSecureToken\":true}";
        RestClient.Post<User>(signUpLink, json).Then(data =>
        {
            user = data;
            UpdateProfile(fullName);
            onComplete.Invoke();
        }).Catch(rejection =>
        {
            onFailed.Invoke(rejection as Proyecto26.RequestException);
        });
    }

    public void UpdateProfile(string fullName)
    {
        string idToken = user.idToken;
        string json = "{\"idToken\":\"" + idToken + "\",\"displayName\":\"" + fullName + "\",\"photoUrl\":\"\",\"returnSecureToken\":true}";
        RestClient.Post(updateProfileLink, json);
    }

    public void SignIn(string email, string password, UnityAction onComplete, UnityAction<Proyecto26.RequestException> onFailed)
    {
        string json = "{\"email\":\"" + email + "\",\"password\":\"" + password + "\",\"returnSecureToken\":true}";
        RestClient.Post<SignInData>(signInLink, json).Then(data =>
        {
            signInData = data;
            user.localId = data.localId;
            onComplete.Invoke();
        }).Catch(rejection =>
        {
            onFailed.Invoke(rejection as Proyecto26.RequestException);
        });
    }

    public void ResetPassword(string email, UnityAction onComplete, UnityAction<Proyecto26.RequestException> onFailed)
    {
        string json = "{\"requestType\":\"PASSWORD_RESET\",\"email\":\"" + email + "\"}";
        RestClient.Post(resetLink, json).Then(response =>
        {
            onComplete.Invoke();
        }).Catch(rejected =>
        {
            onFailed.Invoke(rejected as Proyecto26.RequestException);
        });
    }

    public void GetSchedule(UnityAction<List<float>> onReceived)
    {
        RestClient.Get<Shedule>(scheduleLink).Then(res =>
        {
            onReceived.Invoke(res.floatDates);
        }).Catch(response => print(response.Message));
    }

    public void SaveData(SaveData data, UnityAction onComplete)
    {
        RestClient.Put<SaveData>(dataLink, data).Then(data => onComplete.Invoke()).Catch(rejected => Debug.Log(rejected.Message));
    }

    public void SaveProgress(GameLog gameLog, UnityAction onComplete)
    {
        RestClient.Put<GameLog>(progressLink, gameLog).Then(log => onComplete.Invoke()).Catch(rejected => Debug.Log(rejected.Message));
    }

    public void GetData(UnityAction<SaveData> onReceived, UnityAction onFailed)
    {
        RestClient.Get<SaveData>(dataLink).Then(data => onReceived.Invoke(data)).
            Catch(rejected =>
            {
                Debug.LogError(rejected.Message);
                onFailed.Invoke();
            });
    }

    public void GetProgress(UnityAction<GameLog> onReceived, UnityAction onFailed)
    {
        RestClient.Get<GameLog>(progressLink).Then(log => onReceived.Invoke(log)).
            Catch(rejected =>
            {
                Debug.LogError(rejected.Message);
                onFailed.Invoke();
            });
    }

    [SerializeField]
    class Shedule
    {
        public List<string> dates = new List<string>()
        {
            DateTime.UtcNow.ToString(dateFormat),
            DateTime.UtcNow.ToString(dateFormat),
            DateTime.UtcNow.ToString(dateFormat),
            DateTime.UtcNow.ToString(dateFormat),
            DateTime.UtcNow.ToString(dateFormat),
            DateTime.UtcNow.ToString(dateFormat),
            DateTime.UtcNow.ToString(dateFormat),
            DateTime.UtcNow.ToString(dateFormat),
            DateTime.UtcNow.ToString(dateFormat)
        };

        public List<float> floatDates
        {
            get
            {
                List<float> result = new List<float>();
                foreach (var date in dates)
                {
                    DateTime startTime;
                    DateTime.TryParseExact(date, dateFormat, null, DateTimeStyles.None, out startTime);
                    long elapsedTicks = startTime.Ticks - DateTime.UtcNow.Ticks;
                    TimeSpan elapsedSpan = new TimeSpan(elapsedTicks);
                    result.Add((float)elapsedSpan.TotalSeconds);
                }

                return result;
            }
        }
    }

    [System.Serializable]
    class User
    {
        public string idToken;
        public string email;
        public string refreshToken;
        public string expiresIn;
        public string localId;
    }

    [System.Serializable]
    class SignInData
    {
        public string localId;
        public string email;
        public string displayName;
        public string idToken;
        public bool registered;
        public string refreshToken;
        public string expiresIn;
    }
}
