using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GateTeacher : TeacherTaskCheck
{
    [SerializeField]
    private float mathAnxietyCap;

    [SerializeField]
    private int correctGateCode = 123;

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
            dialogueTrigger.SwitchToDialogue(3);
            Interact();
        }
        else
        {
            dialogueTrigger.SwitchToDialogue(2);
            Interact();
        }

        // Set dialogue position back to teacher
        dialogueTrigger.DialoguePosition = transform.position;
    }

    public override void Completed()
    {
        HasInteraction = false;
        dialogueTrigger.SwitchToDialogue(4);

        // Subsscribe gate action to Interact() event
        onInteract.AddListener(OpenGate);

        Interact();
    }

    public override void OpenUIPanel()
    {
        base.OpenUIPanel();
        UIPanel.GetComponent<CheckGateCode>().GateTeacher = this;
        UIPanel.GetComponent<CheckGateCode>().correctCode = correctGateCode;
    }

    /// <summary>
    /// Opens the gate that this teacher is at
    /// </summary>
    public void OpenGate()
    {
        onOpenGate.Invoke();
    }
}
