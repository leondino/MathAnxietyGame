using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Script to handle the code screen at a gate task
/// </summary>
public class CheckGateCode : MonoBehaviour
{
    public GateTeacher GateTeacher { get; set; }

    [SerializeField]
    private TMP_InputField codeInput;

    [SerializeField]
    private int correctCode;

    /// <summary>
    /// Checks if the code filled in code is correct or not
    /// </summary>
    public void CheckCode()
    {
        if (codeInput.text == correctCode.ToString())
        {
            OnCorrectCode();
        }
        else
        {
            OnIncorrectCode();
        }
    }

    public void OnCorrectCode()
    {
        Debug.Log("Code was correct!");

        GateTeacher.Completed();

        // Close UI
        gameObject.SetActive(false);
        transform.parent.gameObject.SetActive(false);
        GameManager.Instance.UIIsActive = false;
    }

    public void OnIncorrectCode()
    {
        Debug.Log("Code was incorrect!");
        codeInput.text = null;
    }
}
