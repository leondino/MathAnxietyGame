using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogueTrigger))]
public class DialogueInteractable : Interactable
{
    public enum DialogueType
    {
        Single, Repeated, Multiple
    }
    public DialogueType dialogueType;

    [HideInInspector]
    public DialogueTrigger dialogueTrigger;

    [SerializeField][Tooltip("This object can open a specific UI panel when the dialogue has ended.")]
    public GameObject UIPanel;

    [SerializeField]
    private bool autoOpenUI;

    protected override void Start()
    {
        base.Start();
        dialogueTrigger = GetComponent<DialogueTrigger>();
    }

    public override void Interact()
    {
        base.Interact();
    }

    public virtual void ActivateDialogueEndAction()
    {
        if (dialogueType != DialogueType.Single)
        {
            HasInteraction = false;
        }

        if (dialogueType == DialogueType.Multiple)
        {
            dialogueTrigger.NextDialogue();
        }

        if (UIPanel != null && autoOpenUI)
        {
            OpenUIPanel();
        }
    }

    public virtual void OpenUIPanel()
    {
        UIPanel.transform.parent.gameObject.SetActive(true);
        UIPanel.SetActive(true);
        GameManager.Instance.UIIsActive = true;
    }

    public void CloseUIPanel()
    {
        UIPanel.transform.parent.gameObject.SetActive(false);
        UIPanel.SetActive(false);
        GameManager.Instance.UIIsActive = false;
    }
}
