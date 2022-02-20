using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//Class needed to separate Dialogue from the manager
public class DialogueTrigger : MonoBehaviour
{

    [Tooltip("This is where you store your dialogue. Each block of dialogue is a sentence. Create" +
        "as many sentences as you like but make sure it doesnt overflow from the dialogue box in the Scene")]
    public Dialogue dialogue;
    private bool hasBeenTriggered = false;


    //Called through a trigger/collision or button press
    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
        hasBeenTriggered = true;
    }

    public bool HasBeenTriggered()
    {
        return hasBeenTriggered;
    }
}
