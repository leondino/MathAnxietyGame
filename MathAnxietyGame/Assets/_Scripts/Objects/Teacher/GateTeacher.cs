using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTeacher : TeacherTaskCheck
{
    [SerializeField]
    private float mathAnxietyCap;

    public override void CheckTask()
    {
        base.CheckTask();

        // Set dialogue position to player characters
        dialogueTrigger.dialoguePosition =
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
        dialogueTrigger.dialoguePosition = transform.position;
    }
}
