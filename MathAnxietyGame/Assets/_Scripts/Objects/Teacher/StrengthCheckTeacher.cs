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
        if (GameManager.Instance.strenghtsManager.CompareCorrectStrengths())
        {
            dialogueTrigger.SwitchToDialogue(4);
            dialogueType = DialogueType.Single;
            Interact();
        }
        else
        {
            dialogueTrigger.SwitchToDialogue(3);
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
        if (chosenStrengths.Length > 1)
        {
            string chosenStrengthsString = chosenStrengths[0].GetLocalizedString() + ", " +
                chosenStrengths[1].GetLocalizedString() + "& " + chosenStrengths[2].GetLocalizedString() + ".";
            dialogueTrigger.dialogue[2].dialogueSentences[0].Arguments = new object[] { chosenStrengthsString };
            Debug.Log(chosenStrengthsString);
        }
    }

    /// <summary>
    /// Continues dialogue boxes after chosing 3 strengths
    /// </summary>
    public void ContinueTaskExplanation()
    {
        autoOpenUI = false;
        Interact();
    }
}
