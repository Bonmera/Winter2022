using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


// Should be on its own gameObject

// Set up the dialogue to display in the DialogueTrigger class
public class DialogueManager : MonoBehaviour
{

    private static DialogueManager _instance;
    public static DialogueManager Instance
    {
        get
        {
            return _instance;
        }
    }


    private Queue<string> sentences;
    private string sentence;
    private int isDone = -1;

    [Tooltip("The reference to a TextMeshPro gameObject where the characters name should be displayed")]
    public TextMeshProUGUI NameText;
    [Tooltip("The reference to a TextMeshPro gameObject where the dialogue")]
    public TextMeshProUGUI dialogueText;

    //Holds the current Coroutine running that is typing dialogue.
    //If not null then stop the coroutine and put all dialogue in dialogue Text
    private Coroutine TypingCoroutine;



    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    // Start is called before the first frame update

    void Start()
    {
        sentences = new Queue<string>();
    }


    /* SHOULD BE CALLED ONLY THROUGH DIALOGUETRIGGER
     * Use to start a dialogue event. */
    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Starting conversation with " + dialogue.name);

        NameText.text = dialogue.name;
        isDone = 0;
        sentences.Clear();
        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }


    /* Called for every sentence of dialogue. 
     * If called while still displaying
     * then will immediately display all text 
     * param (choice): pass what sentence should be read next depending on
     * players choice
     */
    public void DisplayNextSentence(int choice = 0)
    {
        if(TypingCoroutine != null)
        {
            dialogueText.text = sentence;
            StopCoroutine(TypingCoroutine);
            TypingCoroutine = null;
            return;
        }
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        sentence = sentences.Dequeue();
        //Debug.Log(sentence);
        TypingCoroutine = StartCoroutine(TypeSentence(sentence));
    }
    

    //Types the sentence on screen one letter at a time
    //(Should only be called by DisplayNextSentence)
    IEnumerator TypeSentence(string sentence)
    {
        sentence.Replace("$Name", NameText.text);   //not optimal, might find a better place/way depending on
                                                    //tokens used

        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(.02f);
        }
    }

    public void EndDialogue()
    {
        sentences.Clear();
        sentence = "";
        isDone = 1;
        if (TypingCoroutine != null)
        {
            StopCoroutine(TypingCoroutine);
            TypingCoroutine = null;
        }
        Debug.Log("End Conversation.");
    }

    public bool IsDone()
    {
        if (isDone == 1)
            return true;
        return false;
    }
}
