using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Localization;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;

    private DialogueTrigger trigger;

    private Queue<string> dialogueSentences;
    private static DialogueManager _instance;

    // Singleton of DialogueManager
    public static DialogueManager Instance
    {
        get
        {
            if (_instance is null)
                Debug.Log("Dialogue Manager is NULL");
            return _instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
        dialogueSentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue, DialogueTrigger trigger, Vector3 position)
    {
        dialogueBox.GetComponentInParent<Bilboard>().SetBilboard();
        Vector3 dialoguePosition = position;
        dialoguePosition.y += 2;
        dialogueBox.transform.position = dialoguePosition;
        dialogueBox.SetActive(true);

        this.trigger = trigger;

        dialogueSentences.Clear();

        foreach (LocalizedString sentence in dialogue.dialogueSentences)
        {
            dialogueSentences.Enqueue(sentence.GetLocalizedString());
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (dialogueSentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = dialogueSentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    private void EndDialogue()
    {
        dialogueBox.SetActive(false);
        trigger.DialogueEndAction();
    }

}
