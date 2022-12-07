using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QnA
{
    [TextArea(3, 10)]
    public string question;

    [TextArea(3, 10)]
    public string answer;
}
