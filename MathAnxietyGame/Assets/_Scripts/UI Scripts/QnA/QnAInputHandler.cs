using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

public class QnAInputHandler : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public TMP_InputField answerInput;

    private Queue<LocalizedString> questionSentences = new Queue<LocalizedString>();
    private LocalizedString currentQuestion = null;

    [SerializeField]
    private List<QnA> QnAList;

    public void StartQnA(Dialogue questions, List<QnA> QnAList)
    {
        questionText.text = null;
        answerInput.text = null;
        questionSentences.Clear();
        this.QnAList = QnAList;

        foreach (LocalizedString question in questions.dialogueSentences)
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
                question = currentQuestion,
                answer = answerInput.text
            };
            QnAList.Add(newQnA);
        }

        if (questionSentences.Count == 0)
        {
            EndQnA();
            return;
        }

        currentQuestion = questionSentences.Dequeue();
        questionText.text = currentQuestion.GetLocalizedString();
        answerInput.text = null;
    }

    private void EndQnA()
    {
        GameManager.Instance.UIIsActive = false;
        transform.parent.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
