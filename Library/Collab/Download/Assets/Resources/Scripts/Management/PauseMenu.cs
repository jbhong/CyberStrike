using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class PauseMenu : MonoBehaviour
{
    public static bool IsOn = false;

    [SerializeField] private string lobbyName = "Lobby";

    [SerializeField] private GameObject normalPause;

    [SerializeField] private GameObject infoPause;
    [SerializeField] private Text infoText;

    [SerializeField] private GameObject questionPause;
    [SerializeField] private Text questionText;
    [SerializeField] private Text option1;
    [SerializeField] private Text option2;
    [SerializeField] private Text option3;

    [SerializeField] private GameObject endQuizPause;
    [SerializeField] private Text endQuizText;
    [SerializeField] private GameObject quizWinButton;
    [SerializeField] private GameObject quizLoseButton;

    private int correctAnswers;

    private GameObject mainMonitor;
    private GameManager gameManager;
    private QuestionManager questionManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        questionManager = FindObjectOfType<QuestionManager>();

        this.gameObject.SetActive(false);
        PauseMenu.IsOn = false;

        quizLoseButton.SetActive(false);
        quizWinButton.SetActive(false);
    }

    public void SetPauseType(string _type)
    {
        this.gameObject.SetActive(true);
        PauseMenu.IsOn = true;
        if (_type.Equals("regular"))
        {
            normalPause.SetActive(true);
            infoPause.SetActive(false);
            questionPause.SetActive(false);
            endQuizPause.SetActive(false);
        }
        else if (_type.Equals("info"))
        {
            normalPause.SetActive(false);
            infoPause.SetActive(true);
            questionPause.SetActive(false);
            endQuizPause.SetActive(false);
        }
        else if (_type.Equals("question"))
        {
            normalPause.SetActive(false);
            infoPause.SetActive(false);
            questionPause.SetActive(true);
            endQuizPause.SetActive(false);
        }
        else if (_type.Equals("endQuiz"))
        {
            normalPause.SetActive(false);
            infoPause.SetActive(false);
            questionPause.SetActive(false);
            endQuizPause.SetActive(true);
        }
    }

    public void SetInfoText(string _information)
    {
        infoText.text = _information;
    }

    public void SetQuestionText(GameObject _object, string type)
    {
        string[] data = _object.GetComponent<QuestionManager>().GetNextQuestion();
        if (data != null)
        {
            questionText.text = data[0];
            option1.text = data[1];
            option2.text = data[2];
            option3.text = data[3];
            if (type == "Quiz")
            {
                mainMonitor = _object;
            }
        }
    }

    public void AnswerQuestion(int _selection)
    {
        if (mainMonitor != null)
        {
            int _rightAnswer = mainMonitor.GetComponent<QuestionManager>().GetCorrectChoice();
            if (_rightAnswer == _selection)
            {
                correctAnswers += 1;
                gameManager.PlayQuizCorrectSound();
            }
            else
            {
                gameManager.PlayQuizWrongSound();

            }
            string[] data = mainMonitor.GetComponent<QuestionManager>().GetNextQuestion();
            if (data != null)
            {
                questionText.text = data[0];
                option1.text = data[1];
                option2.text = data[2];
                option3.text = data[3];
            }
            else
            {
                EndOfQuiz();
            }
        }
    }

    private void SetEndQuizText(string _text)
    {
        endQuizText.text = _text;
    }

    private void EndOfQuiz()
    {
        SetPauseType("endQuiz");
        if (Convert.ToInt32(Difficulty.GetVariables()["numQuestions"]) == correctAnswers)
        {
            if (Difficulty.level == 2)
            {
                ContinueGame();
                gameManager.SetGameOverType("complete", correctAnswers);
            }
            else
            {
                if (Difficulty.level == 0)
                {
                    SetEndQuizText("Congratulations, you've managed to beat the culprit this time. A clue into the identity of the culprit: he/she was born in the 90's!");
                }
                else if(Difficulty.level == 1)
                {
                    SetEndQuizText("Congratulations, you've managed to beat the culprit again! A clue into the identity of the culprit: they never studied in QLD");
                }
                quizWinButton.SetActive(true);
            }

        }
        else
        {
            ContinueGame();
            gameManager.SetGameOverType("incorrect", correctAnswers);
            //SetEndQuizText("You're a fool to think you could beat me, better luck next time!");
            //quizLoseButton.SetActive(true);
        }
    }

    public void ContinueGame()
    {
        this.gameObject.SetActive(false);
        PauseMenu.IsOn = false;
    }

    public void LeaveRoom()
    {
        SceneManager.LoadScene(lobbyName, LoadSceneMode.Single);
    }
}
