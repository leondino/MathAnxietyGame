using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GateTeacher : TeacherTaskCheck
{
    [SerializeField]
    private float mathAnxietyCap;

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
        dialogueTrigger.SwitchToDialogue(3);

        // Subsscribe gate action to Interact() event

        Interact();
    }
}
