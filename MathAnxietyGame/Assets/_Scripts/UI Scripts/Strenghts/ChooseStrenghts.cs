using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChooseStrenghts : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> selectedStrenghts = new List<GameObject>();

    public StrengthCheckTeacher StrengthTeacher { get; set; }

    /// <summary>
    /// Adds or removes the selected strengt to the list of chosen strenghts.
    /// Changes the color of the button to visualize selection.
    /// </summary>
    public void SelectStrenght()
    {
        GameObject selectedStrenght = EventSystem.current.currentSelectedGameObject;

        // Only select when strenght isn't selected yet and less then 3 strenghts are selected
        if (!selectedStrenghts.Contains(selectedStrenght) && selectedStrenghts.Count < 3)
        {
            // Highlight color
            ColorBlock selectedColor = selectedStrenght.GetComponent<Button>().colors;
            selectedColor.normalColor = Color.green;
            selectedColor.selectedColor = Color.green;
            selectedStrenght.GetComponent<Button>().colors = selectedColor;

            selectedStrenghts.Add(selectedStrenght);
        }
        else
        {
            // Remove highlight color
            ColorBlock selectedColor = selectedStrenght.GetComponent<Button>().colors;
            selectedColor.normalColor = Color.white;
            selectedColor.selectedColor = Color.white;
            selectedStrenght.GetComponent<Button>().colors = selectedColor;

            selectedStrenghts.Remove(selectedStrenght);
        }
    }

    public void ConfirmSelection()
    {
        if (selectedStrenghts.Count >= 3)
        {
            GameManager.Instance.strenghtsManager.ReceiveStrenghtSelection(selectedStrenghts);

            StrengthTeacher.SetChosenStrengths();

            gameObject.SetActive(false);
            transform.parent.gameObject.SetActive(false);
            GameManager.Instance.UIIsActive = false;
        }
        else Debug.Log("Choose 3 strenghts please!");
    }
}
