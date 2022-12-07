using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QnAInputHandler : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public TMP_InputField answerInput;

    private Queue<string> questionSentences = new Queue<string>();

    [SerializeField]
    private List<QnA> QnAList;

    public void StartQnA(Dialogue questions, List<QnA> QnAList)
    {
        questionText.text = null;
        answerInput.text = null;
        questionSentences.Clear();
        this.QnAList = QnAList;

        foreach (string question in questions.dialogueSentences)
        {
            questionSentences.Enqueue(question);
        }
        NextQuestion();
    }

    public void NextQuestion()
    {
        if (questionText.text != null)
        {
            if (answerInput.text == "")
            {
                Debug.Log("No answer filled in!");
                return;
            }
            QnA newQnA = new QnA()
            {
                question = questionText.text,
                answer = answerInput.text
            };
            QnAList.Add(newQnA);
        }

        if (questionSentences.Count == 0)
        {
            EndQnA();
            return;
        }

        string question = questionSentences.Dequeue();
        questionText.text = question;
        answerInput.text = null;
    }

    private void EndQnA()
    {
        GameManager.Instance.UIIsActive = false;
        transform.parent.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
