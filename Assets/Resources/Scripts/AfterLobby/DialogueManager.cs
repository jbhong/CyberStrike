using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public Text nameText;
    public Text dialogueText;

    public GameObject startButton;
    public GameObject startPrompt;
    public GameObject Controller;
    public Animator animator;

    private Queue<string> sentences;

    private string currentSentence;

    private Dialogue dialogue;

    private bool printingText;
    private bool finishingText;

    void Start()
    {
        sentences = new Queue<string>();
        startButton.SetActive(false);
        startPrompt.SetActive(false);
        Controller.SetActive(false);
        dialogue = ScenarioManager.GetDialogue();

        StartDialogue();
    }

    public void StartDialogue ()
    {
        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }
    
    public void DisplayNextSentence ()
    {
        if (!printingText)
        {
            if (sentences.Count == 0)
            {
                EndDialogue();
                return;
            }
            string sentence = sentences.Dequeue();
            currentSentence = sentence;
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }
        else
        {
            if (!finishingText)
            {
                StopAllCoroutines();
                StartCoroutine(FinishTextTyping());
            }
        }
    }

    IEnumerator FinishTextTyping()
    {
        finishingText = true;
        dialogueText.text = currentSentence;
        yield return new WaitForSeconds(.5f);
        printingText = false;
        finishingText = false;
     }

    IEnumerator TypeSentence (string sentence)
    {
        printingText = true;
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(.02f);

        }
        printingText = false;
    }
   
    void EndDialogue()
    {
        dialogueText.text = "";
        animator.SetBool("IsOpen", false);
        startButton.SetActive(true);
        startPrompt.SetActive(true);
        Controller.SetActive(true);
    }
}
