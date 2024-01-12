using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

public class StrengthNote : DialogueInteractable
{
    public Strength strength;
    [SerializeField]
    private Material normalMaterial, highlightedMaterial;
    [HideInInspector]
    public LocalizedString strengthButton;
    public bool canGivePower = true;
    public bool giveConfirmation = false;

    public void SelectThisStrength(LocalizedString strengthButton)
    {
        this.strengthButton = strengthButton;
        GameManager.Instance.strenghtsManager.HighlightStrenght(this);
    }

    public void UnselectThisStrength()
    {
        strengthButton = null;
        GameManager.Instance.strenghtsManager.DeHighlightStrength(this);
        CloseUIPanel();
    }

    public void HighlightColor()
    {
        GetComponent<Renderer>().material = highlightedMaterial;
    }

    public void RemoveHighlightColor()
    {
        GetComponent<Renderer>().material = normalMaterial;
    }

    public void UpdateNoteTracker()
    {
        UIPanel.GetComponent<StrenghtNoteTracker>().CurrentNote = this;
    }
}
