using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionTeacher : TeacherTaskCheck
{
    private bool firstPartComplete = false;

    /// <summary>
    /// Checks if the emotion task has been completed and changes it's dialogue accordingly
    /// </summary>
    public override void CheckTask()
    {
        base.CheckTask();
        if (!firstPartComplete)
        {
            CheckCategorisation();
        }
        else
        {
            CheckDiscussion();
        }
    }

    private void CheckCategorisation()
    {
        if (GameManager.Instance.emotionManager.CheckAllCategorized())
        {
            dialogueTrigger.SwitchToDialogue(3);
            firstPartComplete = true;
            Interact();
        }
        else
        {
            dialogueTrigger.SwitchToDialogue(2);
            Interact();
        }
    }

    private void CheckDiscussion()
    {
        if (GameManager.Instance.emotionManager.CheckBothDiscussed())
        {
            dialogueTrigger.SwitchToDialogue(6);
            dialogueType = DialogueType.Single;
            Interact();
        }
        else
        {
            dialogueTrigger.SwitchToDialogue(5);
            Interact();
        }
    }
}
