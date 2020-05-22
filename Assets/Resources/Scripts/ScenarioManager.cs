public static class ScenarioManager
{
    public static bool DialogueSet;

    private static Dialogue currentDialogue;

    private static Scenario currentScenario;

    public static void GenerateDialogue()
    {
        currentScenario = Scenarios.GetRandomScenario();

        DialogueSet = true;
    }

    public static void SetDialogue(int _dialogueNum)
    {
        Dialogue _dialogue = new Dialogue();
        string[] _sentences;
        if (_dialogueNum == 0)
        {
            _sentences = currentScenario.FirstString.Split(':');
        }
        else if (_dialogueNum == 1)
        {
            _sentences = currentScenario.SecondString.Split(':');
        }
        else
        {
            _sentences = currentScenario.ThirdString.Split(':');
        }
        _dialogue.SetText(_sentences);
        currentDialogue = _dialogue;
    }

    public static Dialogue GetDialogue()
    {
        return currentDialogue;
    }

    public static string GetHint(int _level)
    {
        if(_level == 0)
        {
            return currentScenario.Hint1;
        }
        else if(_level == 1)
        {
            return currentScenario.Hint2;
        }
        return null;

    }

    public static string GetEndQuote()
    {
        return currentScenario.EndMessage;
    }
}
