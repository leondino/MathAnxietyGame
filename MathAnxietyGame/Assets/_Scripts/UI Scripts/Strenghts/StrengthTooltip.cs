using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization;

public class StrengthTooltip : MonoBehaviour
{
    public Strength MyStrength { get; set; }
    public GameObject toolTipBox;
    [SerializeField]
    private float yOffset;
    private Vector3 myPosition;

    private void Start()
    {
        myPosition = transform.position;
        myPosition.y -= yOffset;
    }

    /// <summary>
    /// Set Tooltip box to correct position with correct text
    /// </summary>
    /// <param name="eventTrigger"></param>
    public void OnPointerEnter(EventTrigger eventTrigger)
    {
        toolTipBox.GetComponentInChildren<TextMeshProUGUI>().text = MyStrength.name.GetLocalizedString();
        toolTipBox.SetActive(true);
        toolTipBox.transform.position = myPosition;
        Debug.Log("open pop-up");
    }

    /// <summary>
    /// Hides tooltip box
    /// </summary>
    /// <param name="eventTrigger"></param>
    public void OnPointerLeave(EventTrigger eventTrigger)
    {
        toolTipBox.SetActive(false);
        Debug.Log("close pop-up");
    }
}
