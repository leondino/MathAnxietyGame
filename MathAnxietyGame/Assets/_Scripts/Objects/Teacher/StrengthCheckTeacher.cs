using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

public class StrengthCheckTeacher : TeacherTaskCheck
{
    /// <summary>
    /// Chekcs if strenghts are correct and changes the next dialogue based on the check
    /// </summary>
    public override void CheckTask()
    {
        base.CheckTask();
        SetChosenStrengths();
        if (GameManager.Instance.strenghtsManager.CompareCorrectStrengths())
        {
            dialogueTrigger.SwitchToDialogue(3);
            dialogueType = DialogueType.Single;
            Interact();
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
        LocalizedString[] chosenStrengths = GameManager.Instance.strenghtsManager.goalStrengths.ToArray();
        string chosenStrengthsString = chosenStrengths[0].GetLocalizedString() + ", " + 
            chosenStrengths[1].GetLocalizedString() + " & " + chosenStrengths[2].GetLocalizedString() + ".";
        dialogueTrigger.dialogue[1].dialogueSentences[0].Arguments = new object[] { chosenStrengthsString };
    }
}
