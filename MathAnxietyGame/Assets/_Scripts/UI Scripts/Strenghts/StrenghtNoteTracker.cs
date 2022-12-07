using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrenghtNoteTracker : MonoBehaviour
{
    public StrengthNote CurrentNote { get; set; }

    public void HighlightNote()
    {
        CurrentNote.SelectThisStrength();
    }

    public void DeHighlightNote()
    {
        CurrentNote.UnselectThisStrength();
    }
}
