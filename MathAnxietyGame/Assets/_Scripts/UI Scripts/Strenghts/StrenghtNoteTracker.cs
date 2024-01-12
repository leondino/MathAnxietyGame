using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class StrenghtNoteTracker : MonoBehaviour
{
    [SerializeField]
    private List<Button> strengthButtons = new List<Button>();

    [SerializeField]
    private Button confirmButton;

    public StrengthNote CurrentNote { get; set; }
    public StrengthNote ConfirmationNote { get; set; }

    private void Start()
    {
        foreach (Button strengthButton in strengthButtons)
        {
            strengthButton.onClick.AddListener(delegate
            {
                HighlightNote(strengthButton.GetComponentInChildren<LocalizeStringEvent>().StringReference);
                confirmButton.onClick = strengthButton.onClick;
            });
        }
    }

    public void HighlightNote(LocalizedString strengthButton)
    {
        CurrentNote.SelectThisStrength(strengthButton);
    }

    public void DeHighlightNote()
    {
        if (ConfirmationNote != null)
        {
            ConfirmationNote.giveConfirmation = true;
            ConfirmationNote = null;
        }
        CurrentNote.UnselectThisStrength();
    }

    public void SetStrenghtHighlightUI()
    {
        ManageStrengths strengthsManager = GameManager.Instance.strenghtsManager;

        for (int i = 0; i < strengthButtons.Count; i++)
        {
            strengthButtons[i].GetComponentInChildren<LocalizeStringEvent>().StringReference = strengthsManager.goalStrengths[i];
        }
    }
}
