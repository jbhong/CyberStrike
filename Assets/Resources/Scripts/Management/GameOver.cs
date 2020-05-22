using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    private string lobbyName = "Lobby";

    private Dictionary<string, object> user = new Dictionary<string, object>();

    [SerializeField]
    GameManager gameManager;

    private void Start()
    {
        user = Utility.GetUserLoggedIn();
    }

    public void Restart()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void LeaveRoom()
    {
        int _score = gameManager.GetScore();

        if (_score > Convert.ToInt32(user["highscore"]))
        {
            //Database data
            Utility.UpdateUsersScore(user["username"].ToString(), user["password"].ToString(), _score);
        }

        SceneManager.LoadScene(lobbyName, LoadSceneMode.Single);
    }
}