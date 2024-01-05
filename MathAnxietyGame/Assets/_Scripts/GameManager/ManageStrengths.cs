using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

public class ManageStrengths : MonoBehaviour
{
    private const int TOTAL_STRENGTHS = 24, RANDOM_STRENGTHS = 9;

    private bool isRandomized = false;

    [SerializeField]
    private GameObject strengthButtons, strengthNotes;

    [SerializeField]
    private StrengthCheckTeacher strengthTeacher;
    [SerializeField]
    private StrenghtNoteTracker highlightStrengthUI;
    [SerializeField]
    private GameObject highlightConfirmUI;

    [SerializeField]
    private AudioClip correctSound;

    // Remove serialize in final version
    [SerializeField]
    public List<LocalizedString> goalStrengths = new List<LocalizedString>();
    [SerializeField]
    private List<StrengthNote> highlightedStrengths = new List<StrengthNote>();

    [SerializeField]
    private Strength[] allStrenghts = new Strength[TOTAL_STRENGTHS];
    [SerializeField]
    private List<StrengthNote> allStrengthNotes = new List<StrengthNote>();

    // Remove serialize in final version
    [SerializeField]
    private List<Strength> randomStrengths = new List<Strength>();

    public List<Strength> learnedStrengths = new List<Strength>();

    /// <summary>
    /// Randomizes which 9 of the 24 strengths should be shown in game
    /// </summary>
    public void RandomizeStrengths()
    {
        if (!isRandomized)
        {
            isRandomized = true;

            for (int iStrenght = 0; iStrenght < RANDOM_STRENGTHS; iStrenght++)
            {
                Strength randomStrenght = null;
                while (randomStrengths.Contains(randomStrenght) || randomStrenght == null)
                    randomStrenght = allStrenghts[Random.Range(0, TOTAL_STRENGTHS - 1)];
                randomStrengths.Add(randomStrenght);
            }

            UpdateStrengthSelection();
            UpdateStrengthNotes();
        }
    }

    /// <summary>
    /// Updates the strength selection screen based on the 9 random strengths
    /// </summary>
    private void UpdateStrengthSelection()
    {
        for (int iStrenght = 0; iStrenght < RANDOM_STRENGTHS; iStrenght++)
        {
            GameObject strenghtButton = strengthButtons.transform.GetChild(iStrenght).gameObject;
            strenghtButton.GetComponent<LocalizeStringEvent>().StringReference.SetReference(
                randomStrengths[iStrenght].name.TableReference, randomStrengths[iStrenght].name.TableEntryReference);
            strenghtButton.GetComponent<StrengthTooltip>().MyStrength = randomStrengths[iStrenght];
        }
    }

    /// <summary>
    /// Updates the strength notes int the maze based on the 9 random strengths
    /// </summary>
    private void UpdateStrengthNotes()
    {
        for (int iStrenght = 0; iStrenght < RANDOM_STRENGTHS; iStrenght++)
        {
            GameObject strenghtNote = strengthNotes.transform.GetChild(iStrenght).gameObject;
            strenghtNote.GetComponentInChildren<DialogueTrigger>().dialogue = randomStrengths[iStrenght].tasksDialogue;
            strenghtNote.GetComponentInChildren<StrengthNote>().strength = randomStrengths[iStrenght];
            allStrengthNotes.Add(strenghtNote.GetComponentInChildren<StrengthNote>());
        }
    }

    /// <summary>
    /// Saves the highlighted/goal strengths as the learned strengths
    /// </summary>
    private void SaveLearnedStrengths()
    {
        foreach (StrengthNote note in highlightedStrengths)
        {
            learnedStrengths.Add(note.strength);
        }
    }

    /// <summary>
    /// Keeps track of which strengths were chosen and should be checked for the goal
    /// </summary>
    /// <param name="selectedStrengths"></param>
    public void ReceiveStrenghtSelection(List<GameObject> selectedStrengths)
    {
        foreach (GameObject strength in selectedStrengths)
        {
            goalStrengths.Add(strength.GetComponent<LocalizeStringEvent>().StringReference);
        }
        strengthTeacher.ContinueTaskExplanation();
        highlightStrengthUI.SetStrenghtHighlightUI();
    }

    /// <summary>
    /// Highlights a strength note when interacting with it
    /// </summary>
    /// <param name="strenghtNote">Note that should be highlighted</param>
    public void HighlightStrenght(StrengthNote strengthNote)
    {
        DeHighlightStrength(strengthNote);
        foreach (StrengthNote note in allStrengthNotes)
        {
            // Code for already selected strength on another note
            if (strengthNote.strengthButton == note.strengthButton)
            {
                if (note.giveConfirmation)
                {
                    highlightStrengthUI.gameObject.SetActive(false);
                    highlightConfirmUI.SetActive(true);
                    note.giveConfirmation = false;
                    highlightStrengthUI.ConfirmationNote = note;
                    return;
                }
                DeHighlightStrength(note);
            }
        }
        highlightedStrengths.Add(strengthNote);
        strengthNote.HighlightColor();
        strengthNote.giveConfirmation = true;
        strengthNote.CloseUIPanel();
        highlightConfirmUI.SetActive(false);
        
        if (strengthNote.strengthButton.GetLocalizedString() == strengthNote.strength.name.GetLocalizedString())
        {
            if (strengthNote.canGivePower)
            {
                //Give super power (glow + destroy wall)
                GameManager.Instance.soundManager.playCorrectSound();
                GameManager.Instance.thePlayer.GetComponent<PlayerControler>().GiveSuperStrength();
                strengthNote.canGivePower = false;
            }
            Debug.Log("Correct!!!");
        }
        
    }

    /// <summary>
    /// Unselects the strength note
    /// </summary>
    /// <param name="strength"></param>
    public void DeHighlightStrength(StrengthNote strengthNote)
    {
        if (highlightedStrengths.Contains(strengthNote))
        {
            highlightedStrengths.Remove(strengthNote);
            strengthNote.RemoveHighlightColor();
        }
        highlightConfirmUI.SetActive(false);
        strengthNote.giveConfirmation = false;
    }

    /// <summary>
    /// Checks if the 3 highlight strenght notes overlap with the chosen strengths
    /// </summary>
    /// <returns></returns>
    public bool CompareCorrectStrengths()
    {
        // Make sure you have 3 strenghts highlighted
        if (highlightedStrengths.Count < 3)
        {
            Debug.Log("Not all strengths!");
            return false;
        }

        int matchingStrengths = 0;

        foreach (StrengthNote note in highlightedStrengths)
        {
            foreach (LocalizedString goalStrength in goalStrengths)
            {
                if (goalStrength.GetLocalizedString() == note.strength.name.GetLocalizedString())
                {
                    matchingStrengths++;
                    break;
                }
            }
        }

        if (matchingStrengths == 3)
        {
            Debug.Log("All correct!");
            SaveLearnedStrengths();
            return true;
        }
        else
        {
            Debug.Log("Not all strengths are correct!");
            return false;
        }
    }
}
