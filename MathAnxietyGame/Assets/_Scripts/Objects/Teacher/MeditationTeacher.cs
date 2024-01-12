using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeditationTeacher : TeacherTaskCheck
{
    /// <summary>
    /// Checks if the meditation task has been completed and changes it's dialogue accordingly
    /// </summary>
    public override void CheckTask()
    {
        base.CheckTask();
        if (GameManager.Instance.MeditationCompleted)
        {
            dialogueTrigger.SwitchToDialogue(3);
            Interact();
        }
        else
        {
            dialogueTrigger.SwitchToDialogue(2);
            Interact();
        }
    }
}
