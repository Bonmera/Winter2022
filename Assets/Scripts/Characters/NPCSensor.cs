using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSensor : MonoBehaviour
{

    public DialogueTrigger dialogueToTrigger;
    [SerializeField] private GameManager gameManager;

    private void Update()
    {
        if (Input.GetKeyDown("space") && dialogueToTrigger != null && dialogueToTrigger.HasBeenTriggered())
        {
            if (DialogueManager.Instance.IsDone())
            {
                gameManager.DialogueBox.SetActive(false);

            }
            else
            {
                DialogueManager.Instance.DisplayNextSentence();
            }
        }

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !collision.isTrigger)
        {
            if (dialogueToTrigger != null)
            {
                gameManager.DialogueBox.SetActive(true);
                dialogueToTrigger.TriggerDialogue();
            }
            else
            {
                Debug.LogError("Should have dialogue set.");
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !collision.isTrigger)
        {
            if (dialogueToTrigger != null)
            {
                DialogueManager.Instance.EndDialogue();
                gameManager.DialogueBox.SetActive(false);

            }
            else
            {
                Debug.LogError("Should have dialogue set.");
            }
        }
    }
}
