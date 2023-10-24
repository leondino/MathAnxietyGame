using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

public class StrenghtNoteTracker : MonoBehaviour
{
    [SerializeField]
    private List<LocalizeStringEvent> strengthButtons = new List<LocalizeStringEvent>();

    public StrengthNote CurrentNote { get; set; }

    public void HighlightNote()
    {
        CurrentNote.SelectThisStrength();
    }

    public void DeHighlightNote()
    {
        CurrentNote.UnselectThisStrength();
    }

    public void SetStrenghtHighlightUI()
    {
        ManageStrengths strengthsManager = GameManager.Instance.strenghtsManager;

        for (int i = 0; i < strengthButtons.Count; i++) 
        {
            strengthButtons[i].StringReference = strengthsManager.goalStrengths[i];
        }
    }
}
