using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    public TextMeshProUGUI NameText;
    public TextMeshProUGUI dialogueText;

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

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Starting conversation with " + dialogue.name);

        NameText.text = dialogue.name;
        sentences.Clear();
        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }


    //Called for every sentence of dialogue. If called while still displaying
    //then will immediately display all text
    public void DisplayNextSentence()
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

    void EndDialogue()
    {
        Debug.Log("End Conversation.");
    }

}
