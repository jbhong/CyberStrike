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
    [SerializeField] private GameObject option3Button;
    [SerializeField] private Text quizScoreText;

    [SerializeField] private GameObject endQuizPause;
    [SerializeField] private Text endQuizText;
    [SerializeField] private GameObject quizWinButton;
    [SerializeField] private GameObject quizLoseButton;

    private int correctAnswers;
    private bool shownPrompt;
    private int questionsAnswered;

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
        option3Button.SetActive(true);
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
            quizScoreText.text = "0 / 0";
            questionsAnswered = 0;
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
            if(option3.text == "")
            {
                option3Button.SetActive(false);
            }
            else
            {
                option3Button.SetActive(true);
            }
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
            questionsAnswered++;
            quizScoreText.text = correctAnswers +" / " + questionsAnswered;
            string[] data = mainMonitor.GetComponent<QuestionManager>().GetNextQuestion();
            if (data != null)
            {
                questionText.text = data[0];
                option1.text = data[1];
                option2.text = data[2];
                option3.text = data[3];
                if (option3.text == "")
                {
                    option3Button.SetActive(false);
                }
                else
                {
                    option3Button.SetActive(true);
                }
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
        if (correctAnswers >= Convert.ToInt32(Difficulty.GetVariables()["numQuestions"]) - 1)
        {
            if (Difficulty.level == 2)
            {
                ContinueGame();
                gameManager.SetGameOverType("complete", correctAnswers);
            }
            else
            {
                SetEndQuizText("Congratulations, with " +correctAnswers + "/"+ Convert.ToInt32(Difficulty.GetVariables()["numQuestions"]) + " you've managed to beat me this time.\n" + ScenarioManager.GetHint(Difficulty.level));
                quizWinButton.SetActive(true);
            }

        }
        else
        {
            ContinueGame();
            gameManager.SetGameOverType("incorrect", correctAnswers);
        }
    }

    public void ContinueGame()
    {
        if (infoPause.activeSelf)
        {
            if (gameManager.laptopsCollectedCounter == gameManager.laptopSpawnNumber && !shownPrompt)
            {
                infoText.text = "You have now found all the laptops. Find the super computer to start the quiz!";
                shownPrompt = true;
            }
            else
            {
                gameObject.SetActive(false);
                IsOn = false;
            }
        }
        else
        {
            gameObject.SetActive(false);
            IsOn = false;
        }
    }

    public void LeaveRoom()
    {
        SceneManager.LoadScene(lobbyName, LoadSceneMode.Single);
    }
}
