public class Scenario
{
    public string FirstString { get; set; }
    public string SecondString { get; set; }
    public string ThirdString { get; set; }
    public string Hint1 { get; set; }
    public string Hint2 { get; set; }
    public string EndMessage { get; set; }
}

public static class Scenarios
{
    private static Scenario[] scenarios = {
        new Scenario {
            FirstString = "You are a police detective that has imprisoned 5 cybercriminals in the past. One of these cybercriminals has hacked into your police security system and threatens to wipe out all of humanity through the control of the police security robots.:" +
                            "Any of these cybercriminals could have a personal vendetta against you, but only one is the real culprit.:" +
                            "These are the profiles of the five suspects\n\nInitials                    M.J     L.R     K.F     M.I     P.W\nYear of birth         1994   1992   1989   1995   1987\nState of study        WA      WA   QLD    WA   NSW:" +
                            "(1) Find all the laptops and gain the necessary information from them to\n(2) Answer the quiz questions correctly that will be found on the master-computer, get 1 or less wrong to move to the next level.\nAt the end of winning each level you will be given a hint that will help expose the identity of the hacker.\n-Beware the security robots will attack you (not whilst reading the laptop information or answering quiz questions)\n-Once you start the quiz you cannot go back to the laptops",
            SecondString = "You have narrowed down the suspects to the following three\nInitials                    M.J     L.R     M.I\nYear of birth         1994   1992   1995\nState of study        WA      WA   WA",
            ThirdString = "You have narrowed down the suspects to the last two\nInitials                    M.J     L.R \nYear of birth         1994   1992\nState of study        WA      WA",
            Hint1 = "I'll give you a clue into the identity of the culprit, he/she was born in the 90's!",
            Hint2 = "I'll give you another clue into the identity of the culprit, the last name doesn't start with I!",
            EndMessage = "You have found the hacker - Mackenzie Jones! Thank you for your help!"
},
        new Scenario {
            FirstString = "You are a police detective that has imprisoned 5 cybercriminals in the past. One of these cybercriminals has hacked into your police security system and threatens to wipe out all of humanity through the control of the police security robots.:" +
                            "Any of these cybercriminals could have a personal vendetta against you, but only one is the real culprit.:" +
                            "These are the profiles of the five suspects\n\nInitials                    M.J     L.R     K.F     M.I     P.W\nYear of birth         1994   1992   1989   1995   1987\nState of study        WA      WA   QLD    WA   NSW:" +
                            "(1) Find all the laptops and gain the necessary information from them to\n(2) Answer the quiz questions correctly that will be found on the master-computer, get 1 or less wrong  to move to the next level.\nAt the end of winning each level you will be given a hint that will help expose the identity of the hacker.\n-Beware the security robots will attack you (not whilst reading the laptop information or answering quiz questions)\n-Once you start the quiz you cannot go back to the laptops",
            SecondString = "You have narrowed down the suspects to the following three\nInitials                    M.J     L.R     M.I\nYear of birth         1994   1992   1995\nState of study        WA      WA   WA",
            ThirdString = "You have narrowed down the suspects to the last two\nInitials                    M.J     M.I\nYear of birth         194   1995\nState of study        WA      WA",
            Hint1 = "I'll give you a clue into the identity of the culprit, he/she wasn't born in the 80's!",
            Hint2 = "I'll give you another clue into the identity of the culprit, the last name doesn't start with R!",
            EndMessage = "You have found the hacker - Michael Isner! Thank you for your help!"
},
           new Scenario {
            FirstString = "You are a police detective that has imprisoned 5 cybercriminals in the past. One of these cybercriminals has hacked into your police security system and threatens to wipe out all of humanity through the control of the police security robots.:" +
                            "Any of these cybercriminals could have a personal vendetta against you, but only one is the real culprit.:" +
                            "These are the profiles of the five suspects\n\nInitials                    M.J     M.I     P.W     L.R     K.F\nYear of birth         1998   1991   1988   1996   1989\nState of study        WA      WA   NSW    NSW   QLD:" +
                            "(1) Find all the laptops and gain the necessary information from them to\n(2) Answer the quiz questions correctly that will be found on the master-computer, get 1 or less wrong  to move to the next level.\nAt the end of winning each level you will be given a hint that will help expose the identity of the hacker.\n-Beware the security robots will attack you (not whilst reading the laptop information or answering quiz questions)\n-Once you start the quiz you cannot go back to the laptops",
            SecondString = "You have narrowed down the suspects to the following three\nInitials                    M.J     M.I     K.F\nYear of birth         1998   1991   1989\nState of study        WA      WA   QLD",
            ThirdString = "You have narrowed down the suspects to the last two\nInitials                    M.J     M.I\nYear of birth         1998   1991\nState of study        WA      WA",
            Hint1 = "I'll give you a clue into the identity of the culprit, he/she did not study in NSW!",
            Hint2 = "I'll give you another clue into the identity of the culprit, he/she studied in WA!",
            EndMessage = "You have found the hacker - Mackenzie Jones! Thank you for your help!"
},
             new Scenario {
            FirstString = "You are a police detective that has imprisoned 5 cybercriminals in the past. One of these cybercriminals has hacked into your police security system and threatens to wipe out all of humanity through the control of the police security robots.:" +
                            "Any of these cybercriminals could have a personal vendetta against you, but only one is the real culprit.:" +
                            "These are the profiles of the five suspects\n\nInitials                    K.F     L.R     P.W     M.I     M.J\nYear of birth         1981   1980   1990   1986   1995\nState of study        WA      QLD   NSW    WA   QLD:" +
                            "(1) Find all the laptops and gain the necessary information from them to\n(2) Answer the quiz questions correctly that will be found on the master-computer, get 1 or less wrong  to move to the next level.\nAt the end of winning each level you will be given a hint that will help expose the identity of the hacker.\n-Beware the security robots will attack you (not whilst reading the laptop information or answering quiz questions)\n-Once you start the quiz you cannot go back to the laptops",
            SecondString = "You have narrowed down the suspects to the following three\nInitials                    L.R     P.W     M.J\nYear of birth         1980   1990   1995\nState of study        QLD     NSW   QLD",
            ThirdString = "You have narrowed down the suspects to the last two\nInitials                    P.W     M.J\nYear of birth         1990  1995\nState of study        NSW      QLD",
            Hint1 = "I'll give you a clue into the identity of the culprit, he/she did not study in WA!",
            Hint2 = "I'll give you another clue into the identity of the culprit, he/she was born in the 90's!",
            EndMessage = "You have found the hacker - Peter Walker! Thank you for your help!"
}
    };

    public static Scenario GetRandomScenario()
    {
        return scenarios[UnityEngine.Random.Range(0, scenarios.Length)];
    }
}
