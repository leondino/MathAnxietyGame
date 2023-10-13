using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue[] dialogue;
    public UnityEvent[] onDialogueEnd;

    [SerializeField]
    private bool isStatic = true;

    private int dialogueNumber = 0;

    //! Use to change where dialogue position apears
    public Vector3 DialoguePosition { get; set; }

    //! Standard appearance of dialogue position is aboven the object that started it
    private void Start()
    {
        DialoguePosition = transform.position;
    }

    /// <summary>
    /// Triggers a dialogue cloud
    /// </summary>
    public void TriggerDialogue()
    {
        if (!isStatic)
            DialoguePosition = transform.position;
        DialogueManager.Instance.StartDialogue(dialogue[dialogueNumber], this, DialoguePosition);
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
