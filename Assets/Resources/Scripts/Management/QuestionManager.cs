using UnityEngine;
using System;

public class QuestionManager : MonoBehaviour
{
    private string[,] questionData;
    private int[] correctChoices;
    private int nextQuestion = -1;
    private int numQuestions;

    public void SetNumQuestions(int _numQuestions)
    {
        numQuestions = _numQuestions;
        questionData = new string[numQuestions, 4];
        correctChoices = new int[numQuestions];
    }

    public void SetQuestionData(int questionNo, string _question, string _option1, string _option2, string _option3, int _correct)
    {
        questionData[questionNo, 0] = _question;
        questionData[questionNo, 1] = _option1;
        questionData[questionNo, 2] = _option2;
        questionData[questionNo, 3] = _option3;
        correctChoices[questionNo] = _correct;
    }

    public string[] GetNextQuestion()
    {
        nextQuestion++;
        string[] data = new string[4];
        if (nextQuestion < numQuestions)
        {
            data[0] = questionData[nextQuestion, 0];
            data[1] = questionData[nextQuestion, 1];
            data[2] = questionData[nextQuestion, 2];
            data[3] = questionData[nextQuestion, 3];

        }
        else
        {
            data = null;
        }
        return data;
    }

    public int GetCorrectChoice()
    {
        return correctChoices[nextQuestion];
    }
}
