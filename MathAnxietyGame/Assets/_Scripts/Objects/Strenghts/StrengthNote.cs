using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthNote : DialogueInteractable
{
    public Strength strength;
    [SerializeField]
    private Material normalMaterial, highlightedMaterial;

    public void SelectThisStrength()
    {
        GameManager.Instance.strenghtsManager.HighlightStrenght(this);
        CloseUIPanel();
    }

    public void UnselectThisStrength()
    {
        GameManager.Instance.strenghtsManager.DeHighlightStrength(strength);
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
