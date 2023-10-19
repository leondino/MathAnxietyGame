using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization;

public class StrengthTooltip : MonoBehaviour
{
    public Strength MyStrength { get; set; }

    public void OnPointerEnter(EventTrigger eventTrigger)
    {
        Debug.Log("open pop-up");
    }

    public void OnPointerLeave(EventTrigger eventTrigger)
    {
        Debug.Log("close pop-up");
    }
}
