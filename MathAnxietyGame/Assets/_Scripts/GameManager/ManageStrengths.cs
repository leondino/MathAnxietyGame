using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManageStrengths : MonoBehaviour
{
    private const int TOTAL_STRENGTHS = 24, RANDOM_STRENGTHS = 9;

    private bool isRandomized = false;

    public string[] GetGoalStrengths { get { return goalStrengths.ToArray(); } }

    [SerializeField]
    private GameObject strengthButtons, strengthNotes;

    // Remove serialize in final version
    [SerializeField]
    private List<string> goalStrengths = new List<string>();
    [SerializeField]
    private List<string> highlightedStrengths = new List<string>();

    [SerializeField]
    private Strength[] allStrenghts = new Strength[TOTAL_STRENGTHS];

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
            strenghtButton.GetComponentInChildren<TextMeshProUGUI>().text = randomStrengths[iStrenght].name;
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
            strenghtNote.GetComponentInChildren<StrengthNote>().strength = randomStrengths[iStrenght].name;
        }
    }

    /// <summary>
    /// Compares the goal strengths string value to the names of strength objects.
    /// Adds the correct ones to a list to use for data collection.
    /// </summary>
    private void SaveLearnedStrengths()
    {
        foreach (string goalStrength in goalStrengths)
        {
            foreach (Strength strength in randomStrengths)
            {
                if (goalStrength == strength.name)
                {
                    learnedStrengths.Add(strength);
                    break;
                }
            }
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
            goalStrengths.Add(strength.GetComponentInChildren<TextMeshProUGUI>().text);
        }
    }

    /// <summary>
    /// Highlights a strength note when interacting with it
    /// </summary>
    /// <param name="strenghtNote">Note that should be highlighted</param>
    public void HighlightStrenght(StrengthNote strenghtNote)
    {
        string strenght = strenghtNote.strength;
        if (!highlightedStrengths.Contains(strenght) && highlightedStrengths.Count < 3)
        {
            highlightedStrengths.Add(strenght);
            strenghtNote.HighlightColor();
        }
        else Debug.Log("Strenght already highlighted or already highlighted 3 strenghts");
    }

    /// <summary>
    /// Unselects the strength note
    /// </summary>
    /// <param name="strength"></param>
    public void DeHighlightStrength(string strength)
    {
        if (highlightedStrengths.Contains(strength))
            highlightedStrengths.Remove(strength);
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

        foreach (string strenght in highlightedStrengths)
        {
            if (!goalStrengths.Contains(strenght))
            {
                Debug.Log("Wrong Strength!");
                return false;
            }
        }
        Debug.Log("All correct!");
        SaveLearnedStrengths();
        return true;
    }
}
