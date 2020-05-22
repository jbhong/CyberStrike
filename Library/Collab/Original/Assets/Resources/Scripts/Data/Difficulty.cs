using System.Collections.Generic;
using UnityEngine;

public static class Difficulty
{
    public static int difficulty = -1;
    public static int level;
    public static int cumulativeScore;
    private static Dictionary<string, string> variables = new Dictionary<string, string>();

    public static void SetDifficulty(int _diificulty)
    {
        difficulty = _diificulty;
        switch (difficulty)
        {
            case 0:
                {
                    variables["minutes"] = "5.0";
                    break;
                }
            case 1:
                {
                    variables["minutes"] = "4.0";
                    break;
                }
            case 2:
                {
                    variables["minutes"] = "3.0";
                    break;
                }
        }
        SetLevel(level);
        SetFileVariables(difficulty, level);
        ScenarioManager.SetDialogue(level);
    }

    private static void SetLevel(int _level)
    {
        level = _level;
        switch (level)
        {
            case 0:
                {
                    variables["laptopSpawnNumber"] = "4";
                    variables["boxesSpawnNumber"] = "3";
                    variables["numQuestions"] = "4";
                    break;
                }
            case 1:
                {
                    variables["laptopSpawnNumber"] = "6";
                    variables["boxesSpawnNumber"] = "3";
                    variables["numQuestions"] = "6";
                    break;
                }
            case 2:
                {
                    variables["laptopSpawnNumber"] = "8";
                    variables["boxesSpawnNumber"] = "3";
                    variables["numQuestions"] = "8";
                    break;
                }
        }
    }

    private static void SetFileVariables(int diff, int _level)
    {
        /* 00 -> Difficulty - Easy & Level - 1
         * 01 -> Difficulty - Easy & Level - 2
         * 02 -> Difficulty - Easy & Level - 3
         * 10 -> Difficulty - Medium & Level - 1
         * 11 -> Difficulty - Medium & Level - 2
         * 12 -> Difficulty - Medium & Level - 3
         * 20 -> Difficulty - Difficult & Level - 1
         * 21 -> Difficulty - Difficult & Level - 2
         * 22 -> Difficulty - Difficult & Level - 3
         * */
        switch (diff.ToString() + _level.ToString())
        {
            case "00":
                {
                    variables["infoPath"] = "Assets/Resources/TextFiles/CybersecurityInfoE1.txt";
                    variables["questionPath"] = "Assets/Resources/TextFiles/CybersecurityQuestionsE1.txt";
                    break;
                }
            case "01":
                {
                    variables["infoPath"] = "Assets/Resources/TextFiles/CybersecurityInfoE2.txt";
                    variables["questionPath"] = "Assets/Resources/TextFiles/CybersecurityQuestionsE2.txt";
                    break;
                }
            case "02":
                {
                    variables["infoPath"] = "Assets/Resources/TextFiles/CybersecurityInfoE3.txt";
                    variables["questionPath"] = "Assets/Resources/TextFiles/CybersecurityQuestionsE3.txt";
                    break;
                }
            case "10":
                {
                    variables["infoPath"] = "Assets/Resources/TextFiles/CybersecurityInfoM1.txt";
                    variables["questionPath"] = "Assets/Resources/TextFiles/CybersecurityQuestionsM1.txt";
                    break;
                }
            case "11":
                {
                    variables["infoPath"] = "Assets/Resources/TextFiles/CybersecurityInfoM2.txt";
                    variables["questionPath"] = "Assets/Resources/TextFiles/CybersecurityQuestionsM2.txt";
                    break;
                }
            case "12":
                {
                    variables["infoPath"] = "Assets/Resources/TextFiles/CybersecurityInfoM3.txt";
                    variables["questionPath"] = "Assets/Resources/TextFiles/CybersecurityQuestionsM3.txt";
                    break;
                }
            case "20":
                {
                    variables["infoPath"] = "Assets/Resources/TextFiles/CybersecurityInfoH1.txt";
                    variables["questionPath"] = "Assets/Resources/TextFiles/CybersecurityQuestionsH1.txt";
                    break;
                }
            case "21":
                {
                    variables["infoPath"] = "Assets/Resources/TextFiles/CybersecurityInfoH2.txt";
                    variables["questionPath"] = "Assets/Resources/TextFiles/CybersecurityQuestionsH2.txt";
                    break;
                }
            case "22":
                {
                    variables["infoPath"] = "Assets/Resources/TextFiles/CybersecurityInfoH3.txt";
                    variables["questionPath"] = "Assets/Resources/TextFiles/CybersecurityQuestionsH3.txt";
                    break;
                }
        }
    }

 
    public static void IncreaseLevel()
    {
        level++;
        cumulativeScore += UnityEngine.GameObject.FindObjectOfType<GameManager>().GetScore();
        SetDifficulty(difficulty);
    }

    public static Dictionary<string, string> GetVariables()
    {
        return variables;
    }
}
