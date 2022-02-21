using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class NPCSensor : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField]
    private NPCConversation[] conversations;    
    private bool isDialogTriggered = false;
    private int conversationIndex = 0;

    private void Start()
    {
        
    }

    private void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player" && !collision.isTrigger && isDialogTriggered == false && conversations.Length > conversationIndex)
        {
            isDialogTriggered = true;
            ConversationManager.Instance.StartConversation(conversations[conversationIndex]);
        }

    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !collision.isTrigger && isDialogTriggered == true)
        {
            isDialogTriggered = false;
            ConversationManager.Instance.EndConversation();
        }
    }

    public void UpdateConversationIndex()
    {
        conversationIndex++;
    }


}
