using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using Proyecto26;
using System.Text.RegularExpressions;
using UnityEngine.Events;

public class LoginControl : MonoBehaviour
{
    [SerializeField]
    InputField email;
    [SerializeField]
    InputField password;
    [SerializeField]
    Button loginButton;
    [SerializeField]
    Button signUpPageButton;
    [SerializeField]
    Button resetButton;
    [SerializeField]
    Text message;
    [SerializeField]
    SignUp signUpData;

    Firebase firebase => Firebase.instance;

    public static string sid;
    public static bool loggedIn { get; private set; }
    public static List<float> schedule;
    public static float scheduleTime = 0;


    void Awake()
    {
        loginButton.onClick.AddListener(OnLogInClick);
        signUpData.Initialize(OnSignUpClick, SignUpVerificationFailed);

        resetButton.onClick.AddListener(ResetPassword);
        loggedIn = true;
        if (MainMenu.isOffline)
            MainPage();
    }

    void SignUpVerificationFailed(string message)
    {
        ShowMessage(message);
    }

    private void ResetPassword()
    {
        message.enabled = false;
        if (IsValid(email.text))
        {
            firebase.ResetPassword(email.text, OnPasswordResetCompleted, OnPasswordResetFailed);
        }
        else
        {
            ShowMessage("Email not valid!");
        }
    }

    public static bool IsValid(string email)
    {
        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        Match match = regex.Match(email);
        return match.Success;
    }
    private void OnPasswordResetCompleted()
    {
        ShowMessage("Reset link sent to your email!");
    }

    private void OnPasswordResetFailed(RequestException exception)
    {
        if (exception.IsNetworkError)
        {
            ShowMessage("Network connection error!");
        }
        else
        {
            ShowMessage("Reset link sent to your email!");
        }
    }

    void OnSignUpClick(string email, string password, string fullName)
    {
        this.message.enabled = false;
        firebase.SignUp(email, password, fullName, SignUpComplete, SignUpFailed);
    }

    private void SignUpComplete()
    {
        firebase.GetSchedule(schedule =>
        {
            LoginControl.schedule = schedule;
            scheduleTime = Time.realtimeSinceStartup;
            Serialization.cached = new SaveData();
            Serialization.cached.userInfo = new SaveData.UserInfo(signUpData.firstName.text, signUpData.lastName.text, signUpData.email.text);

            Serialization.log = new GameLog();
            MainPage();
        });
    }

    private void SignUpFailed(Proyecto26.RequestException exception)
    {
        this.message.enabled = true;
        if (exception.IsHttpError)
        {
            ShowMessage("Email already registered!");
        }
        else
        {
            ShowMessage("Network connection error!");
        }
    }

    void OnLogInClick()
    {
        this.message.enabled = false;
        firebase.SignIn(email.text, password.text, LoginComplete, LogInFailed);
    }

    private void LoginComplete()
    {
        firebase.GetData(data =>
        {
            Serialization.cached = data;
            if (Serialization.log != null)
            {
                MainPage();
            }
        },
        () =>
        {
            ShowMessage("Server missing data!");
        });

        firebase.GetProgress(data =>
        {
            Serialization.log = data;
            if (Serialization.cached != null)
            {
                MainPage();
            }
        },
        () =>
        {
            ShowMessage("Server missing data!");
        });
    }

    private void LogInFailed(Proyecto26.RequestException exception)
    {
        this.message.enabled = true;
        if (exception.IsHttpError)
        {
            ShowMessage("Wrong email or password!");
        }
        else
        {
            ShowMessage("Network connection error!");
        }
    }

    void ShowMessage(string message)
    {
        this.message.enabled = true;
        this.message.text = message;
    }

    void MainPage()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    [System.Serializable]
    class SignUp
    {
        public InputField firstName;
        public InputField lastName;
        public InputField email;
        public InputField password;
        public InputField confirmPassword;
        public Button signUp;

        public UnityEvent<string, string, string> onInputVerificationCompleted = new UnityEvent<string, string, string>();
        public UnityEvent<string> onInputVerificationFailed = new UnityEvent<string>();

        public void Initialize(UnityAction<string, string, string> onInputVerificationCompleted, UnityAction<string> onInputVerificationFailed)
        {
            signUp.onClick.AddListener(VerifyInput);
            this.onInputVerificationCompleted.AddListener(onInputVerificationCompleted);
            this.onInputVerificationFailed.AddListener(onInputVerificationFailed);
        }
        void VerifyInput()
        {
            if (firstName.text == string.Empty)
            {
                onInputVerificationFailed.Invoke("First name is required!");
                return;
            }
            else if (lastName.text == string.Empty)
            {
                onInputVerificationFailed.Invoke("Last name is required!");
                return;
            }
            else if (!IsValid(email.text))
            {
                onInputVerificationFailed.Invoke("Email is not valid!");
                return;
            }
            else if (password.text == string.Empty)
            {
                onInputVerificationFailed.Invoke("Password is missing!");
                return;
            }
            else if (password.text.Length < 6)
            {
                onInputVerificationFailed.Invoke("Password needs to be at least 6 character!");
                return;
            }
            else if (confirmPassword.text != password.text)
            {
                onInputVerificationFailed.Invoke("Password do not match!");
                return;
            }

            onInputVerificationCompleted.Invoke(email.text, password.text, firstName.text + " " + lastName.text);
        }

    }
}
