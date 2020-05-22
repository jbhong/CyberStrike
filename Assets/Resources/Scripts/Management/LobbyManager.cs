using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System.Text.RegularExpressions;

public class LobbyManager : MonoBehaviour
{
    [SerializeField]
    private string mainSceneName = "MainLevel";

    [SerializeField]
    GameObject mainMenu;
    [SerializeField]
    GameObject loginMenu;
    [SerializeField]
    GameObject normalMenu;
    [SerializeField]
    GameObject difficultyMenu;
    [SerializeField]
    GameObject leaderboard;
    [SerializeField]
    GameObject loginStatus;
    [SerializeField]
    GameObject confirmField;
    [SerializeField]
    GameObject loginForm;
    [SerializeField]
    GameObject loginorcreate;
    [SerializeField]
    GameObject loginButton;
    [SerializeField]
    GameObject createButton;
    [SerializeField]
    GameObject loginReturnButton;
    [SerializeField]
    GameObject resetPrompt;
    [SerializeField]
    GameObject loginStatusReturnButton;


    [SerializeField]
    Text logInWaitText;

    [SerializeField]
    InputField username;

    [SerializeField]
    InputField password;

    [SerializeField]
    InputField confirm;

    [SerializeField]
    GameObject leaderBoardEntryPrefab;

    [SerializeField]
    RectTransform leaderListParent;

    [SerializeField]
    Text leaderBoardWaitingText;

    [SerializeField]
    Text prompt;

    private bool loadingScores = true;

    private bool waitingForLogin = false;

    private bool authenticated = false;

    private List<KeyValuePair<string, int>> scores = new List<KeyValuePair<string, int>>();

    List<GameObject> leaderList = new List<GameObject>();

    private void Start()
    {
        if(!Utility.IsConnected())
            Utility.ConnectToFireBase();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        bool loggedIn = Utility.Authenticated() == "Success";
        prompt.text = "";
        loginMenu.SetActive(!loggedIn);
        loginStatus.SetActive(false);
        loginForm.SetActive(false);
        loginButton.SetActive(false);
        createButton.SetActive(false);
        loginorcreate.SetActive(true);
        mainMenu.SetActive(loggedIn);
        normalMenu.SetActive(true);
        difficultyMenu.SetActive(false);
        leaderboard.SetActive(false);
        loginReturnButton.SetActive(false);
        resetPrompt.SetActive(false);
        loginStatusReturnButton.SetActive(false);
        if(!ScenarioManager.DialogueSet)
            ScenarioManager.GenerateDialogue();
    }

    private void Update()
    {
        if (Utility.scores.Count > 0 && loadingScores)
        {
            scores = Utility.GetLoadedScores();
            CreateScoreBoard();
        }

        if (!loadingScores && leaderBoardWaitingText.text != "")
        {
            leaderBoardWaitingText.text = "";
        }
        else if(loadingScores && leaderBoardWaitingText.text == "")
        {
            leaderBoardWaitingText.text = "Loading...";
        }

        if (loginMenu.activeSelf && !mainMenu.activeSelf && !loginorcreate.activeSelf) {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (confirmField.activeSelf)
                {
                    if (username.isFocused)
                    {
                        password.Select();
                    }
                    else if (password.isFocused)
                    {
                        confirm.Select();
                    }
                    else if (confirm.isFocused)
                    {
                        username.Select();
                    }
                }
                else
                {
                    if (username.isFocused)
                    {
                        password.Select();
                    }
                    else if (password.isFocused)
                    {
                        username.Select();
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                if (confirmField.activeSelf)
                    CreateAccount();
                else
                    Login();
            }
        }

        if (waitingForLogin)
        {
            string auth = Utility.Authenticated();
            if (auth == "Success")
            {
                logInWaitText.text = "";
                waitingForLogin = false;
                authenticated = true;
            }
            else if(auth == "Failure" || auth == "NoUser")
            {
                logInWaitText.text = "Failed to login";
                waitingForLogin = false;
                authenticated = false;
                loginStatusReturnButton.SetActive(true);
            }
            else if(auth == "Username")
            {
                logInWaitText.text = "Username already in use";
                waitingForLogin = false;
                authenticated = false;
                loginStatusReturnButton.SetActive(true);

            }
            else if (auth == "Email")
            {
                logInWaitText.text = "Email already in use";
                waitingForLogin = false;
                authenticated = false;
                loginStatusReturnButton.SetActive(true);
            }
        }

        if (authenticated)
        {
            loginStatus.SetActive(false);
            loginMenu.SetActive(false);
            mainMenu.SetActive(true);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Login()
    {
        if (username.text.Length > 0 && password.text.Length > 0)
        {
            Utility.Login(username.text.ToLower(), password.text.GetHashCode().ToString());
            waitingForLogin = true;
            logInWaitText.text = "Logging in...";
            loginStatus.SetActive(true);
            loginMenu.SetActive(false);
            loginStatusReturnButton.SetActive(false);
        }
        else
        {
            prompt.text = "You must enter a username and password.";
        }
    }

    public void CreateAccount()
    {
        /*Regex r = new Regex(@"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$", RegexOptions.IgnoreCase);
        MatchCollection matches = r.Matches(confirm.text);
        string emailValue = null;*/
        if(confirm.text != password.text)
        {
            prompt.text = "Passwords must match";
            return;
        }

        if(username.text.Length < 6)
        {
            prompt.text = "Username needs to be longer than 6 characters";
            return;
        }
        else
        {
            Regex r = new Regex(@"[^.a-zA-Z\d]", RegexOptions.IgnoreCase);
            MatchCollection matches = r.Matches(username.text);
            if (matches.Count > 0)
            {
                prompt.text = "Username can only contain letters and numbers";
                return;
            }
        }
        if (password.text.Length < 6)
        {
            prompt.text = "Password needs to be longer than 6 characters";
            return;
        }
        prompt.text = "";
        Utility.CreateUser(username.text.ToLower(), password.text.GetHashCode().ToString());
        waitingForLogin = true;
        logInWaitText.text = "Creating account...";
        loginStatus.SetActive(true);
        loginMenu.SetActive(false);
        loginStatusReturnButton.SetActive(false);
    }

    private void CreateScoreBoard() {
        int i = 0;
        foreach(KeyValuePair<string, int> _pair in scores)
        {
            if (_pair.Value != 0)
            {
                i++;
                GameObject _leaderBoardItem = (GameObject)Instantiate(leaderBoardEntryPrefab);
                _leaderBoardItem.transform.SetParent(leaderListParent);

                LeaderBoardEntry _entry = _leaderBoardItem.GetComponent<LeaderBoardEntry>();
                if (_entry != null)
                    _entry.Setup(i, _pair.Key, _pair.Value);

                leaderList.Add(_leaderBoardItem);
            }
        }
        loadingScores = false;
    }

    private void DestroyLeaderBoard()
    {
        for(int i = 0; i< leaderList.Count; i++)
        {
            Destroy(leaderList[i]);
        }
        leaderList.Clear();
    }

    public void ReturnToMenu()
    {
        scores.Clear();
        if (loginStatus.activeSelf)
        {
            loginStatus.SetActive(false);
            loginMenu.SetActive(true);
            waitingForLogin = false;
        }
        else if (loginMenu.activeSelf)
        {
            loginForm.SetActive(false);
            loginorcreate.SetActive(true);
            loginButton.SetActive(false);
            createButton.SetActive(false);
            confirmField.SetActive(false);
            loginReturnButton.SetActive(false);
        }
        else if (difficultyMenu.activeSelf)
        {
            normalMenu.SetActive(true);
            difficultyMenu.SetActive(false);
        }
        else
        {
            normalMenu.SetActive(true);
            leaderboard.SetActive(false);
        }
    }

    public void ShowLeaderBoard()
    {
        loadingScores = true;
        DestroyLeaderBoard();
        Utility.GetScores();
        normalMenu.SetActive(false);
        leaderboard.SetActive(true);
    }

    public void ShowLogin()
    {
        prompt.text = "Please Login.";
        loginForm.SetActive(true);
        loginorcreate.SetActive(false);
        loginButton.SetActive(true);
        confirmField.SetActive(false);
        loginReturnButton.SetActive(true);
        resetPrompt.SetActive(true);
        username.Select();
    }

    public void ShowCreate()
    {
        prompt.text = "Create Account.";
        loginForm.SetActive(true);
        loginorcreate.SetActive(false);
        createButton.SetActive(true);
        confirmField.SetActive(true);
        loginReturnButton.SetActive(true);
        resetPrompt.SetActive(false);
        username.Select();
    }

    public void ShowDifficultySelection()
    {
        normalMenu.SetActive(false);
        difficultyMenu.SetActive(true);
    }

    public void SetDifficulty(int _difficulty)
    {
        Difficulty.SetDifficulty(_difficulty);
        SceneManager.LoadSceneAsync(1);
    }
}
