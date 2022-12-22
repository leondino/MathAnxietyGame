using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GateTeacher : TeacherTaskCheck
{
    [SerializeField]
    private float mathAnxietyCap;

    //! Event that holds all methods that opens the gate
    public UnityEvent onOpenGate;

    public override void CheckTask()
    {
        base.CheckTask();

        // Set dialogue position to player characters
        dialogueTrigger.DialoguePosition =
                GameManager.Instance.thePlayer.GetComponentInChildren<PlayerInteraction>().transform.position;

        if (GameManager.Instance.mathAnxietyLevel <= mathAnxietyCap)
        {
            dialogueTrigger.SwitchToDialogue(2);
            Interact();
        }
        else
        {
            dialogueTrigger.SwitchToDialogue(1);
            Interact();
        }

        // Set dialogue position back to teacher
        dialogueTrigger.DialoguePosition = transform.position;
    }

    public override void Completed()
    {
        HasInteraction = false;
        dialogueTrigger.SwitchToDialogue(3);

        // Subsscribe gate action to Interact() event
        onInteract.AddListener(OpenGate);

        Interact();
    }

    public override void OpenUIPanel()
    {
        base.OpenUIPanel();
        UIPanel.GetComponent<CheckGateCode>().GateTeacher = this;
    }

    /// <summary>
    /// Opens the gate that this teacher is at
    /// </summary>
    public void OpenGate()
    {
        onOpenGate.Invoke();
    }
}
