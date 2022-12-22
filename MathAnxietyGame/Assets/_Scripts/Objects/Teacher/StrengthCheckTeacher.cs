using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthCheckTeacher : TeacherTaskCheck
{
    /// <summary>
    /// Chekcs if strenghts are correct and changes the next dialogue based on the check
    /// </summary>
    public override void CheckTask()
    {
        base.CheckTask();
        if (GameManager.Instance.strenghtsManager.CompareCorrectStrengths())
        {
            dialogueTrigger.SwitchToDialogue(3);
            dialogueType = DialogueType.Single;
            OpenUIPanel();
        }
        else
        {
            dialogueTrigger.SwitchToDialogue(2);
            Interact();
        }
    }

    public override void OpenUIPanel()
    {
        base.OpenUIPanel();
        UIPanel.GetComponent<ChooseStrenghts>().StrengthTeacher = this;
    }

    /// <summary>
    /// Sets the 3 chosen strengths as a dialogue text for the player to check on
    /// </summary>
    public void SetChosenStrengths()
    {
        string[] chosenStrengths = GameManager.Instance.strenghtsManager.GetGoalStrengths;
        dialogueTrigger.dialogue[1].dialogueSentences[0] += 
            chosenStrengths[0] + ", " + chosenStrengths[1] + " & " + chosenStrengths[2] + ".";
    }
}
