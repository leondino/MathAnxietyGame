using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

[System.Serializable]
public class QnA
{
    public LocalizedString question;

    [TextArea(3, 10)]
    public string answer;
}
