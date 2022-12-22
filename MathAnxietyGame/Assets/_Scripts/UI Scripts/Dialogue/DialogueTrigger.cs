using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue[] dialogue;
    public UnityEvent[] onDialogueEnd;

    private int dialogueNumber = 0;

    //! Use to change where dialogue position apears
    [HideInInspector]
    public Vector3 dialoguePosition;

    //! Standard appearance of dialogue position is aboven the object that started it
    private void Start()
    {
        dialoguePosition = transform.position;
    }

    /// <summary>
    /// Triggers a dialogue cloud
    /// </summary>
    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogue[dialogueNumber], this, dialoguePosition);
    }

    /// <summary>
    /// Activates the actions after a certain dialogue ended
    /// </summary>
    public void DialogueEndAction()
    {
        if (onDialogueEnd[dialogueNumber].GetPersistentEventCount() != 0)
        {
            onDialogueEnd[dialogueNumber].Invoke();
        }
    }

    /// <summary>
    /// Go to a specified dialogue and dialogue end action
    /// </summary>
    /// <param name="dialogueNumber"></param>
    public void SwitchToDialogue(int dialogueNumber)
    {
        this.dialogueNumber = dialogueNumber;
    }

    /// <summary>
    /// Go to next dialogue and dialogue end action
    /// </summary>
    public void NextDialogue()
    {
        dialogueNumber++;
    }

    /// <summary>
    /// Go to previous dialogue and dialogue end action
    /// </summary>
    public void PreviousDialogue()
    {
        dialogueNumber--;
    }
}
