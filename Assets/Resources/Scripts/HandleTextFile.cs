using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

public class HandleTextFile : MonoBehaviour
{
    public string[] information;

    public Dictionary<int, Question> questions = new Dictionary<int, Question>();

    public List<int> questionsToUse = new List<int>();
    public List<int> questionsUsed = new List<int>();
    public List<int> keyList;

    public void GenerateData(string infoPath, string questionPath)
    {
        ReadInformation(infoPath, questionPath);
    }

    public Question GetRandomQuestion()
    {
        int randomKey = keyList[UnityEngine.Random.Range(0, keyList.Count)];
        while (questionsUsed.Contains(randomKey) && (questionsToUse.Count - questionsUsed.Count > 0))
        {
            randomKey = keyList[UnityEngine.Random.Range(0, keyList.Count)];
        }
        questionsUsed.Add(randomKey);
        if(questionsUsed.Count == questionsToUse.Count)
        {
            questionsUsed.Clear();
        }
        return questions[randomKey];
    }

    private void ReadInformation(string infoPathString, string questionPathString)
    {
        /*string infoPath = infoPathString;

        string[] _reader = File.ReadAllLines(infoPath);*/
        string[] _reader = Information.GetLevelData(infoPathString);

        foreach(string _info in _reader)
        {
            string[] _qs = _info.Split(':')[1].Split(',');
            foreach(string _q in _qs)
            {
                if(!questionsToUse.Contains(Convert.ToInt32(_q)))
                    questionsToUse.Add(Convert.ToInt32(_q));
            }
        }

        information = _reader;
        ReadQuestions(questionPathString);
    }

    private void ReadQuestions(string questionPathString)
    {
        /*string questionPath =  questionPathString;

        string[] _reader = File.ReadAllLines(questionPath);*/
        string[] _reader = Questions.GetLevelData(questionPathString);

        foreach (string _question in _reader)
        {
            string[] _parts = _question.Split(':');
            if (questionsToUse.Contains(Convert.ToInt32(_parts[0])))
            {
                questions.Add(Convert.ToInt32(_parts[0]), new Question
                {
                    QuestionString = _parts[1],
                    Option1 = _parts[2],
                    Option2 = _parts[3],
                    Option3 = _parts[4],
                    Correct = Convert.ToInt32(_parts[5])
                });
            }

        }
        keyList = new List<int>(questions.Keys);
    }
}
