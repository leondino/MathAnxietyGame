using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthNote : DialogueInteractable
{
    public string strength;
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
        GetComponent<Renderer>().material = normalMaterial;
        CloseUIPanel();
    }

    public void HighlightColor()
    {
        GetComponent<Renderer>().material = highlightedMaterial;
    }

    public void UpdateNoteTracker()
    {
        UIPanel.GetComponent<StrenghtNoteTracker>().CurrentNote = this;
    }
}
